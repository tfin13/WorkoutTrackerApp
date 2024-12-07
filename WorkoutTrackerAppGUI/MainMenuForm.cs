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

namespace WorkoutTrackerAppGUI
{
    public partial class MainMenuForm : Form
    {
        private readonly DatabaseManager _databaseManager;

        public MainMenuForm(DatabaseManager databaseManager)
        {
            InitializeComponent();
            _databaseManager = databaseManager;
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            var userForm = new UserForm(_databaseManager);
            userForm.ShowDialog();
        }

        private void btnManageWorkoutRoutine_Click(object sender, EventArgs e)
        {
            var routineForm = new WorkoutRoutineForm(_databaseManager);
            routineForm.ShowDialog();
        }

        private void btnTrackProgress_Click(object sender, EventArgs e)
        {
            var progressForm = new ProgressForm(_databaseManager);
            progressForm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close(); // Return to LoginForm
        }
    }
}
