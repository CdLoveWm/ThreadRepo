using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MyDelegate
{
    /// <summary>
    /// 委托
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            //DeclareDelegate declare = new DeclareDelegate();
            //declare.Show();

            //MultiDelegate multi = new MultiDelegate();
            //multi.Show();

            //GenericityDelegate genericityDelegate = new GenericityDelegate();
            //genericityDelegate.Show();

            //ActionDelegate.Show();

            //AsyncDelegate.Show1();
            //AsyncDelegate.Show2();
            //AsyncDelegate.Show3();
            //AsyncDelegate.Show4();
            //AsyncDelegate.Show5();
            //AsyncDelegate.Show6();
            //AsyncDelegate.Show7();
            AsyncDelegate.Show8();

            Console.ReadKey();
        }

    }


    /// <summary>
    /// 声明委托
    /// </summary>
    public class DeclareDelegate
    {
        // 声明委托
        public delegate int delegate1(string str);
        public delegate void delegate2(string str, int num);
        /// <summary>
        /// 实例化和调用
        /// </summary>
        public void Show()
        {
            delegate1 d1 = new delegate1(ConvertToInt);
            // 调用方式1
            d1("12");
            // 调用方式2
            d1.Invoke("23a");
        }
        /// <summary>
        /// 对应的相同返回值、相同参数的方法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int ConvertToInt(string str)
        {
            int result = 0;
            int.TryParse(str, out result);
            Console.WriteLine($"result is {result}");
            return result;
        }
    }

    /// <summary>
    /// 多播委托
    /// </summary>
    public class MultiDelegate
    {
        public delegate string delegate2(int num);
        delegate2 dgt2;
        public void Show()
        {
            // 1、先实例化委托进行多播绑定调用
            delegate2 dgt1 = new delegate2(ConvertToString);
            dgt1 += ConvertToString2;
            dgt1 += ConvertToString2;
            dgt1 += ConvertToString3;
            dgt1 += ConvertToString3;
            dgt1 -= ConvertToString3;
            dgt1(231);
            Console.WriteLine("*****************************");
            // 2、对于未赋值的dgt2也可以直接添加移除操作

            dgt2 += ConvertToString;
            dgt2 += ConvertToString2;
            dgt2 += ConvertToString3;
            dgt2(456);
        }

        public string ConvertToString(int num)
        {
            var res = num.ToString();
            Console.WriteLine(res);
            return res;
        }
        public string ConvertToString2(int num)
        {
            var res = $"##-{num.ToString()}";
            Console.WriteLine(res);
            return res;
        }
        public string ConvertToString3(int num)
        {
            var res = $"**-{num.ToString()}";
            Console.WriteLine(res);
            return res;
        }
    }
    /// <summary>
    /// 泛型委托
    /// </summary>
    public class GenericityDelegate
    {
        public delegate void delegate3<T>(T arg);

        public void Show()
        {
            delegate3<string> delegate3 = new delegate3<string>(Print);
            delegate3("qwer");
        }
        public void Print(string arg)
        {
            Console.WriteLine(arg);
        }
    }

    /// <summary>
    /// 内置Action委托
    /// </summary>
    public class ActionDelegate
    {
        public static void Show()
        {
            Action action = () =>
            {
                Console.WriteLine("action");
            };
            action.Invoke();

            Action<string> action1 = str =>
            {
                Console.WriteLine($"input string is {str}");
            };
            action1("ppap");
        }
    }
    /// <summary>
    /// 内置Func委托
    /// </summary>
    public class FuncDelegate
    {
        public static void Show()
        {
            Func<string> func = () =>
            {
                Console.WriteLine("func");
                return "";
            };

            Func<int, string> func1 = num =>
            {
                Console.WriteLine(num);
                return num.ToString();
            };
        }

    }

    /// <summary>
    /// 委托异步
    /// </summary>
    public class AsyncDelegate
    {
        /// <summary>
        /// 演示异步执行
        /// </summary>
        public static void Show1()
        {
            Console.WriteLine($"show1方法开始执行, 线程ID：{Thread.CurrentThread.ManagedThreadId.ToString("00")}，{DateTime.Now.ToString("HH: mm:ss.fff")}");
            Action<int> action = num =>
            {
                Console.WriteLine($"Actions开始执行, 线程ID：{Thread.CurrentThread.ManagedThreadId.ToString("00")}，{DateTime.Now.ToString("HH:mm:ss.fff")}");
            };
            action.BeginInvoke(12, null, null);
            Console.WriteLine($"show1方法结束执行, 线程ID：{Thread.CurrentThread.ManagedThreadId.ToString("00")}，{DateTime.Now.ToString("HH: mm:ss.fff")}");
        }
        /// <summary>
        /// BeginInvoke传值
        /// </summary>
        public static void Show2()
        {
            Action<string> action = str =>
            {
                Console.WriteLine($"input string is {str}");
            };
            action.BeginInvoke("abc", null, null);
        }
        /// <summary>
        /// BeginInvoke等待
        /// </summary>
        public static void Show3()
        {
            Console.WriteLine("show3开始执行...");
            Action action = () =>
            {
                Console.WriteLine("action开始执行...");
            };

            IAsyncResult result = action.BeginInvoke(null, null);
            result.AsyncWaitHandle.WaitOne(100000000);
            Console.WriteLine("show3结束执行");
        }
        /// <summary>
        /// 判断执行是否完毕
        /// </summary>
        public static void Show4()
        {
            Action action = () =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("Action执行中...");
            };
            IAsyncResult result = action.BeginInvoke(null, null);
            while (!result.IsCompleted)
            {
                Console.WriteLine("等待Action执行完毕中...");
                Thread.Sleep(600);
            }
            Console.WriteLine("Action执行完毕...");
        }
        /// <summary>
        /// 回调
        /// </summary>
        public static void Show5()
        {
            Console.WriteLine("Show5开始执行...");
            Action action = () =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("Action执行中...");
            };
            IAsyncResult result = action.BeginInvoke(res =>
            {
                Console.WriteLine("回调函数执行中...");
            }, null);
            Console.WriteLine("Show5结束执行...");
        }
        /// <summary>
        /// 回调 -- 获取BeginInvoke的参数
        /// </summary>
        public static void Show6()
        {
            Console.WriteLine("Show6开始执行...");
            Func<string, string> func = str =>
            {
                Console.WriteLine("Action执行中...");
                return "func1Result";
            };
            IAsyncResult result = func.BeginInvoke("inputpara", res =>
            {
                Console.WriteLine($"回调函数执行中...，BeginInvoke最后一个参数为：{res.AsyncState}");

            }, "参数3");
            Console.WriteLine("Show6结束执行...");
        }
        /// <summary>
        /// EndInvoke 阻塞
        /// </summary>
        public static void Show7()
        {
            Console.WriteLine("Show7开始执行...");
            Func<string, string> func = str =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Action执行中...");
                return "func1Result";
            };
            IAsyncResult result = func.BeginInvoke("inputpara", res =>
            {
                Console.WriteLine($"回调函数执行中...");

            }, null);
            string funcRes = func.EndInvoke(result);
            Console.WriteLine($"func 执行返回结果为：{funcRes}");
            Console.WriteLine("Show7结束执行...");
        }
        /// <summary>
        /// 回调--获取返回值
        /// </summary>
        public static void Show8()
        {
            Console.WriteLine("Show8 开始执行...");
            Func<string, string> func = str =>
            {
                Console.WriteLine("Action执行中...");
                return "func1Result";
            };
            IAsyncResult result = func.BeginInvoke("inputpara", res =>
            {
                Console.WriteLine($"回调函数执行中...");
                // 获取委托的返回值
                var retureRes = func.EndInvoke(res);
                Console.WriteLine($"获取到委托的返回值：{retureRes}");

            }, null);
            //string funcRes = func.EndInvoke(result);
            //Console.WriteLine($"func 执行返回结果为：{funcRes}");
            Console.WriteLine("Show8 结束执行...");
        }
    }

}
