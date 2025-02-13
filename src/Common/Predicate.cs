using System;

namespace StatementIQ
{
    public static class Predicate
    {
        public static void TryCache(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch
            {
                // Ignored
            }
        }

        public static TReturn TryCache<TReturn>(Func<TReturn> func)
        {
            try
            {
                return func();
            }
            catch
            {
                return default;
            }
        }

        public static TReturn TryCache<TReturn>(Func<TReturn> func, Func<TReturn> onError)
        {
            try
            {
                return func();
            }
            catch
            {
                return onError();
            }
        }
    }
}