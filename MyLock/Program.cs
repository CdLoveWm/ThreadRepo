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

            #region 混合锁

            // Monitor实现
            //TestLock(MyMonitor.Show1);
            //Console.WriteLine(MyMonitor.Count1);
            //Console.WriteLine(MyMonitor.Count2);

            // Lock实现
            //TestLock(MyMonitor.Show2);
            //Console.WriteLine(MyMonitor.Count1);
            //Console.WriteLine(MyMonitor.Count2);

            #region Lock(this)测试

            {
                //List<Task> tasks = new List<Task>();
                //for (int i = 0; i < 200; i++)
                //{
                //    tasks.Add(Task.Run(() => {
                //        // 每次线程产生一个新的this实例，那这个this只会在当前线程有效，达不到预期效果
                //        MyMonitor obj = new MyMonitor();
                //        obj.Show3();
                //    }));
                //}
                //Task.WaitAll(tasks.ToArray());
                //Console.WriteLine(MyMonitor.Count1);
                //Console.WriteLine(MyMonitor.Count2);
            }
            {
                //List<Task> tasks = new List<Task>();
                //MyMonitor obj = new MyMonitor();
                //for (int i = 0; i < 200; i++)
                //{
                //    tasks.Add(Task.Run(() => {
                //        obj.Show3();
                //    }));
                //}
                //Task.WhenAll(tasks.ToArray()).ContinueWith(t => {
                //    Console.WriteLine(MyMonitor.Count1);
                //    Console.WriteLine(MyMonitor.Count2);
                //});
                //// 这里锁住obj，就相当于锁住Show3方法中的this，这个lock先执行，那么Show3的lLock(this)就会一直等待锁的释放
                //// 一直不释放，则程序就会一直卡住
                //lock (obj)
                //{
                //    Thread.Sleep(int.MaxValue);
                //}
            }

            #endregion

            #region Lock字符串

            //List<Task> tasks = new List<Task>();
            //MyMonitor obj = new MyMonitor();
            //for (int i = 0; i < 200; i++)
            //{
            //    tasks.Add(Task.Run(() =>
            //    {
            //        obj.Show4();
            //    }));
            //}
            //Task.WhenAll(tasks.ToArray()).ContinueWith(t =>
            //{
            //    Console.WriteLine(MyMonitor.Count1);
            //    Console.WriteLine(MyMonitor.Count2);
            //});
            //// 这里lock的字符串和Show4方法中lock的字符串相同，由于字符串的享元特性，两个地方其实是lock的同一个对象
            //// 这里一直不释放，那Show4就会一直等待
            //var _lockObj = "lockStr";
            //lock (_lockObj)
            //{
            //    Thread.Sleep(int.MaxValue);
            //}

            #endregion

            #region Lock强转的数字

            //TestLock(MyMonitor.Show5);
            //Console.WriteLine(MyMonitor.Count1);
            //Console.WriteLine(MyMonitor.Count2);

            #endregion

            #endregion

            #region 信号量

            //TestLock(MySemaphore.Show1);
            //Console.WriteLine(MySemaphore.Count1);
            //Console.WriteLine(MySemaphore.Count2);

            //// 防止程序多开
            //MySemaphore.Show2();

            #endregion

            #region 读写锁

            //for (int i = 0; i < 100; i++)
            //{
            //    MyReadWriteLock.Write(i.ToString(), $"值{i}");
            //}

            //while (true)
            //{
            //    for (int i = 0; i < 3; i++)
            //    {
            //        string value = MyReadWriteLock.Read(new Random().Next(100).ToString());
            //        Console.WriteLine($"获取到值： {value}");
            //    }
            //}

            #endregion

            #region 死锁

            //// 死锁
            //Task.Run(() => DeadLock.Show1());
            //Task.Run(() => DeadLock.Show2());

            //// Monitor避免死锁
            //Task.Run(() => DeadLock.Show3());
            //Task.Run(() => DeadLock.Show4());

            // Monitor 活锁
            Task.Run(() => DeadLock.Show5());
            Task.Run(() => DeadLock.Show6());

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
