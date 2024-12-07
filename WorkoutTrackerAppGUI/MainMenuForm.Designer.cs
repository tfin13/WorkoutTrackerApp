namespace WorkoutTrackerAppGUI
{
    partial class MainMenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblMainMenu = new Label();
            btnManageUsers = new Button();
            btnManageWorkoutRoutine = new Button();
            btnTrackProgress = new Button();
            btnLogout = new Button();
            SuspendLayout();
            // 
            // lblMainMenu
            // 
            lblMainMenu.AutoSize = true;
            lblMainMenu.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMainMenu.Location = new Point(358, 93);
            lblMainMenu.Name = "lblMainMenu";
            lblMainMenu.Size = new Size(120, 30);
            lblMainMenu.TabIndex = 0;
            lblMainMenu.Text = "Main Menu";
            // 
            // btnManageUsers
            // 
            btnManageUsers.Location = new Point(334, 154);
            btnManageUsers.Name = "btnManageUsers";
            btnManageUsers.Size = new Size(173, 23);
            btnManageUsers.TabIndex = 1;
            btnManageUsers.Text = "Manage Users";
            btnManageUsers.UseVisualStyleBackColor = true;
            btnManageUsers.Click += btnManageUsers_Click;
            // 
            // btnManageWorkoutRoutine
            // 
            btnManageWorkoutRoutine.Location = new Point(334, 193);
            btnManageWorkoutRoutine.Name = "btnManageWorkoutRoutine";
            btnManageWorkoutRoutine.Size = new Size(173, 23);
            btnManageWorkoutRoutine.TabIndex = 2;
            btnManageWorkoutRoutine.Text = "Manage Workout Routines";
            btnManageWorkoutRoutine.UseVisualStyleBackColor = true;
            btnManageWorkoutRoutine.Click += btnManageWorkoutRoutine_Click;
            // 
            // btnTrackProgress
            // 
            btnTrackProgress.Location = new Point(334, 232);
            btnTrackProgress.Name = "btnTrackProgress";
            btnTrackProgress.Size = new Size(173, 23);
            btnTrackProgress.TabIndex = 3;
            btnTrackProgress.Text = "Track Progress";
            btnTrackProgress.UseVisualStyleBackColor = true;
            btnTrackProgress.Click += btnTrackProgress_Click;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(334, 271);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(173, 23);
            btnLogout.TabIndex = 4;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLogout);
            Controls.Add(btnTrackProgress);
            Controls.Add(btnManageWorkoutRoutine);
            Controls.Add(btnManageUsers);
            Controls.Add(lblMainMenu);
            Name = "MainMenuForm";
            Text = "MainMenu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMainMenu;
        private Button btnManageUsers;
        private Button btnManageWorkoutRoutine;
        private Button btnTrackProgress;
        private Button btnLogout;
    }
}