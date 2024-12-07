using System;
using System.Windows.Forms;
using WorkoutTrackerAppGUI.DataAccess;
using WorkoutTrackerAppGUI.Entities;

namespace WorkoutTrackerAppGUI
{
    public partial class UserForm : Form
    {
        private readonly DatabaseManager _databaseManager;

        // Constructor accepting DatabaseManager instance
        public UserForm(DatabaseManager databaseManager)
        {
            InitializeComponent();
            _databaseManager = databaseManager;
        }

        // Fetch a user by their ID (integer)
        private void btnGetUserById_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate the input and ensure it's an integer
                if (int.TryParse(txtUserId.Text, out int userId))
                {
                    var user = _databaseManager.GetUserById(userId);
                    if (user != null)
                    {
                        DisplayUser(user);
                    }
                    else
                    {
                        MessageBox.Show($"User with ID {userId} not found.", "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid numeric User ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Fetch a user by their username (string)
        private void btnGetByUsername_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text;

                if (!string.IsNullOrWhiteSpace(username))
                {
                    var user = _databaseManager.GetUserByUsername(username);
                    if (user != null)
                    {
                        DisplayUser(user);
                    }
                    else
                    {
                        MessageBox.Show($"User with username '{username}' not found.", "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a username.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Display user information on the form
        private void DisplayUser(User user)
        {
            txtUserId.Text = user.UserId.ToString();
            txtUsername.Text = user.Username;
            txtEmail.Text = user.Email;
            // Display other user fields as needed
        }
    }
}
