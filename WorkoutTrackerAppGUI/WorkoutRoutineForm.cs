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
    public partial class WorkoutRoutineForm : Form
    {
        private readonly DatabaseManager _databaseManager;

        public WorkoutRoutineForm(DatabaseManager databaseManager)
        {
            InitializeComponent();
            _databaseManager = databaseManager;

            // Link the load event to the WorkoutRoutineForm_Load method
            this.Load += WorkoutRoutineForm_Load;
        }

        private void WorkoutRoutineFrom_Load(object sender, EventArgs e)
        {
            LoadWorkoutRoutines();
        }

        private void LoadWorkoutRoutines()
        {
            try
            {
                lstRoutines.Items.Clear(); // Ensure lstRoutines is defined in your form
                var routines = _databaseManager.GetAllWorkoutRoutines(); // Fetch routines
                foreach (var routine in routines)
                {
                    lstRoutines.Items.Add($"{routine.RoutineId}: {routine.Name}"); // Display ID and name
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading routines: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddRoutine_Click(object sender, EventArgs e)
        {
            string routineName = Prompt.ShowDialog("Enter routine name:", "Add Routine");
            if (!string.IsNullOrWhiteSpace(routineName))
            {
                try
                {
                    var routine = new WorkoutRoutine(routineName, null, new List<string>(), TimeSpan.Zero);
                    _databaseManager.AddWorkoutRoutine(routine);
                    LoadWorkoutRoutines();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding routine: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void btnEditRoutine_Click(object sender, EventArgs e)
        {
            if (lstRoutines.SelectedItem == null)
            {
                MessageBox.Show("Please select a routine to edit.", "Edit Routine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string currentRoutine = lstRoutines.SelectedItem.ToString();
            string newRoutineName = Prompt.ShowDialog("Edit routine name:", "Edit Routine", currentRoutine);

            if (!string.IsNullOrWhiteSpace(newRoutineName))
            {
                try
                {
                    var routine = _databaseManager.GetWorkoutRoutineById(int.Parse(currentRoutine)); // Replace with appropriate retrieval method
                    routine.Name = newRoutineName;
                    _databaseManager.UpdateWorkoutRoutine(routine); // Assuming this method exists
                    LoadWorkoutRoutines();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error editing routine: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeleteRoutine_Click(object sender, EventArgs e)
        {
            if (lstRoutines.SelectedItem == null)
            {
                MessageBox.Show("Please select a routine to delete.", "Delete Routine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedRoutine = lstRoutines.SelectedItem.ToString();

            if (MessageBox.Show($"Are you sure you want to delete '{selectedRoutine}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _databaseManager.DeleteWorkoutRoutine(int.Parse(selectedRoutine)); // Replace with appropriate ID
                    LoadWorkoutRoutines();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting routine: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddExercise_Click(object sender, EventArgs e)
        {
            if (lstRoutines.SelectedItem == null)
            {
                MessageBox.Show("Please select a routine to add an exercise.", "Add Exercise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string exerciseName = Prompt.ShowDialog("Enter exercise name:", "Add Exercise");
            if (!string.IsNullOrWhiteSpace(exerciseName))
            {
                try
                {
                    var routine = _databaseManager.GetWorkoutRoutineById(int.Parse(lstRoutines.SelectedItem.ToString())); // Replace with ID
                    routine.Exercises.Add(exerciseName);
                    _databaseManager.UpdateWorkoutRoutine(routine);
                    LoadWorkoutRoutines();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding exercise: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRemoveExercise_Click(object sender, EventArgs e)
        {
            if (lstRoutines.SelectedItem == null)
            {
                MessageBox.Show("Please select a routine to remove an exercise.", "Remove Exercise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string routineName = lstRoutines.SelectedItem.ToString();
            string exerciseName = Prompt.ShowDialog("Enter exercise name to remove:", "Remove Exercise");

            if (!string.IsNullOrWhiteSpace(exerciseName))
            {
                try
                {
                    var routine = _databaseManager.GetWorkoutRoutineById(int.Parse(routineName)); // Replace with ID
                    routine.Exercises.Remove(exerciseName);
                    _databaseManager.UpdateWorkoutRoutine(routine);
                    LoadWorkoutRoutines();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error removing exercise: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void lstBoxExercises_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void WorkoutRoutineForm_Load(object sender, EventArgs e)
        {

        }
    }
}
