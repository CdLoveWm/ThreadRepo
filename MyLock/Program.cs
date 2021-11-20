using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyLock
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region 自旋锁
            // 使用Interlocked实现自旋锁
            //TestLock(MySpinLock.SpinLock1);
            //Console.WriteLine(MySpinLock.Count1);
            //Console.WriteLine(MySpinLock.Count2);

            // 自带的自旋锁
            //TestLock(MySpinLock.SpinLock2);
            //Console.WriteLine(MySpinLock.Count1);
            //Console.WriteLine(MySpinLock.Count2);
            #endregion

            #region 互斥锁

            //TestLock(MyMutex.Show);
            //Console.WriteLine(MyMutex.Count1);
            //Console.WriteLine(MyMutex.Count2);

            // 防止程序多开
            //MyMutex.Show2();

            #endregion

            Console.ReadKey();
        }

        public static void TestLock(Action action)
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 200; i++)
            {
                tasks.Add(Task.Run(action));
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}
