namespace WorkoutTrackerAppGUI
{
    partial class WorkoutRoutineForm
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
            lblRoutineName = new Label();
            lblDuration = new Label();
            lblDescription = new Label();
            txtRoutineName = new TextBox();
            txtDuration = new TextBox();
            txtDescription = new TextBox();
            lstRoutines = new ListBox();
            btnAddRoutine = new Button();
            btnEditRoutine = new Button();
            btnDeleteRoutine = new Button();
            btnAddExercise = new Button();
            btnRemoveExercise = new Button();
            btnBack = new Button();
            SuspendLayout();
            // 
            // lblRoutineName
            // 
            lblRoutineName.AutoSize = true;
            lblRoutineName.Location = new Point(12, 9);
            lblRoutineName.Name = "lblRoutineName";
            lblRoutineName.Size = new Size(86, 15);
            lblRoutineName.TabIndex = 0;
            lblRoutineName.Text = "Routine Name:";
            // 
            // lblDuration
            // 
            lblDuration.AutoSize = true;
            lblDuration.Location = new Point(12, 47);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(93, 15);
            lblDuration.TabIndex = 1;
            lblDuration.Text = "Duration (mins):";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(12, 85);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(70, 15);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Description:";
            // 
            // txtRoutineName
            // 
            txtRoutineName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRoutineName.Location = new Point(111, 6);
            txtRoutineName.Name = "txtRoutineName";
            txtRoutineName.Size = new Size(493, 23);
            txtRoutineName.TabIndex = 3;
            // 
            // txtDuration
            // 
            txtDuration.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDuration.Location = new Point(111, 44);
            txtDuration.Name = "txtDuration";
            txtDuration.Size = new Size(493, 23);
            txtDuration.TabIndex = 4;
            // 
            // txtDescription
            // 
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Location = new Point(111, 82);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(493, 23);
            txtDescription.TabIndex = 5;
            // 
            // lstRoutines
            // 
            lstRoutines.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstRoutines.FormattingEnabled = true;
            lstRoutines.ItemHeight = 15;
            lstRoutines.Location = new Point(12, 139);
            lstRoutines.Name = "lstRoutines";
            lstRoutines.Size = new Size(592, 184);
            lstRoutines.TabIndex = 6;
            lstRoutines.SelectedIndexChanged += lstBoxExercises_SelectedIndexChanged;
            // 
            // btnAddRoutine
            // 
            btnAddRoutine.Location = new Point(9, 362);
            btnAddRoutine.Name = "btnAddRoutine";
            btnAddRoutine.Size = new Size(96, 23);
            btnAddRoutine.TabIndex = 7;
            btnAddRoutine.Text = "Add Routine";
            btnAddRoutine.UseVisualStyleBackColor = true;
            btnAddRoutine.Click += btnAddRoutine_Click;
            // 
            // btnEditRoutine
            // 
            btnEditRoutine.Location = new Point(111, 362);
            btnEditRoutine.Name = "btnEditRoutine";
            btnEditRoutine.Size = new Size(91, 23);
            btnEditRoutine.TabIndex = 8;
            btnEditRoutine.Text = "Edit Routine";
            btnEditRoutine.UseVisualStyleBackColor = true;
            btnEditRoutine.Click += btnEditRoutine_Click;
            // 
            // btnDeleteRoutine
            // 
            btnDeleteRoutine.Location = new Point(208, 362);
            btnDeleteRoutine.Name = "btnDeleteRoutine";
            btnDeleteRoutine.Size = new Size(97, 23);
            btnDeleteRoutine.TabIndex = 9;
            btnDeleteRoutine.Text = "Delete Routine";
            btnDeleteRoutine.UseVisualStyleBackColor = true;
            btnDeleteRoutine.Click += btnDeleteRoutine_Click;
            // 
            // btnAddExercise
            // 
            btnAddExercise.Location = new Point(311, 362);
            btnAddExercise.Name = "btnAddExercise";
            btnAddExercise.Size = new Size(86, 23);
            btnAddExercise.TabIndex = 10;
            btnAddExercise.Text = "Add Exercise";
            btnAddExercise.UseVisualStyleBackColor = true;
            btnAddExercise.Click += btnAddExercise_Click;
            // 
            // btnRemoveExercise
            // 
            btnRemoveExercise.Location = new Point(403, 362);
            btnRemoveExercise.Name = "btnRemoveExercise";
            btnRemoveExercise.Size = new Size(105, 23);
            btnRemoveExercise.TabIndex = 11;
            btnRemoveExercise.Text = "Remove Exercise";
            btnRemoveExercise.UseVisualStyleBackColor = true;
            btnRemoveExercise.Click += btnRemoveExercise_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(514, 362);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(105, 23);
            btnBack.TabIndex = 12;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // WorkoutRoutineForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBack);
            Controls.Add(btnRemoveExercise);
            Controls.Add(btnAddExercise);
            Controls.Add(btnDeleteRoutine);
            Controls.Add(btnEditRoutine);
            Controls.Add(btnAddRoutine);
            Controls.Add(lstRoutines);
            Controls.Add(txtDescription);
            Controls.Add(txtDuration);
            Controls.Add(txtRoutineName);
            Controls.Add(lblDescription);
            Controls.Add(lblDuration);
            Controls.Add(lblRoutineName);
            Name = "WorkoutRoutineForm";
            Text = "WorkoutRoutineForm";
            Load += WorkoutRoutineForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblRoutineName;
        private Label lblDuration;
        private Label lblDescription;
        private TextBox txtRoutineName;
        private TextBox txtDuration;
        private TextBox txtDescription;
        private ListBox lstRoutines;
        private Button btnAddRoutine;
        private Button btnEditRoutine;
        private Button btnDeleteRoutine;
        private Button btnAddExercise;
        private Button btnRemoveExercise;
        private Button btnBack;
    }
}