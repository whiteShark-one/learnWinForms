namespace learnWinForms
{
    partial class Index
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
            args = new Label();
            SuspendLayout();
            // 
            // args
            // 
            args.AutoSize = true;
            args.Location = new Point(314, 179);
            args.Name = "args";
            args.Size = new Size(0, 20);
            args.TabIndex = 0;
            // 
            // Index
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(args);
            Name = "Index";
            Text = "Index";
            FormClosing += Index_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label args;
    }
}