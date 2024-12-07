namespace WorkoutTrackerAppGUI
{
    partial class UserForm
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
            lblUserId = new Label();
            txtUserId = new TextBox();
            btnGetUserById = new Button();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            btnGetByUsername = new Button();
            SuspendLayout();
            // 
            // lblUserId
            // 
            lblUserId.AutoSize = true;
            lblUserId.Location = new Point(101, 121);
            lblUserId.Name = "lblUserId";
            lblUserId.Size = new Size(47, 15);
            lblUserId.TabIndex = 0;
            lblUserId.Text = "User ID:";
            // 
            // txtUserId
            // 
            txtUserId.Location = new Point(183, 118);
            txtUserId.Name = "txtUserId";
            txtUserId.Size = new Size(259, 23);
            txtUserId.TabIndex = 1;
            // 
            // btnGetUserById
            // 
            btnGetUserById.Location = new Point(459, 121);
            btnGetUserById.Name = "btnGetUserById";
            btnGetUserById.Size = new Size(135, 23);
            btnGetUserById.TabIndex = 2;
            btnGetUserById.Text = "Get User by ID";
            btnGetUserById.UseVisualStyleBackColor = true;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(101, 163);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(63, 15);
            lblUsername.TabIndex = 3;
            lblUsername.Text = "Username:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(183, 160);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(259, 23);
            txtUsername.TabIndex = 4;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(101, 203);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(183, 200);
            txtEmail.Name = "txtEmail";
            txtEmail.ReadOnly = true;
            txtEmail.Size = new Size(259, 23);
            txtEmail.TabIndex = 6;
            // 
            // btnGetByUsername
            // 
            btnGetByUsername.Location = new Point(459, 163);
            btnGetByUsername.Name = "btnGetByUsername";
            btnGetByUsername.Size = new Size(135, 23);
            btnGetByUsername.TabIndex = 7;
            btnGetByUsername.Text = "Get User by Username";
            btnGetByUsername.UseVisualStyleBackColor = true;
            btnGetByUsername.Click += btnGetByUsername_Click;
            // 
            // UserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(647, 357);
            Controls.Add(btnGetByUsername);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Controls.Add(btnGetUserById);
            Controls.Add(txtUserId);
            Controls.Add(lblUserId);
            Name = "UserForm";
            Text = "UserForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUserId;
        private TextBox txtUserId;
        private Button btnGetUserById;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblEmail;
        private TextBox txtEmail;
        private Button btnGetByUsername;
    }
}