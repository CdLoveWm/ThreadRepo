using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyInterlocked
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //MyInterlocked.Show1();
            MyInterlocked.Show2();

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 原子操作
    /// </summary>
    public class MyInterlocked
    {
        /// <summary>
        /// 原子操作API
        /// </summary>
        public static void Show1()
        {
            int a = 0, b;
            // 原子递增操作，改变值本身，返回递增后的值
            b = Interlocked.Increment(ref a);
            Console.WriteLine($"Interlocked.Increment---原值a：{a}，结果b：{b}");
            // 原子递减操作，改变值本身，返回递减后的值
            b = Interlocked.Decrement(ref a);
            Console.WriteLine($"Interlocked.Decrement---原值a：{a}，结果b：{b}");
            // 对两个数进行求和并用和替换第一个整数，上述操作作为一个原子操作完成。返回求和后的结果
            b = Interlocked.Add(ref a, 2);
            Console.WriteLine($"Interlocked.Add---原值a：{a}，结果b：{b}");
            b = Interlocked.Add(ref a, -2);
            Console.WriteLine($"Interlocked.Add---原值a：{a}，结果b：{b}");
            // 将原值替换为第二个参数的值，并返回原值
            b = Interlocked.Exchange(ref a, 3);
            Console.WriteLine($"Interlocked.Exchange---原值a：{a}，结果b：{b}");

            // 将原值与第三个参数比较，相同则将原值替换为第二个参数，不相同则不替换，返回的永远是原来的值
            a = 3;
            b = Interlocked.CompareExchange(ref a, 5, 3);
            Console.WriteLine($"Interlocked.CompareExchange---原值a：{a}，结果b：{b}");
            a = 3;
            b = Interlocked.CompareExchange(ref a, 5, 4);
            Console.WriteLine($"Interlocked.CompareExchange---原值a：{a}，结果b：{b}");

        }

        #region CompareExchange实现无锁算法

        public static void Show2()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(Print));
            tasks.Add(Task.Run(Print));
            tasks.Add(Task.Run(Print));
            tasks.Add(Task.Run(Print));
            tasks.Add(Task.Run(Print));
            tasks.Add(Task.Run(Print));
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"最终结果 -- Counter.CounterA：{counter1.TimesA}, Counter.CounterB：{counter1.TimesB}");
        }

        private static Counter counter1 = new Counter();
        /// <summary>
        /// 打印结果
        /// </summary>
        private static void Print()
        {
            var result = Increment(ref counter1);
            Console.WriteLine($"Counter.CounterA：{result.TimesA}, Counter.CounterB：{result.TimesB}");
        }
        /// <summary>
        /// Counter自增，返回自增后的值
        /// </summary>
        /// <param name="counter"></param>
        private static Counter Increment(ref Counter counter)
        {
            Counter oldCounter, newCounter;
            do
            {
                oldCounter = counter;
                newCounter = new Counter() { TimesA = oldCounter.TimesA + 1, TimesB = oldCounter.TimesB + 2 };

            } 
            while (Interlocked.CompareExchange(ref counter, newCounter, oldCounter) != oldCounter);
            
            return newCounter;
        }

        class Counter
        {
            public int TimesA { get; set; }
            public int TimesB { get; set; }
        }

        #endregion

    }
}
