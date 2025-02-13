using System;

namespace StatementIQ
{
    public abstract class SingletonBase
    {
        protected static readonly object SyncLock = new object();
    }

    public class Singleton<T> : SingletonBase where T : class
    {
        private static T _instance;

        protected Singleton()
        {
        }

        public static T Instance
        {
            get
            {
                // Support multithreaded applications through double checking locking
                if (_instance != null) return _instance;

                lock (SyncLock)
                {
                    _instance ??= (T) Activator.CreateInstance(typeof(T), true);
                }

                // Boxing and unboxing to avoid code smell
                return _instance as T;
            }
        }
    }
}