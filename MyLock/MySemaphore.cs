using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyLock
{
    /// <summary>
    /// 信号量
    /// </summary>
    public class MySemaphore
    {
        private static Semaphore semaphore = new Semaphore(1, int.MaxValue);
        public static int Count1;
        public static int Count2;
        /// <summary>
        /// 信号量使用演示
        /// </summary>
        public static void Show1()
        {
            try
            {
                // 等待释放信号
                semaphore.WaitOne();

                #region 锁保护区域
                Count1++;
                Count2++;
                Console.WriteLine($"进来一个线程...{Count1}");
                #endregion
            }
            finally
            {
                // 释放一个信号量，等同于semaphore.Release(1);
                int a = semaphore.Release();
            }
        }
        /// <summary>
        /// 信号量防止程序多开
        /// </summary>
        public static void Show2()
        {
            Semaphore semaphore1 = new Semaphore(1, 5, "test", out bool flag);
            if (flag)
            {
                Console.WriteLine("程序正常运行");
            }
            else
            {
                Console.WriteLine("已有程序在运行，不能同时多开, 5秒后退出！");
                Task.Delay(5000).ContinueWith(t =>
                {
                    Environment.Exit(0);
                });
            }
        }

    }
}
