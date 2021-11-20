using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyLock
{
    /// <summary>
    /// 读写锁
    /// </summary>
    public class MyReadWriteLock
    {
        private readonly static ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();
        private static Dictionary<string, string> dics = new Dictionary<string, string>();
        /// <summary>
        /// 读取
        /// </summary>
        public static string Read(string key)
        {
            try
            {
                readerWriterLock.EnterReadLock(); // 获取读锁
                if (dics.TryGetValue(key, out string value))
                    return value;
                return "";
            }
            finally
            {
                readerWriterLock.ExitReadLock(); // 释放读锁
            }
        }
        /// <summary>
        /// 写入，同一个时间只能有一个线程进入
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Write(string key, string value)
        {
            try
            {
                readerWriterLock.EnterWriteLock(); // 获取写锁
                if (dics.ContainsKey(key))
                {
                    Console.WriteLine("key已经存在");
                    return;
                }
                dics.Add(key, value);
            }
            finally
            {
                readerWriterLock.ExitWriteLock(); // 释放写锁
            }

           
        }
    }
}
