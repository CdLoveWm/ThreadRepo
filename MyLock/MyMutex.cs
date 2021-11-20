using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyLock
{
    /// <summary>
    /// 互斥锁
    /// </summary>
    public class MyMutex
    {
        private static Mutex mutex = new Mutex();
        public static int Count1 = 0;
        public static int Count2 = 0;
        /// <summary>
        /// 互斥锁演示
        /// </summary>
        public static void Show()
        {
            bool waitHandle = false;
            try
            {
                waitHandle = mutex.WaitOne(); // 获取锁

                #region 锁保护区域
                Count1++;
                Count2++;
                #endregion
            }
            finally
            {
                if(waitHandle)
                    mutex.ReleaseMutex(); // 释放锁
            }
        }

        /// <summary>
        /// 防止程序多开
        /// </summary>
        public static void Show2()
        {
            Mutex mutex = new Mutex(false, "", out bool flag);
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
