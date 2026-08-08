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

        // 按钮2-4：IO BOUND 操作
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
        /*
            foreach+await(串行执行)
            执行时序：
                #1 处理第 1 个 url：发起请求 → await，让出 UI 线程，等待网络返回
                #2 必须等第一个 HTTP 完整结束拿到响应，才进入下一轮 foreach 循环，发起第 2 个 url 请求
                #3 第二个请求发送、等待完成 → 第三个…… 以此类推
            总耗时： 请求 1 耗时 + 请求 2 耗时 + 请求 3 耗时 + …… 全部累加。
            特点：
                - 界面不会卡死，每次await释放 UI 消息循环
                - 按 urls 顺序逐个请求，拿到一个结果，立刻就可以AppendText更新 UI
                - 缺点：url 多的时候总时间很长，没有利用网络可以并发的能力
                - 优点：收到一个就渲染一个，逐步显示内容，用户可以逐步看到输出
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
            执行时序：
                #1 urls.Select(...) 遍历所有 url，一次性把全部 HTTP 请求同时发出去，网络请求全部并行跑。
                #2 await Task.WhenAll 等待所有 url 全部请求完毕。
                #3 必须全部请求都结束之后，才拿到 results 数组。
            总耗时 ≈ 最慢那一个请求的耗时，不是累加。
        请求1 ──┐
        请求2 ──┼──同时发起，一起等待
        请求3 ──┘
            总时间 ≈ max(T1,T2,T3)
         */
        private async void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var sw = Stopwatch.StartNew();

            var results = await Task.WhenAll(urls.Select(httpClient.GetStringAsync));
            foreach (var str in results)
            {
                textBox1.AppendText(str.Replace("\n", Environment.NewLine));
                textBox1.AppendText(Environment.NewLine);
            }
            sw.Stop();
            MessageBox.Show($"{sw.ElapsedMilliseconds} ms");
        }

        // 按钮5-7 CPU BOUND 操作
        static void cpuBoundMethod()
        {
            for (int i = 0; i < 1000000; i++) { }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        // button5：CPU耗时调用，同步调用，卡住UI线程
        private void button5_Click(object sender, EventArgs e)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                cpuBoundMethod();
            }
            sw.Stop();
            MessageBox.Show($"cpu5计算： {sw.ElapsedMilliseconds}");
        }

        //// button6：CPU耗时调用，异步调用
        ///  让计算任务放到线程池中执行，不阻塞UI线程
        /*
            执行流程：
                #1 当前运行在UI 线程，执行到Task.Run(...)
                #2 Task.Run 立刻向线程池提交任务，从 .NET 线程池拿一条工作线程，去执行括号里面的委托代码，得到一个 Task 对象
                #3 主线程（UI 线程）不等待，直接继续往下执行后面代码
                #4 线程池的某条后台工作线程，去跑里面 for 循环，循环调用 1000 次cpuBoundMethod()
                #5 UI 线程和后台线程同时并行跑 → UI 可以响应拖动、点击，不会卡住。
            重点区分：
                - cpuBoundMethod()：CPU 密集，需要线程一直在跑，必须占用线程；
                - HttpClient.GetStringAsync：IO 密集，等待网络阶段不占用线程。
         */
        private async void button6_Click(object sender, EventArgs e)
        {
            var sw = Stopwatch.StartNew();
            await Task.Run(() => // Run()会返回一个Task，使用await进行一个异步的等待
            {
                for (int i = 0; i < 1000; i++)
                {
                    cpuBoundMethod();
                }
            });
            sw.Stop();
            MessageBox.Show($"cpu6计算： {sw.ElapsedMilliseconds}");
        }

        // button7：CPU耗时任务：异步调用，并行计算
        /*
            Parallel.For(int fromInclusive, int toExclusive, Action<int> body)
                - Parallel 在 System.Threading.Tasks，专门用于 CPU 密集型并行计算（CPU‑bound）
                - 它内部会自己向线程池申请多个线程池线程，拆分这 1000 次迭代，多核 CPU 上同时跑
                - Parallel.For 本身是阻塞方法：它会等待所有迭代全部执行完毕，才会返回
            执行流程：
                #1 UI 线程调用 Task.Run，把内层 lambda 交给线程池，立刻返回；UI 线程继续跑，不会阻塞。
                #2 线程池拿出1 个工作线程 T1，开始执行这个 lambda：Parallel.For(0,1000,...)
                #3 T1 线程进入 Parallel.For：
                #4 Parallel 内部根据 CPU 核心数，向线程池再拿若干线程；
                #5 将 0‑999 共 1000 次循环迭代拆分，分配到多个线程池线程上并行执行 cpuBoundMethod；
                #6 T1 这个线程会被 Parallel.For 阻塞住，等待全部 1000 次迭代全部跑完；
                #7 所有迭代执行完毕 → Parallel.For 返回 → lambda 结束；该线程池线程归还线程池。
            关键点：
                - Task.Run 只提供外层一个线程；
                - 真正多线程并行是Parallel.For 内部自己创建管理的；
                - Parallel.For 本身是阻塞，调用它的那个线程（T1）会等到全部循环结束
         */
        private async void button7_Click(object sender, EventArgs e)
        {
            var sw = Stopwatch.StartNew();
            await Task.Run(() => Parallel.For(0,1000,i => cpuBoundMethod()));
            sw.Stop();
            MessageBox.Show($"cpu6计算： {sw.ElapsedMilliseconds}");
        }
        /*
            总结
                #1 Task.Run：把整个 Parallel.For 丢进线程池，保护 UI 不卡死，仅此而已。
                #2 Parallel.For：CPU 密集并行工具，内部会使用多个线程池线程拆分循环，并且是阻塞调用。
                #3 IO 场景不要用 Parallel；IO 用 async/WhenAll
         */


    }
}
