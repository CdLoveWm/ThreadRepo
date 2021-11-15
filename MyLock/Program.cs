using System;
using System.Threading.Tasks;

namespace MyLock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyLockTest.TestLock();
            Console.ReadKey();
        }
    }
    /// <summary>
    /// 锁
    /// </summary>
    public class MyLockTest
    {
        private static readonly object _lock = new object();
        private static int i = 0;
        public static void TestLock()
        {
            Task.Run(()=> MyLockTest.Show1());
            Task.Run(()=> MyLockTest.Show1());
            Task.Run(()=> MyLockTest.Show1());
            Task.Run(()=> MyLockTest.Show1());
            Task.Run(()=> MyLockTest.Show1());
            Task.Run(()=> MyLockTest.Show1());
            Task.Run(()=> MyLockTest.Show1());
            Task.Run(()=> MyLockTest.Show1());
            Task.Run(()=> MyLockTest.Show1());
            Task.Run(()=> MyLockTest.Show1());
        }

        public static void Show1()
        {
            //lock (_lock)
            //{
                Console.WriteLine(++i);
            //}
        }

    }
}
