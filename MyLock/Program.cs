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

            //TestLock(MySpinLock.SpinLock1);
            //Console.WriteLine(MySpinLock.Count1);
            //Console.WriteLine(MySpinLock.Count2);

            TestLock(MySpinLock.SpinLock2);
            Console.WriteLine(MySpinLock.Count1);
            Console.WriteLine(MySpinLock.Count2);


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
