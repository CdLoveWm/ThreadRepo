using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyAsyncAwait
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"主线程在main中开始执行...【{Thread.CurrentThread.ManagedThreadId}】");
            AsyncAwaitTest awaitTest = new AsyncAwaitTest();
            awaitTest.Show1();
            Console.WriteLine($"主线程在main中结束执行...【{Thread.CurrentThread.ManagedThreadId}】");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Async/Await 
    /// </summary>
    public class AsyncAwaitTest
    {
        /// <summary>
        /// async/await执行逻辑
        /// </summary>
        /// <returns></returns>
        public async Task Show1()
        {
            Console.WriteLine($"Show1 开始执行...【{Thread.CurrentThread.ManagedThreadId}】");
            
            await Task.Run(() => TimeConsumingMethod1());
            Console.WriteLine($"耗时方法1 执行之后...【{Thread.CurrentThread.ManagedThreadId}】");
            Console.WriteLine("------------------------");
            await Task.Run(() => TimeConsumingMethod2());
            Console.WriteLine($"耗时方法2 执行之后...【{Thread.CurrentThread.ManagedThreadId}】");
            Console.WriteLine("------------------------");
            await Task.Run(() => TimeConsumingMethod3());
            Console.WriteLine($"耗时方法3 执行之后...【{Thread.CurrentThread.ManagedThreadId}】");
            Console.WriteLine($"Show1 结束执行...【{Thread.CurrentThread.ManagedThreadId}】");
            Console.WriteLine("------------------------");
        }

        #region 耗时操作
        /// <summary>
        /// 耗时操作1
        /// </summary>
        private void TimeConsumingMethod1()
        {
            Thread.Sleep(1000);
            Console.WriteLine($"耗时方法1...【{Thread.CurrentThread.ManagedThreadId}】");
        }
        /// <summary>
        /// 耗时操作2
        /// </summary>
        private void TimeConsumingMethod2()
        {
            Thread.Sleep(1000);
            Console.WriteLine($"耗时方法2...【{Thread.CurrentThread.ManagedThreadId}】");
        }
        /// <summary>
        /// 耗时操作3
        /// </summary>
        private void TimeConsumingMethod3()
        {
            Thread.Sleep(1000);
            Console.WriteLine($"耗时方法3...【{Thread.CurrentThread.ManagedThreadId}】");
        }
        #endregion

        /// <summary>
        /// 返回空的方法，标记返回Task是为了使用await
        /// </summary>
        /// <returns></returns>
        public Task Show2()
        {
            Console.WriteLine("这是返回值void的方法");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 返回string的方法，泛型方法
        /// 标记返回Task是为了使用await
        /// </summary>
        /// <returns></returns>
        public Task<string> Show3()
        {
            Console.WriteLine("这个方法返回带返回值的Task");
            return Task.FromResult("我是返回值");
        }
        /// <summary>
        /// 这个方法带async标记
        /// 返回值直接返回即可
        /// </summary>
        /// <returns></returns>
        public async Task<string> Show4()
        {
            await Task.Run(() => Task.Delay(100));
            return "我是返回值";
        }
    }
}
