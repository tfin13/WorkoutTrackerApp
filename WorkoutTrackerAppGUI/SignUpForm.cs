using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkoutTrackerAppGUI.DataAccess;
using WorkoutTrackerAppGUI.Entities;

namespace WorkoutTrackerAppGUI
{
    public partial class SignUpForm : Form
    {
        private readonly DatabaseManager _databaseManager;

        // Constructor accepting DatabaseManager as a parameter
        public SignUpForm(DatabaseManager databaseManager = null)
        {
            InitializeComponent();

            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Retrieve user input
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string email = txtEmail.Text;

            // Validate input fields
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirm password validation
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Create a new user with hashed password
                var newUser = User.Register(username, password, email);
                _databaseManager.AddUser(newUser); // Add the new user to the database

                MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Close the form after successful registration
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


