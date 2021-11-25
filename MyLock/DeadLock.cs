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
    }
}
