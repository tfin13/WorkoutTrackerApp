using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace WorkoutTrackerAppGUI.Entities
{
    public class User
    {
        // Attributes
        public int UserId { get; set; }
        public string Username { get; private set; }
        private string PasswordHash { get; set; }
        public string Email { get; private set; }

        // Constructor
        public User(string username, string passwordHash, string email)
        {
            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format.");
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
                throw new ArgumentException("Username must be at least 3 characters.");

            Username = username;
            Email = email;
            PasswordHash = passwordHash; // Expecting a pre-hashed password
        }


        // Register new user with raw password
        public static User Register(string username, string password, string email)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters.");

            string hashedPassword = HashPassword(password);
            return new User(username, hashedPassword, email);
        }

        // Methods

        // Login: Validates username and password
        public bool Login(string inputUsername, string inputPassword)
        {
            return Username.Equals(inputUsername, StringComparison.OrdinalIgnoreCase) &&
                   VerifyPassword(inputPassword);
        }

        // Update profile information
        public void UpdateProfile(string newEmail)
        {
            if (!IsValidEmail(newEmail))
                throw new ArgumentException("Invalid email format.");
            Email = newEmail;
        }

        // Change Password
        public void ChangePassword(string currentPassword, string newPassword)
        {
            if (!VerifyPassword(currentPassword))
                throw new UnauthorizedAccessException("Current password is incorrect.");
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
                throw new ArgumentException("New password must be at least 6 characters.");

            PasswordHash = HashPassword(newPassword);
        }

        // Helper Methods

        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public bool VerifyPassword(string password)
        {
            // Publicly accessible for password verification
            return HashPassword(password) == PasswordHash;
        }

        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Retrieve user profile information
        public string GetProfile()
        {
            return $"Username: {Username}, Email: {Email}";
        }

        // Internal use for database operations
        public string GetPasswordHash()
        {
            return PasswordHash; // Exposes the password hash securely for database operations
        }
    }
}
