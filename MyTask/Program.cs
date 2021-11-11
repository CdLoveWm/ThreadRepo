using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TaskTest.Show1();
            //TaskTest.Show2();
            //TaskTest.Show3();
            //TaskTest.Show4();
            //TaskTest.Show5();
            //TaskTest.Show6();
            //TaskTest.Show7();
            //TaskTest.Show8();
            TaskTest.Show9();


            Console.ReadKey();
        }
    }

    /// <summary>
    /// Task的使用
    /// </summary>
    public class TaskTest
    {
        /// <summary>
        /// Task启动线程
        /// </summary>
        public static void Show1()
        {
            Console.WriteLine($"Show1 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            // 第一种
            Task.Run(() =>
            {
                Console.WriteLine($"1线程运行中...{Thread.CurrentThread.ManagedThreadId}");
            });
            // 第二种
            Task task = new Task(() =>
            {
                Console.WriteLine($"2线程运行中...{Thread.CurrentThread.ManagedThreadId}");
            });
            task.Start();

            Console.WriteLine($"Show1 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// TaskFactory 启动线程
        /// </summary>
        public static void Show2()
        {
            Console.WriteLine($"Show2 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            TaskFactory taskFactory = new TaskFactory();
            taskFactory.StartNew(() =>
            {
                Console.WriteLine($"线程运行中...{Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine($"Show2 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// Task线程等待 
        /// WaitAll WaitAny
        /// </summary>
        public static void Show3()
        {
            Console.WriteLine($"Show3 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            List<Task> tasks = new List<Task>();
            // 第一种
            tasks.Add(Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"1线程运行中...{Thread.CurrentThread.ManagedThreadId}");
            }));
            tasks.Add(Task.Run(() =>
            {
                Thread.Sleep(500);
                Console.WriteLine($"2线程运行中...{Thread.CurrentThread.ManagedThreadId}");
            }));
            //Task.WaitAll(tasks.ToArray()); // 等待所有线程结束才往下执行
            //Task.WaitAll(tasks.ToArray(), 2000); // 等待所有线程结束才往下执行，最多等待2000ms
            int index = Task.WaitAny(tasks.ToArray()); // 等待任意一个线程结束就可以往下执行
            Console.WriteLine($"线程{index + 1}先执行完成");
            //Task.WaitAny(tasks.ToArray(), 2000); // 等待任意一个线程结束就可以往下执行，最多等待2000ms
            Console.WriteLine($"Show3 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        // <summary>
        /// Task线程等待 
        /// WhenAll WhenAny ContinueWith
        /// </summary>
        public static void Show4()
        {
            Console.WriteLine($"Show4 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            List<Task> tasks = new List<Task>();
            // 第一种
            tasks.Add(Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"1线程运行中...{Thread.CurrentThread.ManagedThreadId}");
            }));
            tasks.Add(Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"2线程运行中...{Thread.CurrentThread.ManagedThreadId}");
            }));

            // 当所有线程执行完成后，再开始执行后面定义的动作
            //Task.WhenAll(tasks).ContinueWith(t =>
            //{
            //    Console.WriteLine($"所有子线程结束后，开始执行...{Thread.CurrentThread.ManagedThreadId}");
            //});

            // 当任意一个线程执行完成后，再开始执行后面定义的动作
            Task.WhenAny(tasks).ContinueWith(t =>
            {
                Console.WriteLine($"任意一个线程结束后，开始执行...{Thread.CurrentThread.ManagedThreadId}");
            });

            Console.WriteLine($"Show4 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// Task.run()也可以使用ContinueWith
        /// </summary>
        public static void Show5()
        {
            Console.WriteLine($"Show5 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            // 第一种
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"1线程运行中...{Thread.CurrentThread.ManagedThreadId}");
            }).ContinueWith(t =>
            {
                Console.WriteLine($"1线程执行完成后的后续动作...{Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine($"Show5 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }

        /// <summary>
        /// TaskFactory 线程等待 
        /// ContinueWhenAll ContinueWhenAny 
        /// </summary>
        public static void Show6()
        {
            Console.WriteLine($"Show6 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            List<Task> tasks = new List<Task>();
            TaskFactory factory = new TaskFactory();
            // 第一种
            tasks.Add(factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"1线程运行中...{Thread.CurrentThread.ManagedThreadId}");
            }));
            tasks.Add(factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"2线程运行中...{Thread.CurrentThread.ManagedThreadId}");
            }));

            // 当所有线程执行完成后，再开始执行后面定义的动作
            //factory.ContinueWhenAll(tasks.ToArray(), ts =>
            //{
            //    Console.WriteLine($"所有子线程结束后，开始执行...{Thread.CurrentThread.ManagedThreadId}");
            //});
            // 当任意一个线程执行完成后，再开始执行后面定义的动作
            factory.ContinueWhenAny(tasks.ToArray(), ts =>
            {
                Console.WriteLine($"任意一个线程结束后，开始执行...{Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine($"Show6 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// Task.Delay & Thead.Sleep
        /// </summary>
        public static void Show7()
        {
            // Task.Delay表示延迟，不阻塞当前线程
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Task.Delay(1000);
                stopwatch.Start();
                Console.WriteLine($"Task.Delay 运行时间：{stopwatch.ElapsedMilliseconds}毫秒");
            }
            Console.WriteLine("*************************");
            // Thread.Sleep表示等待，阻塞当前线程
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Thread.Sleep(1000);
                stopwatch.Start();
                Console.WriteLine($"Thread.Sleep 运行时间：{stopwatch.ElapsedMilliseconds}毫秒");
            }
            Console.WriteLine("*************************");
            // Task.Delay延迟1000毫秒后打印输出，与ContinueWith一起使用
            {
                Console.WriteLine($"主线程开始执行...{Thread.CurrentThread.ManagedThreadId}");
                Task.Delay(1000).ContinueWith(t =>
                {
                    Console.WriteLine($"延迟1000毫秒后打印...{Thread.CurrentThread.ManagedThreadId}");
                });
                Console.WriteLine($"主线程结束执行...{Thread.CurrentThread.ManagedThreadId}");

            }
        }
        /// <summary>
        /// TaskFactory ContinueWhenAny 识别是哪个线程先完成
        /// </summary>
        public static void Show8()
        {
            Console.WriteLine($"Show8 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            List<Task> tasks = new List<Task>();
            TaskFactory factory = new TaskFactory();
            // 第一种
            tasks.Add(factory.StartNew(o =>
            {
                Thread.Sleep(1100);
                Console.WriteLine($"1线程运行中...{Thread.CurrentThread.ManagedThreadId}");
            }, "第一个线程"));
            tasks.Add(factory.StartNew(o =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"2线程运行中...{Thread.CurrentThread.ManagedThreadId}");
            }, "第二个线程"));
            // 当任意一个线程执行完成后，再开始执行后面定义的动作
            factory.ContinueWhenAny(tasks.ToArray(), t =>
            {
                Console.WriteLine($"任意一个线程结束后，开始执行...{Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"第一个完成的线程是：{t.AsyncState}");
            });
            Console.WriteLine($"Show8 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// Task手动控制线程数量
        /// 完成100个任务，控制最多十个线程在运行
        /// </summary>
        public static void Show9()
        {
            Console.WriteLine($"Show9 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                int k = i; // 这里需要这样赋值使用，具体原因后面章节讨论
                tasks.Add(Task.Run(() =>
                {
                    Thread.Sleep(300);
                    Console.WriteLine($"第{k}个线程运行中...{Thread.CurrentThread.ManagedThreadId}");
                }));
                if (tasks.Count > 9)
                {
                    Task.WaitAny(tasks.ToArray());
                    tasks = tasks.Where(it => it.Status != TaskStatus.RanToCompletion).ToList();
                    Console.WriteLine($"去掉一个线程，tasks里面的个数：{tasks.Count}");
                }
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Show9 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
