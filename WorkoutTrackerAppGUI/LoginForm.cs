using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WorkoutTrackerAppGUI.DataAccess;
using WorkoutTrackerAppGUI.Entities;

namespace WorkoutTrackerAppGUI
{
    public partial class LoginForm : Form
    {
        private readonly DatabaseManager _databaseManager;

        public LoginForm(DatabaseManager databaseManager)
        {
            InitializeComponent();
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim(); // Trim spaces
            string password = txtPassword.Text;

            Console.WriteLine($"Raw Email Input: '{txtEmail.Text}'");
            Console.WriteLine($"Trimmed Email: '{email}'");

            if (!User.IsValidEmail(email))
            {
                Console.WriteLine("Invalid email format detected.");
                MessageBox.Show("Invalid email format. Please use the format: example@example.com", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Fetch user from the database using email
                var user = _databaseManager.GetUserByEmail(email);

                if (user != null)
                {
                    Console.WriteLine($"Retrieved User: Username='{user.Username}', Email='{user.Email}'");
                    Console.WriteLine($"Stored Password Hash: {user.PasswordHash}");

                    if (user.VerifyPassword(password))
                    {
                        Console.WriteLine("Password verified successfully.");
                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        var mainMenu = new MainMenuForm(_databaseManager);
                        mainMenu.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        Console.WriteLine("Password verification failed.");
                        MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    Console.WriteLine("No user found with the provided email.");
                    MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSignUp_Click(object sender, EventArgs e)
        {
            this.Hide();
            var signUpForm = new SignUpForm(_databaseManager); // Pass DatabaseManager to SignUpForm
            signUpForm.ShowDialog();
            this.Show();
        }
    }
}
