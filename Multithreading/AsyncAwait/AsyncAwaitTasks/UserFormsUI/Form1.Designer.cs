namespace UserFormsUI
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.FoundedUsersList = new System.Windows.Forms.ListBox();
            this.FindUsersButton = new System.Windows.Forms.Button();
            this.NameField = new System.Windows.Forms.TextBox();
            this.SurnameField = new System.Windows.Forms.TextBox();
            this.AgeField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CreateUserButton = new System.Windows.Forms.Button();
            this.UpdateUserButton = new System.Windows.Forms.Button();
            this.DeleteUserButton = new System.Windows.Forms.Button();
            this.UpdatedUserNameField = new System.Windows.Forms.TextBox();
            this.UpdatedUserSurnameField = new System.Windows.Forms.TextBox();
            this.UpdatedUserAgeField = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.UpdatedUserLabel = new System.Windows.Forms.Label();
            this.CreatedUserLabel = new System.Windows.Forms.Label();
            this.DeletedUserLabel = new System.Windows.Forms.Label();
            this.ToAgeField = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FoundedUsersList
            // 
            this.FoundedUsersList.FormattingEnabled = true;
            this.FoundedUsersList.ItemHeight = 16;
            this.FoundedUsersList.Location = new System.Drawing.Point(25, 255);
            this.FoundedUsersList.Name = "FoundedUsersList";
            this.FoundedUsersList.Size = new System.Drawing.Size(285, 164);
            this.FoundedUsersList.TabIndex = 0;
            // 
            // FindUsersButton
            // 
            this.FindUsersButton.Location = new System.Drawing.Point(25, 211);
            this.FindUsersButton.Name = "FindUsersButton";
            this.FindUsersButton.Size = new System.Drawing.Size(95, 28);
            this.FindUsersButton.TabIndex = 1;
            this.FindUsersButton.Text = "Find users";
            this.FindUsersButton.UseVisualStyleBackColor = true;
            this.FindUsersButton.Click += new System.EventHandler(this.FindUsersButton_Click);
            // 
            // NameField
            // 
            this.NameField.Location = new System.Drawing.Point(25, 30);
            this.NameField.Name = "NameField";
            this.NameField.Size = new System.Drawing.Size(100, 22);
            this.NameField.TabIndex = 2;
            // 
            // SurnameField
            // 
            this.SurnameField.Location = new System.Drawing.Point(25, 78);
            this.SurnameField.Name = "SurnameField";
            this.SurnameField.Size = new System.Drawing.Size(100, 22);
            this.SurnameField.TabIndex = 3;
            // 
            // AgeField
            // 
            this.AgeField.Location = new System.Drawing.Point(25, 124);
            this.AgeField.Name = "AgeField";
            this.AgeField.Size = new System.Drawing.Size(100, 22);
            this.AgeField.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Surname";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Age";
            // 
            // CreateUserButton
            // 
            this.CreateUserButton.Location = new System.Drawing.Point(377, 71);
            this.CreateUserButton.Name = "CreateUserButton";
            this.CreateUserButton.Size = new System.Drawing.Size(95, 27);
            this.CreateUserButton.TabIndex = 8;
            this.CreateUserButton.Text = "Create user";
            this.CreateUserButton.UseVisualStyleBackColor = true;
            this.CreateUserButton.Click += new System.EventHandler(this.CreateUserButton_Click);
            // 
            // UpdateUserButton
            // 
            this.UpdateUserButton.Location = new System.Drawing.Point(377, 391);
            this.UpdateUserButton.Name = "UpdateUserButton";
            this.UpdateUserButton.Size = new System.Drawing.Size(98, 28);
            this.UpdateUserButton.TabIndex = 9;
            this.UpdateUserButton.Text = "Update user";
            this.UpdateUserButton.UseVisualStyleBackColor = true;
            this.UpdateUserButton.Click += new System.EventHandler(this.UpdateUserButton_Click);
            // 
            // DeleteUserButton
            // 
            this.DeleteUserButton.Location = new System.Drawing.Point(377, 163);
            this.DeleteUserButton.Name = "DeleteUserButton";
            this.DeleteUserButton.Size = new System.Drawing.Size(95, 31);
            this.DeleteUserButton.TabIndex = 10;
            this.DeleteUserButton.Text = "Delete user";
            this.DeleteUserButton.UseVisualStyleBackColor = true;
            this.DeleteUserButton.Click += new System.EventHandler(this.DeleteUserButton_Click);
            // 
            // UpdatedUserNameField
            // 
            this.UpdatedUserNameField.Location = new System.Drawing.Point(377, 251);
            this.UpdatedUserNameField.Name = "UpdatedUserNameField";
            this.UpdatedUserNameField.Size = new System.Drawing.Size(100, 22);
            this.UpdatedUserNameField.TabIndex = 11;
            // 
            // UpdatedUserSurnameField
            // 
            this.UpdatedUserSurnameField.Location = new System.Drawing.Point(377, 294);
            this.UpdatedUserSurnameField.Name = "UpdatedUserSurnameField";
            this.UpdatedUserSurnameField.Size = new System.Drawing.Size(100, 22);
            this.UpdatedUserSurnameField.TabIndex = 12;
            // 
            // UpdatedUserAgeField
            // 
            this.UpdatedUserAgeField.Location = new System.Drawing.Point(377, 335);
            this.UpdatedUserAgeField.Name = "UpdatedUserAgeField";
            this.UpdatedUserAgeField.Size = new System.Drawing.Size(100, 22);
            this.UpdatedUserAgeField.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(484, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Updated user name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(483, 294);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Updated user surname";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(483, 335);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "Updated user age";
            // 
            // UpdatedUserLabel
            // 
            this.UpdatedUserLabel.AutoSize = true;
            this.UpdatedUserLabel.Location = new System.Drawing.Point(484, 393);
            this.UpdatedUserLabel.Name = "UpdatedUserLabel";
            this.UpdatedUserLabel.Size = new System.Drawing.Size(0, 17);
            this.UpdatedUserLabel.TabIndex = 17;
            // 
            // CreatedUserLabel
            // 
            this.CreatedUserLabel.AutoSize = true;
            this.CreatedUserLabel.Location = new System.Drawing.Point(478, 75);
            this.CreatedUserLabel.Name = "CreatedUserLabel";
            this.CreatedUserLabel.Size = new System.Drawing.Size(0, 17);
            this.CreatedUserLabel.TabIndex = 18;
            // 
            // DeletedUserLabel
            // 
            this.DeletedUserLabel.AutoSize = true;
            this.DeletedUserLabel.Location = new System.Drawing.Point(478, 170);
            this.DeletedUserLabel.Name = "DeletedUserLabel";
            this.DeletedUserLabel.Size = new System.Drawing.Size(0, 17);
            this.DeletedUserLabel.TabIndex = 19;
            // 
            // ToAgeField
            // 
            this.ToAgeField.Location = new System.Drawing.Point(25, 168);
            this.ToAgeField.Name = "ToAgeField";
            this.ToAgeField.Size = new System.Drawing.Size(100, 22);
            this.ToAgeField.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(132, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 17);
            this.label7.TabIndex = 21;
            this.label7.Text = "To the age";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 736);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ToAgeField);
            this.Controls.Add(this.DeletedUserLabel);
            this.Controls.Add(this.CreatedUserLabel);
            this.Controls.Add(this.UpdatedUserLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UpdatedUserAgeField);
            this.Controls.Add(this.UpdatedUserSurnameField);
            this.Controls.Add(this.UpdatedUserNameField);
            this.Controls.Add(this.DeleteUserButton);
            this.Controls.Add(this.UpdateUserButton);
            this.Controls.Add(this.CreateUserButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AgeField);
            this.Controls.Add(this.SurnameField);
            this.Controls.Add(this.NameField);
            this.Controls.Add(this.FindUsersButton);
            this.Controls.Add(this.FoundedUsersList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox FoundedUsersList;
        private System.Windows.Forms.Button FindUsersButton;
        private System.Windows.Forms.TextBox NameField;
        private System.Windows.Forms.TextBox SurnameField;
        private System.Windows.Forms.TextBox AgeField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CreateUserButton;
        private System.Windows.Forms.Button UpdateUserButton;
        private System.Windows.Forms.Button DeleteUserButton;
        private System.Windows.Forms.TextBox UpdatedUserNameField;
        private System.Windows.Forms.TextBox UpdatedUserSurnameField;
        private System.Windows.Forms.TextBox UpdatedUserAgeField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label UpdatedUserLabel;
        private System.Windows.Forms.Label CreatedUserLabel;
        private System.Windows.Forms.Label DeletedUserLabel;
        private System.Windows.Forms.TextBox ToAgeField;
        private System.Windows.Forms.Label label7;
    }
}

