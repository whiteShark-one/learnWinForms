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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            textBox1 = new TextBox();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            label1 = new Label();
            lstNames = new ListBox();
            textName = new TextBox();
            btnAdd = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(10, 332);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(92, 28);
            button1.TabIndex = 2;
            button1.Text = "健壮异步";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(10, 364);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(92, 28);
            button2.TabIndex = 3;
            button2.Text = "IObutton2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(106, 364);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(92, 28);
            button3.TabIndex = 4;
            button3.Text = "IObutton3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(202, 364);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(92, 28);
            button4.TabIndex = 5;
            button4.Text = "IObutton4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(10, 23);
            textBox1.Margin = new Padding(2);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(485, 277);
            textBox1.TabIndex = 6;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button5
            // 
            button5.Location = new Point(10, 396);
            button5.Name = "button5";
            button5.Size = new Size(111, 29);
            button5.TabIndex = 7;
            button5.Text = "CPUbutton5";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(127, 397);
            button6.Name = "button6";
            button6.Size = new Size(111, 29);
            button6.TabIndex = 8;
            button6.Text = "CPUbutton6";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(244, 397);
            button7.Name = "button7";
            button7.Size = new Size(116, 29);
            button7.TabIndex = 9;
            button7.Text = "CPUbutton7";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(516, 26);
            label1.Name = "label1";
            label1.Size = new Size(59, 20);
            label1.TabIndex = 10;
            label1.Text = "Names";
            // 
            // lstNames
            // 
            lstNames.FormattingEnabled = true;
            lstNames.Location = new Point(516, 49);
            lstNames.Name = "lstNames";
            lstNames.Size = new Size(150, 104);
            lstNames.TabIndex = 11;
            // 
            // textName
            // 
            textName.Location = new Point(672, 49);
            textName.Name = "textName";
            textName.Size = new Size(125, 27);
            textName.TabIndex = 12;
            textName.Text = "txtName";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(672, 82);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(125, 29);
            btnAdd.TabIndex = 13;
            btnAdd.Text = "Add Name";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(821, 437);
            Controls.Add(btnAdd);
            Controls.Add(textName);
            Controls.Add(lstNames);
            Controls.Add(label1);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(textBox1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private TextBox textBox1;
        private Button button5;
        private Button button6;
        private Button button7;
        private Label label1;
        private ListBox lstNames;
        private TextBox textName;
        private Button btnAdd;
    }
}
