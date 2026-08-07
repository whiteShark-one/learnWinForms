namespace learnWinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>

        /*
            解决方案有 3 个关键文件：
                #1  `Form1.cs`：自己写的业务逻辑，事件处理方法写这里
                #2  `Form1.Designer.cs`：自动生成，创建控件、设置属性、绑定事件
                #3  `Program.cs`：程序入口，启动窗体
         */
        [STAThread]
        static void Main()
        {
            /*
                form1程序执行流程：
                    #1 程序启动，进入 `Program.Main()`
                    #2 `Application.Run(new Form1())` → 创建 Form1 实例
                    #3 执行 Form1 构造函数 → 调用 `InitializeComponent()`
                    #4 `InitializeComponent()`内部：
                        - new Button () 创建按钮对象
                        - 设置位置、大小、Text 等属性
                        - `btnDoWork.Click += btnDoWork_Click` 注册事件委托（订阅者）
                            此时只是注册，方法不会执行，以后该按钮被点击，就调用该函数btnDoWork_Click
                        - Controls.Add(btnDoWork); 把按钮挂载到窗体，绘制到屏幕
                    #5 窗体显示完成，程序进入**消息循环**，等待用户操作
                        - `btnDoWork_Click` 从来没有跑过，只是登记好了回调
                    #6 用户鼠标点击按钮
                        - Windows 操作系统捕捉鼠标点击消息
                        - WinForms 消息循环把消息分派给按钮控件
                        - 按钮检测发生 Click 事件，触发执行所有注册好的委托方法
                        - 执行我们写的 `btnDoWork_Click(object sender, EventArgs e)`
                        - 运行内部代码：Console 输出，弹出 MessageBox
                    #7 事件方法执行完毕，返回，继续等待下一次用户输入
             */
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}