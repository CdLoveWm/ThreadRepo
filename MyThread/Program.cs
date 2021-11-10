using System;
using System.Threading;

namespace MyThread
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadTest.Show1();
            //ThreadTest.Show2();
            ThreadTest.Show5();
            Console.ReadKey();
        }
    }

    /// <summary>
    /// .Net1.0 1.1 的时候出现的Thread
    /// </summary>
    public class ThreadTest
    {
        /// <summary>
        /// 多线程启动
        /// </summary>
        public static void Show1()
        {
            Console.WriteLine($"Show1 方法开始执行...{Thread.CurrentThread.ManagedThreadId}");
            ThreadStart threadStart = () => {
                Console.WriteLine($"启动线程...{Thread.CurrentThread.ManagedThreadId}");
            };
            Thread thread = new Thread(threadStart);
            thread.Start();
            Console.WriteLine($"Show1 方法结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// 线程等待
        /// </summary>
        public static void Show2()
        {
            Console.WriteLine($"Show2 方法开始执行...{Thread.CurrentThread.ManagedThreadId}");
            ThreadStart threadStart = () => {
                Console.WriteLine($"启动线程...{Thread.CurrentThread.ManagedThreadId}");
            };
            Thread thread = new Thread(threadStart);
            thread.Start();
            thread.Join();
            Console.WriteLine($"Show2 方法结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }

        /// <summary>
        /// 前后台线程
        /// </summary>
        public static void Show3()
        {
            Console.WriteLine($"Show3 方法开始执行...{Thread.CurrentThread.ManagedThreadId}");
            ThreadStart threadStart = () => {
                Console.WriteLine($"启动线程...{Thread.CurrentThread.ManagedThreadId}");
            };
            Thread thread = new Thread(threadStart);
            thread.IsBackground = true; // 设置为后台线程， false为前台线程
            thread.Start();
            Console.WriteLine($"Show3 方法结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// 线程优先级
        /// </summary>
        public static void Show4()
        {
            Console.WriteLine($"Show4 方法开始执行...{Thread.CurrentThread.ManagedThreadId}");
            ThreadStart threadStart = () => {
                Console.WriteLine($"启动线程...{Thread.CurrentThread.ManagedThreadId}");
            };
            Thread thread = new Thread(threadStart);
            thread.Priority = ThreadPriority.Highest; // 设置线程优先级为Highest
            thread.Start();
            Console.WriteLine($"Show4 方法结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// 线程状态
        /// </summary>
        public static void Show5()
        {
            ThreadStart threadStart = () => {
                Console.WriteLine($"启动线程...{Thread.CurrentThread.ManagedThreadId}");
            };
            Thread thread = new Thread(threadStart);
            Console.WriteLine($"线程状态：{thread.ThreadState}");
            thread.Start();
            Console.WriteLine($"线程状态：{thread.ThreadState}");
            thread.Join();
            Console.WriteLine($"线程状态：{thread.ThreadState}");
        }
        /// <summary>
        /// API 展示
        /// </summary>
        public static void Show6()
        {
            Thread thread = new Thread(() =>
            {
                Console.WriteLine("线程启动...");
            });
            thread.Start();

            #region 这些API基本都不用了，也尽量别用，不好控制
            //thread.Suspend(); // 线程挂起
            //thread.Resume(); // 线程唤醒
            //thread.Abort(); // 线程销毁(方式是抛异常)
            //Thread.ResetAbort(); // 取消Abort异常
            #endregion


        }
        /// <summary>
        /// Thread带回调
        /// </summary>
        /// <param name="action">主方法</param>
        /// <param name="callback">回调方法</param>
        public static void ThreadWithCallback(Action action, Action callback)
        {
            new Thread(() =>
            {
                action.Invoke();
                callback.Invoke();
            }).Start();
        }
        /// <summary>
        /// Thread带回调
        /// </summary>
        /// <param name="func">主方法</param>
        public static Func<T> ThreadWithReturn<T>(Func<T> func)
        {
            T t = default(T);
            Thread thread = new Thread(() =>
            {
                t = func();
            });
            return () =>
            {
                thread.Join();
                return t;
            };
        }
    }
}
