using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyLock
{
    /// <summary>
    /// 混合锁Monitor
    /// </summary>
    public class MyMonitor
    {
        private static readonly object _lockObj = new object();
        public static int Count1;
        public static int Count2;
        /// <summary>
        /// Monitor
        /// </summary>
        public static void Show1()
        {
            bool lockTaken = false;
            try
            {
                // Monitor是静态类
                Monitor.Enter(_lockObj, ref lockTaken); // 获取锁

                #region 锁保护区域
                Count1++;
                Count2++;
                #endregion
            }
            finally
            {
                if(lockTaken)
                    Monitor.Exit(_lockObj); // 释放锁
            }
        }
        /// <summary>
        /// Lock是Monitor的语法糖
        /// </summary>
        public static void Show2()
        {
            lock (_lockObj)
            {
                #region 锁保护区域
                Count1++;
                Count2++;
                #endregion
            }
        }

        /// <summary>
        /// lock(this)测试
        /// 每次线程产生一个新的this实例，那这个this只会在当前线程有效
        /// 如果在外部锁住this，一直不释放，就会造成线程获取不了锁
        /// </summary>
        public void Show3()
        {
            lock (this)
            {
                Count1++;
                Count2++;
            }
        }
        /// <summary>
        /// Lock(string)测试
        /// </summary>
        public void Show4()
        {
            lock ("lockStr")
            {
                Count1++;
                Count2++;
            }
        }

        private static int num = 5;
        /// <summary>
        /// Lock((object)number)测试
        /// lock 强转的数字测试
        /// </summary>
        public static void Show5()
        {
            lock ((object)num)
            {
                Count1++;
                Count2++;
            }
        }
    }
}
