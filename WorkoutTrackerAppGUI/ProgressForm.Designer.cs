namespace WorkoutTrackerAppGUI
{
    partial class ProgressForm
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
            lblDate = new Label();
            lblMetricName = new Label();
            lblMetricValue = new Label();
            dateTimePicker1 = new DateTimePicker();
            txtMetricName = new TextBox();
            txtMetricValue = new TextBox();
            lstProgress = new ListView();
            btnAddEntry = new Button();
            btnDeleteEntry = new Button();
            btnBack = new Button();
            SuspendLayout();
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(26, 27);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(34, 15);
            lblDate.TabIndex = 0;
            lblDate.Text = "Date:";
            // 
            // lblMetricName
            // 
            lblMetricName.AutoSize = true;
            lblMetricName.Location = new Point(26, 61);
            lblMetricName.Name = "lblMetricName";
            lblMetricName.Size = new Size(79, 15);
            lblMetricName.TabIndex = 1;
            lblMetricName.Text = "Metric Name:";
            // 
            // lblMetricValue
            // 
            lblMetricValue.AutoSize = true;
            lblMetricValue.Location = new Point(26, 95);
            lblMetricValue.Name = "lblMetricValue";
            lblMetricValue.Size = new Size(75, 15);
            lblMetricValue.TabIndex = 2;
            lblMetricValue.Text = "Metric Value:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dateTimePicker1.Location = new Point(120, 27);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(433, 23);
            dateTimePicker1.TabIndex = 3;
            // 
            // txtMetricName
            // 
            txtMetricName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMetricName.Location = new Point(120, 61);
            txtMetricName.Name = "txtMetricName";
            txtMetricName.Size = new Size(433, 23);
            txtMetricName.TabIndex = 4;
            txtMetricName.TextChanged += textBox1_TextChanged;
            // 
            // txtMetricValue
            // 
            txtMetricValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMetricValue.Location = new Point(120, 95);
            txtMetricValue.Name = "txtMetricValue";
            txtMetricValue.Size = new Size(433, 23);
            txtMetricValue.TabIndex = 5;
            // 
            // lstProgress
            // 
            lstProgress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstProgress.Location = new Point(26, 142);
            lstProgress.Name = "lstProgress";
            lstProgress.Size = new Size(527, 180);
            lstProgress.TabIndex = 6;
            lstProgress.UseCompatibleStateImageBehavior = false;
            lstProgress.SelectedIndexChanged += lstViewProgressEntries_SelectedIndexChanged;
            // 
            // btnAddEntry
            // 
            btnAddEntry.Location = new Point(25, 348);
            btnAddEntry.Name = "btnAddEntry";
            btnAddEntry.Size = new Size(75, 23);
            btnAddEntry.TabIndex = 7;
            btnAddEntry.Text = "Add Entry";
            btnAddEntry.UseVisualStyleBackColor = true;
            btnAddEntry.Click += btnAddEntry_Click;
            // 
            // btnDeleteEntry
            // 
            btnDeleteEntry.Location = new Point(106, 348);
            btnDeleteEntry.Name = "btnDeleteEntry";
            btnDeleteEntry.Size = new Size(92, 23);
            btnDeleteEntry.TabIndex = 8;
            btnDeleteEntry.Text = "Delete Entry";
            btnDeleteEntry.UseVisualStyleBackColor = true;
            btnDeleteEntry.Click += btnDeleteEntry_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(204, 348);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 9;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // ProgressForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBack);
            Controls.Add(btnDeleteEntry);
            Controls.Add(btnAddEntry);
            Controls.Add(lstProgress);
            Controls.Add(txtMetricValue);
            Controls.Add(txtMetricName);
            Controls.Add(dateTimePicker1);
            Controls.Add(lblMetricValue);
            Controls.Add(lblMetricName);
            Controls.Add(lblDate);
            Name = "ProgressForm";
            Text = "ProgressForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDate;
        private Label lblMetricName;
        private Label lblMetricValue;
        private DateTimePicker dateTimePicker1;
        private TextBox txtMetricName;
        private TextBox txtMetricValue;
        private ListView lstProgress;
        private Button btnAddEntry;
        private Button btnDeleteEntry;
        private Button btnBack;
    }
}