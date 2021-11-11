using System;
using System.Threading;

namespace MyThreadPool
{
    /// <summary>
    /// .NET2.0 ThreadPool 线程池
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            //ThreadPoolTest.Show1();
            //ThreadPoolTest.Show2();
            //ThreadPoolTest.Show3();
            ThreadPoolTest.Show4();

            Console.ReadKey();
        }
    }

    public class ThreadPoolTest
    {
        /// <summary>
        /// ThreadPool启动线程
        /// </summary>
        public static void Show1()
        {
            Console.WriteLine($"Show1 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            ThreadPool.QueueUserWorkItem(t =>
            {
                Console.WriteLine("t："+ t);
                Console.WriteLine($"启动线程...{Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine($"Show1 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// ThreadPool启动线程时传参
        /// </summary>
        public static void Show2()
        {
            Console.WriteLine($"Show2 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            ThreadPool.QueueUserWorkItem(t =>
            {
                Console.WriteLine($"启动线程...{Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine("t：" + t);
            }, "参数1");
            Console.WriteLine($"Show2 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// 查询及控制线程数量
        /// </summary>
        public static void Show3()
        {
            // 获取线程数量
            ThreadPool.GetMaxThreads(out int maxWorkThreads, out int maxCompletionPortThreads);
            Console.WriteLine($"最大工作线程数：{maxWorkThreads}，最大异步IO线程数量：{maxCompletionPortThreads}");
            ThreadPool.GetMinThreads(out int minWorkThreads, out int minCompletionPortThreads);
            Console.WriteLine($"最小工作线程数：{minWorkThreads}，最小IO线程数量：{minCompletionPortThreads}");

            // 设置线程数量
            ThreadPool.SetMaxThreads(1000, 999);
            ThreadPool.GetMaxThreads(out maxWorkThreads, out maxCompletionPortThreads);
            Console.WriteLine($"设置后的最大工作线程数：{maxWorkThreads}，最大IO线程数量：{maxCompletionPortThreads}");
            ThreadPool.SetMinThreads(12, 12);
            ThreadPool.GetMinThreads(out minWorkThreads, out minCompletionPortThreads);
            Console.WriteLine($"设置后的最小工作线程数：{minWorkThreads}，最小IO线程数量：{minCompletionPortThreads}");
        }
        /// <summary>
        /// 线程等待
        /// Set()--true
        /// ReSet()--false
        /// false--WaitOne等待
        /// true--WaitOne直接过去
        /// </summary>
        public static void Show4()
        {
            Console.WriteLine($"Show4 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(t =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"启动线程...{Thread.CurrentThread.ManagedThreadId}");
                resetEvent.Set();
            });
            resetEvent.WaitOne();
            Console.WriteLine($"Show4 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
