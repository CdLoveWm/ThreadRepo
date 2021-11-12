using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyParallel
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //ParallelTest.Show1();
            //ParallelTest.Show2();
            ParallelTest.Show3();
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Parallel使用
    /// </summary>
    public class ParallelTest
    {
        /// <summary>
        /// 启动线程方式
        /// </summary>
        public static void Show1()
        {
            Console.WriteLine($"Show1 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            // 第一种，区间为 大于等于1且小于10
            Parallel.For(1, 10, i =>
            {
                Console.WriteLine($"第{i}个线程启动...{Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine("******************************");

            // 第二种
            IEnumerable<int> nums = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Parallel.ForEach(nums, num =>
            {
                Console.WriteLine($"第{num}个线程启动...{Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine($"Show1 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// 控制线程数量
        /// </summary>
        public static void Show2()
        {
            Console.WriteLine($"Show2 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            ParallelOptions options = new ParallelOptions();
            // 设置为最多三个线程，这控制只在当前Parallel循环中作用，不影响其他
            options.MaxDegreeOfParallelism = 3; 
            Parallel.For(1, 10, options, i =>
            {
                Console.WriteLine($"第{i}个线程启动...{Thread.CurrentThread.ManagedThreadId}");
            });

            Console.WriteLine($"Show2 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// Parallel Break Stop
        /// Break，Stop的功能虽然描述的是这样的， 但是往往用的时候却不尽人意
        /// 其实不管这两个API功能多么强大， 还是那句话线程是操作系统的资源，我们发出的停止命令，
        /// 并不能立即去执行。所以一般对于线程的停止等这些不靠谱的操作，能不用则不要用
        /// 这段代码也并不能达到Stop的效果
        /// </summary>
        public static void Show3()
        {
            Console.WriteLine($"Show3 开始执行...{Thread.CurrentThread.ManagedThreadId}");
            ParallelOptions options = new ParallelOptions();
            // 设置为最多三个线程，这控制只在当前Parallel循环中作用，不影响其他
            options.MaxDegreeOfParallelism = 2;
            Parallel.For(1, 10, options, (i, state) =>
            {
                DoSomeThing(i);
                //if (i == 2)
                //{
                //    Console.WriteLine($"Breack，结束当次Parallel...{Thread.CurrentThread.ManagedThreadId}");
                //    state.Break();
                //}
                if (i == 1)
                {
                    state.Stop();
                    Console.WriteLine($"Stop，结束Parallel循环...{Thread.CurrentThread.ManagedThreadId}");
                    return;
                }
            });
            Console.WriteLine($"Show3 结束执行...{Thread.CurrentThread.ManagedThreadId}");
        }

        private static void DoSomeThing(int i)
        {
            Console.WriteLine($"第{i}个线程启动...{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            Console.WriteLine($"第{i}个线程结束...{Thread.CurrentThread.ManagedThreadId}");

        }
    }
}
