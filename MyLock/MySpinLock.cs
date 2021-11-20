using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyLock
{
    /// <summary>
    /// 自旋锁
    /// </summary>
    public class MySpinLock
    {
        private static int _lock = 0; // 0为未获取锁，1表示锁已被获取
        public static int Count1 = 0;
        public static int Count2 = 0;
        /// <summary>
        /// 使用Interlocked实现自旋锁
        /// </summary>
        public static void SpinLock1()
        {
            // 当Exchange返回值不为0的时候，说明此时_lock值为1，表示锁正被占用，循环重试
            // 当Exchange返回值为0的时候，说明_lock值为0，即此时锁是空闲的。则往下执行锁保护区域
            while (Interlocked.Exchange(ref _lock, 1) != 0)
            {
                Thread.SpinWait(1);
            }
            // 锁保护区域
            {
                Count1++;
                Count2++;
            }
            // 释放锁
            Interlocked.Exchange(ref _lock, 0);
        }

        private static SpinLock spinLock = new SpinLock();
        /// <summary>
        /// 自带的自旋锁
        /// </summary>
        public static void SpinLock2()
        {
            bool lockTaken = false;
            try
            {
                spinLock.Enter(ref lockTaken);

                Count1++;
                Count2++;
            }
            finally
            {
                if(lockTaken)
                    spinLock.Exit();
            }
        }
    }
}
