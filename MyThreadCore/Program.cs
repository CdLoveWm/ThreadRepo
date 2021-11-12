using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyThreadCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //MyThreadCoreTest.Show1();
            //MyThreadCoreTest.Show2();
            MyThreadCoreTest.Show3();

            Console.ReadKey();
        }
    }

    /// <summary>
    /// 多线程使用中其他事项
    /// </summary>
    public class MyThreadCoreTest
    {
        /// <summary>
        /// 多线程异常处理
        /// 线程里面的异常在线程外面是抓取不到的，必须在线程内部自己处理
        /// </summary>
        public static void Show1()
        {
            Console.WriteLine($"Show1 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            List<Task> taskList = new List<Task>();
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    string name = $"线程{i}";
                    taskList.Add(Task.Run(() =>
                    {
                        Thread.Sleep(1000);
                        if (name == "线程4")
                            throw new Exception($"{name}执行失败...{Thread.CurrentThread.ManagedThreadId}");
                        Console.WriteLine($"{name}执行成功...{Thread.CurrentThread.ManagedThreadId}");
                    }));
                }
                Task.WaitAll(taskList.ToArray());// 等待所有线程执行结束
            }
            catch (AggregateException ex)
            {
                foreach (var item in ex.InnerExceptions)
                {
                    Console.WriteLine(item.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"Show1 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// 线程取消
        /// </summary>
        public static void Show2()
        {
            Console.WriteLine($"Show2 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            try
            {
                List<Task> tasks = new ();
                CancellationTokenSource cts = new CancellationTokenSource();
                for (int i = 0; i < 20; i++)
                {
                    int k = i;
                    tasks.Add(Task.Run(() =>
                    {
                        try
                        {
                            Thread.Sleep(1000);
                            if (k == 5)
                                throw new Exception($"任务{k}取消...{Thread.CurrentThread.ManagedThreadId}");
                            if (cts.IsCancellationRequested)
                            {
                                Console.WriteLine($"任务{k}取消，放弃继续执行...{Thread.CurrentThread.ManagedThreadId}");
                                return;
                            }
                            Console.WriteLine($"任务{k}执行成功...{Thread.CurrentThread.ManagedThreadId}");
                        }
                        catch (Exception ex)
                        {
                            cts.Cancel();
                            Console.WriteLine(ex.Message);
                        }

                    }, cts.Token));
                }
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException aex)
            {
                foreach (var ex in aex.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine($"Show2 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// 线程临时变量
        /// </summary>
        public static void Show3()
        {
            #region 打印i是相同的
            // 全程只有一个i，所以打印出来的i是相同的
            // 而且单纯的for循环速度极快，而线程的启动需要申请资源等操作，这些操作完成后for循环早就结束了。所以i变为了5
            {
                //Console.WriteLine($"Show3-1 开始执行...{Thread.CurrentThread.ManagedThreadId}");
                //for (int i = 0; i < 5; i++)
                //{
                //    Task.Run(() =>
                //    {
                //        Console.WriteLine(i);
                //    });
                //}
                //Console.WriteLine($"Show3-1 结束执行...{Thread.CurrentThread.ManagedThreadId}");
            }
            #endregion
            #region 引入变量
            {
                Console.WriteLine($"Show3-2 开始执行...{Thread.CurrentThread.ManagedThreadId}");
                for (int i = 0; i < 5; i++)
                {
                    int k = i;
                    Task.Run(() =>
                    {
                        Console.WriteLine(k);
                    });
                }
                Console.WriteLine($"Show3-2 结束执行...{Thread.CurrentThread.ManagedThreadId}");
            }
            #endregion
        }
    }
}
