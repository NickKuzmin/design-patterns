using System.Threading;

namespace DesignPatterns.Core.Singleton
{
    class ThreadSafeSingletonWithLock
    {
        private static ThreadSafeSingletonWithLock _instance;
        private static object _locker = new object();

        public static ThreadSafeSingletonWithLock Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                            _instance = new ThreadSafeSingletonWithLock();
                    }
                }

                return _instance;
            }
        }
    }

    public class ThreadSafeSingletonWithLockClient
    {
        public void Run()
        {
            foreach (var index in System.Linq.Enumerable.Range(0, 10))
            {
                var thread = new Thread(() =>
                {
                    var instance = ThreadSafeSingletonWithLock.Instance;
                });
                thread.Start();
            }
        }
    }
}
