using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyLock
{
    /// <summary>
    /// 死锁
    /// </summary>
    public class DeadLock
    {
        private static readonly object _lock1 = new object();
        private static readonly object _lock2 = new object();

        #region 产生死锁

        /// <summary>
        /// 首先锁住_lock1，等待_lock2释放再加锁
        /// </summary>
        public static void Show1()
        {
            Console.WriteLine("show1开始执行");
            lock (_lock1)
            {
                Console.WriteLine("1--->lock1...");
                Thread.Sleep(200);
                lock (_lock2)
                {
                    Console.WriteLine("1--->lock2...");
                }
            }
            Console.WriteLine("show1结束执行");
        }
        /// <summary>
        /// 首先锁住_lock2，等待_lock1释放再加锁
        /// </summary>
        public static void Show2()
        {
            Console.WriteLine("show2开始执行");
            lock (_lock2)
            {
                Console.WriteLine("2--->lock1...");
                Thread.Sleep(200);
                lock (_lock1)
                {
                    Console.WriteLine("2--->lock2...");
                }
            }
            Console.WriteLine("show2结束执行");
        }

        #endregion

        #region Monitor避免死锁

        public static void Show3()
        {
            Console.WriteLine("Show3 开始执行");
            bool lockTaken = false;
            while (!lockTaken)
            {
                lock (_lock1)
                {
                    Monitor.TryEnter(_lock2, 5000, ref lockTaken);
                    // 获取到锁
                    if (lockTaken)
                    {
                        // 锁保护区域
                        Monitor.Exit(_lock2);
                    }
                }
                // 没有获取到锁，让掉当前CPU执行权，让其他等待的线程去执行，过段时间恢复
                if (!lockTaken) Thread.Yield();
            }
            Console.WriteLine("Show3 结束执行");
        }
        public static void Show4()
        {
            Console.WriteLine("Show4 开始执行");
            lock (_lock2)
            {
                Console.WriteLine("2--->lock1...");
                Thread.Sleep(200);
                lock (_lock1)
                {
                    // 锁保护区域
                    Console.WriteLine("2--->lock2...");
                }
            }
            Console.WriteLine("Show4 结束执行");
        }
        #endregion

        #region 活锁

        public static void Show5()
        {
            Console.WriteLine("Show5 开始执行");
            bool lockTaken = false;
            while (!lockTaken)
            {
                lock (_lock1)
                {
                    Thread.Sleep(1000);
                    Monitor.TryEnter(_lock2, 0, ref lockTaken);
                    // 获取到锁
                    if (lockTaken)
                    {
                        // 锁保护区域
                        Monitor.Exit(_lock2);
                    }
                }
                // 没有获取到锁，让掉当前CPU执行权，让其他等待的线程去执行，过段时间恢复
                if (!lockTaken) Thread.Yield();
            }
            Console.WriteLine("Show5 结束执行");
        }
        public static void Show6()
        {
            Console.WriteLine("Show6 开始执行");
            bool lockTaken = false;
            while (!lockTaken)
            {
                lock (_lock2)
                {
                    Thread.Sleep(1100);
                    Monitor.TryEnter(_lock1, 0, ref lockTaken);
                    // 获取到锁
                    if (lockTaken)
                    {
                        // 锁保护区域
                        Monitor.Exit(_lock1);
                    }
                }
                // 没有获取到锁，让掉当前CPU执行权，让其他等待的线程去执行，过段时间恢复
                if (!lockTaken) Thread.Yield();
            }
            Console.WriteLine("Show6 结束执行");
        }

        #endregion
    }
}
