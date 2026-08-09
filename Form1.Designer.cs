namespace learnWinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            loginTitle = new Label();
            userNameMsg = new Label();
            textUsername = new TextBox();
            passwordMsg = new Label();
            textPassword = new TextBox();
            loginBtn = new Button();
            lblMsg = new Label();
            SuspendLayout();
            // 
            // loginTitle
            // 
            loginTitle.AutoSize = true;
            loginTitle.Font = new Font("黑体", 18F, FontStyle.Regular, GraphicsUnit.Point, 134);
            loginTitle.Location = new Point(341, 73);
            loginTitle.Name = "loginTitle";
            loginTitle.Size = new Size(73, 30);
            loginTitle.TabIndex = 0;
            loginTitle.Text = "登录";
            // 
            // userNameMsg
            // 
            userNameMsg.AutoSize = true;
            userNameMsg.Font = new Font("华文行楷", 16.1999989F, FontStyle.Regular, GraphicsUnit.Point, 134);
            userNameMsg.ForeColor = SystemColors.InfoText;
            userNameMsg.Location = new Point(171, 135);
            userNameMsg.Name = "userNameMsg";
            userNameMsg.Size = new Size(94, 29);
            userNameMsg.TabIndex = 1;
            userNameMsg.Text = "账号：";
            // 
            // textUsername
            // 
            textUsername.Location = new Point(244, 137);
            textUsername.Name = "textUsername";
            textUsername.Size = new Size(293, 27);
            textUsername.TabIndex = 2;
            // 
            // passwordMsg
            // 
            passwordMsg.AutoSize = true;
            passwordMsg.Font = new Font("华文行楷", 16.1999989F, FontStyle.Regular, GraphicsUnit.Point, 134);
            passwordMsg.ForeColor = SystemColors.InfoText;
            passwordMsg.Location = new Point(171, 184);
            passwordMsg.Name = "passwordMsg";
            passwordMsg.Size = new Size(94, 29);
            passwordMsg.TabIndex = 3;
            passwordMsg.Text = "密码：";
            // 
            // textPassword
            // 
            textPassword.Location = new Point(244, 184);
            textPassword.Name = "textPassword";
            textPassword.PasswordChar = '*';
            textPassword.Size = new Size(293, 27);
            textPassword.TabIndex = 4;
            // 
            // loginBtn
            // 
            loginBtn.Location = new Point(341, 271);
            loginBtn.Name = "loginBtn";
            loginBtn.Size = new Size(94, 29);
            loginBtn.TabIndex = 5;
            loginBtn.Text = "登录";
            loginBtn.UseVisualStyleBackColor = true;
            loginBtn.Click += loginBtn_Click;
            // 
            // lblMsg
            // 
            lblMsg.AutoSize = true;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Location = new Point(341, 231);
            lblMsg.Name = "lblMsg";
            lblMsg.Size = new Size(0, 20);
            lblMsg.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblMsg);
            Controls.Add(loginBtn);
            Controls.Add(textPassword);
            Controls.Add(passwordMsg);
            Controls.Add(textUsername);
            Controls.Add(userNameMsg);
            Controls.Add(loginTitle);
            ForeColor = SystemColors.MenuHighlight;
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label loginTitle;
        private Label userNameMsg;
        private TextBox textUsername;
        private Label passwordMsg;
        private TextBox textPassword;
        private Button loginBtn;
        private Label lblMsg;
    }
}
