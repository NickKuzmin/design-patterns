namespace DesignPatterns.Core.Singleton
{
    class Singleton
    {
        private static Singleton _instance;

        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Singleton();

                return _instance;
            }
        }
    }

    public class SingletonClient
    {
        public void Run()
        {
            var instance = Singleton.Instance;
        }
    }
}
