namespace learnWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 登录按钮点击
        // 验证登录规则
        private void loginBtn_Click(object sender, EventArgs e)
        {
            // #1 获取用户输入，去除首位空格
            string username = textUsername.Text.Trim();
            string password = textPassword.Text.Trim();

            // #2 非空校验 
            if (string.IsNullOrWhiteSpace(username))
            {
                //MessageBox.Show("请输入账户");
                lblMsg.Text = "请输入账号";
                return;
            }
            if (string.IsNullOrWhiteSpace(password)) {
                //MessageBox.Show("请输入密码");
                lblMsg.Text = "请输入密码";
                return;
            }
            // #3 验证账户密码（mock数据）
            string correctUser = "admin";
            string corretcPass = "123456";

            if (username == correctUser && password == corretcPass)
            {
                //MessageBox.Show("登录成功");
                lblMsg.Text = "登录成功";
                GlobalFunc.Instance.account = username;
                // 关闭登录窗体，打开index窗体
                //Index index = new Index();
                //index.Show();
                //this.Close();
                //this.Hide();

                this.DialogResult = DialogResult.OK;
                this.Close();   // 关闭 form1 窗口，才能返回Program.cs的form1.ShowDialog()
                //GlobalFunc.Instance.formLogin.Hide();
            } else
            {
                //MessageBox.Show("账号或密码错误");
                lblMsg.Text = "账号或密码错误";
                textPassword.Clear();
                textPassword.Focus();
            }

        }
    }
}
