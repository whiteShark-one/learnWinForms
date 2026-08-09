namespace learnWinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            //Application.Run(new Form1());
            Form1 form1 = new Form1();
            //GlobalFunc.Instance.formLogin = form1;
            /*
             * new Form1()不会启动窗口，ShowDialog()才会启动窗口并阻塞等待
                `ShowDialog()`：模态对话框打开窗体
                    关键特性：调用之后，代码在这里阻塞，原地暂停，不再往下执行，
                    一直等到这个对话框被关闭（`Close()`），`ShowDialog()` 才会返回，返回值就是 `DialogResult` 枚举
             */
            /*
             Program.cs: loginForm.ShowDialog()
                    ↓（代码阻塞，停在此处，运行登录窗体的消息循环）
            用户操作登录界面 → 点击登录按钮 → btnLogin_Click
                    ↓
            Form1内部执行：this.DialogResult = DialogResult.OK;
                    ↓
            Form1内部执行：this.Close(); //关闭窗口
                    ↓
            ShowDialog()结束，return loginForm.DialogResult（也就是OK）
                    ↓
            回到Program，拿到返回值，做if判断
             */
            if (form1.ShowDialog() == DialogResult.OK) {
                // 登录成功，把Index设置为程序真正窗体
                Application.Run(new Index());
            }

            //Application.Run(new Index());
        }
    }
}