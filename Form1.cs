using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace learnWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 了解C#异步和并发编程
        HttpClient httpClient = new HttpClient();
        private string[] urls = [
            "https://raw.githubusercontent.com/KrnlsYs/AsyncExample/refs/heads/main/csharp.txt",
            "https://raw.githubusercontent.com/KrnlsYs/AsyncExample/refs/heads/main/HelloWorld.txt",
            "https://raw.githubusercontent.com/KrnlsYs/AsyncExample/refs/heads/main/KrnlsYs.txt",
            "https://raw.githubusercontent.com/KrnlsYs/AsyncExample/refs/heads/main/await.txt",
            "https://raw.githubusercontent.com/KrnlsYs/AsyncExample/refs/heads/main/async.txt",
            "https://raw.githubusercontent.com/KrnlsYs/AsyncExample/refs/heads/main/dotnet.txt",
            "https://raw.githubusercontent.com/KrnlsYs/AsyncExample/refs/heads/main/Microsoft.txt",
            "https://raw.githubusercontent.com/KrnlsYs/AsyncExample/refs/heads/main/VisualStudio.txt",
            "https://raw.githubusercontent.com/KrnlsYs/AsyncExample/refs/heads/main/Abracadabra.txt",
            "https://raw.githubusercontent.com/KrnlsYs/AsyncExample/refs/heads/main/Apple.txt"
            ];

        private void Names_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /*
            事件处理器签名：`void Method(object sender, EventArgs e)`
            async 事件只能用 `async void`，不能 async Task
         */
        /*
            UI 事件（Click、Load、MouseDown）允许唯一特例：`async void`
                - 普通业务方法尽量写 `async Task`；事件处理函数必须 async void
                - 如果你写成 `async Task`，VS 直接报签名不匹配，无法绑定到 Click 事件。
         */
        /*
            async void 的风险：
                - 如果 `await` 后面代码抛出异常，不会被 Task 捕获，会直接弹出程序崩溃窗口。
                - 建议加上 try‑catch
         */
        private async void button1_Click(object sender, EventArgs e)
        {
            //button1.Enabled = false;    // 防止重复点击
            //await Task.Delay(3000);     // 模拟耗时工作
            //MessageBox.Show("Finished");
            //button1.Enabled = true;
            // 健壮版本
            button1.Enabled = false;
            try
            {
                await Task.Delay(3000); //模拟耗时，比如数据库、文件IO
                MessageBox.Show("Finished");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message}");
            }
            finally
            {
                button1.Enabled = true;
            }
        }

        // 按钮2：阻塞UI线程，会造成界面卡死无法拖动
        /*
            为什么GetResult()会造成卡死？
         */
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var sw = Stopwatch.StartNew();

            foreach (var url in urls)
            {
                textBox1.AppendText(
                    httpClient.GetStringAsync(url).GetAwaiter().GetResult().Replace("\n", Environment.NewLine)
                    );
                textBox1.AppendText(Environment.NewLine + Environment.NewLine);
            }

            sw.Stop();
            MessageBox.Show($"{sw.ElapsedMilliseconds} ms");
        }
        // 按钮3：异步await调用IO下载操作，释放UI线程回WF的消息循环
        /*
            结论：await 会把 [当前 UI 线程] 释放回 WinForms 的消息循环
            方法button3_Click，运行在UI线程（消息循环线程）
            执行顺序：
                #1 `textBox1.Clear();` → UI 线程执行
                #2 `sw.StartNew()` → UI 线程执行
                #3 执行：`httpClient.GetStringAsync(url)`
                    - 发起 HTTP 网络请求，立刻返回 `Task<string>`
                    - 网络 IO 开始跑，不需要占用 CPU 线程（IO 完成端口）        
                #4 遇到await
                    - 此处发生暂停：方法在这里挂起，把当前 UI 线程归还给 WinForms 消息循环
                    - 线程跑去处理别的界面消息：窗口拖动、点击其它按钮、重绘界面，界面不会卡死
                    - 此时`button3_Click`方法是 “暂停状态”，还没结束，只是让出线程
                #5 网络请求完成（IO 完成），需要恢复继续执行 `await`后面代码：`.Replace(...)`
                    - WinForms 的 `SynchronizationContext` 会把后续代码 调度回原来的 UI 线程上执行。
                    - 等待 UI 线程空闲，消息循环处理完其它消息，回来继续跑 `await` 之后的语句。
                #6 `AppendText` 更新文本框（必须 UI 线程）
         */
        private async void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var sw = Stopwatch.StartNew();

            foreach (var url in urls)
            {
                textBox1.AppendText(
                    //httpClient.GetStringAsync(url).GetAwaiter().GetResult().Replace("\n", Environment.NewLine)
                    (await httpClient.GetStringAsync(url)).Replace("\n", Environment.NewLine)
                    );
                textBox1.AppendText(Environment.NewLine + Environment.NewLine);
            }

            sw.Stop();
            MessageBox.Show($"{sw.ElapsedMilliseconds} ms");
        }
        // 按钮4：异步+提高下载效率(并发)
        /*
            目前理解：
                摒弃foreach循环每个url、依次等待await每个io操作
                采取Task.WhenALL同时启用所有url的异步IO操作，同一时间段内，并发执行，await all url
         */

        private async void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var sw = Stopwatch.StartNew();

            var results = await Task.WhenAll(urls.Select(httpClient.GetStringAsync));
            foreach(var str in results)
            {
                textBox1.AppendText(str.Replace("\n", Environment.NewLine));
                textBox1.AppendText(Environment.NewLine);
            }
            sw.Stop();
            MessageBox.Show($"{sw.ElapsedMilliseconds} ms");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
