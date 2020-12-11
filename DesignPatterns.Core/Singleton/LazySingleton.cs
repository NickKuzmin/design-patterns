namespace DesignPatterns.Core.Singleton
{
    class LazySingleton
    {
        private static readonly System.Lazy<LazySingleton> _lazy = new System.Lazy<LazySingleton>(() => new LazySingleton());

        public static LazySingleton Instance
        {
            get
            {
                // high-performance loading
                return _lazy.Value;
            }
        }
    }

    public class LazySingletonClient
    {
        public void Run()
        {
            var instance = LazySingleton.Instance;
        }
    }
}
