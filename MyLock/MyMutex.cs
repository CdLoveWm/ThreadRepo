using System;
using System.Collections.Generic;
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
    }
}
