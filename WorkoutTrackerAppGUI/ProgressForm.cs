using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkoutTrackerAppGUI.DataAccess;
using WorkoutTrackerAppGUI.Entities;

namespace WorkoutTrackerAppGUI
{
    public partial class ProgressForm : Form
    {
        private readonly DatabaseManager _databaseManager;
        public ProgressForm(DatabaseManager databaseManager)
        {
            InitializeComponent();
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));

            // Link Load event to load progress entries
            this.Load += ProgressForm_Load;
        }

        // Load ProgressForm event
        private void ProgressForm_Load(object sender, EventArgs e)
        {
            LoadProgressEntries();
        }

        // Load Progress entries into the ListBox
        private void LoadProgressEntries()
        {
            try
            {
                lstProgress.Items.Clear();
                var progressEntries = _databaseManager.GetProgressForUser(1); // Replace 1 with the actual user ID
                foreach (var progress in progressEntries)
                {
                    lstProgress.Items.Add($"Date: {progress.Date.ToShortDateString()} - Metrics: {progress.Metrics.Count}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading progress entries: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            try
            {
                var date = DateTime.Now;
                var metrics = new Dictionary<string, double>();
                metrics["Calories Burned"] = 500; // Example metric
                metrics["Workout Duration"] = 60; // Example metric in minutes

                var newProgress = new ProgressTracking(1, date, metrics); // Replace 1 with the actual user ID
                _databaseManager.AddProgressEntry(newProgress);
                LoadProgressEntries();
                MessageBox.Show("Progress added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding progress: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            if (lstProgress.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a progress entry to delete.", "Delete Progress", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get the selected item
                var selectedItem = lstProgress.SelectedItems[0]; // First selected item
                string selectedEntry = selectedItem.Text; // Use the `Text` property or SubItems if needed

                // Logic to delete progress entry by identifying the specific ID or date
                MessageBox.Show($"Deleting: {selectedEntry}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Example: Remove from ListView after deletion
                lstProgress.Items.Remove(selectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting progress: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lstViewProgressEntries_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
