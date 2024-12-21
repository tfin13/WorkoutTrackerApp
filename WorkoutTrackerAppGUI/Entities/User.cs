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
        public string Username { get; set; } // Made setter public for database operations
        public string PasswordHash { get; private set; } // Made getter public for secure access
        public string Email { get; set; } // Made setter public for database operations

        // Constructor
        public User(string username, string passwordHash, string email)
        {
            Console.WriteLine($"User Constructor Called - Username: '{username}', Email: '{email}', PasswordHash: '{passwordHash}'");

            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email is null or empty.");
                throw new ArgumentException("Invalid email format.");
            }

            email = email.Trim(); // Trim leading/trailing spaces
            Console.WriteLine($"Trimmed Email: '{email}'");

            if (!IsValidEmail(email))
            {
                Console.WriteLine("Email validation failed.");
                throw new ArgumentException("Invalid email format.");
            }

            Username = username;
            PasswordHash = passwordHash;
            Email = email;
        }

        // Parameterless Constructor (Needed for database mapping)
        public User() { }

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

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }


        public bool VerifyPassword(string password)
        {
            string hashedInput = HashPassword(password);
            Console.WriteLine($"Input Password Hash: {hashedInput}");
            Console.WriteLine($"Stored Password Hash: {PasswordHash}");

            return hashedInput == PasswordHash;
        }
        

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            email = email.Trim();
            bool isValid = Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            Console.WriteLine($"Validating Email: '{email}', Validation Result: {isValid}");
            return isValid;
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
