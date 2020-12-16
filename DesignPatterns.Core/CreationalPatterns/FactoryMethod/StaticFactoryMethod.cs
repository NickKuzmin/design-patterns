namespace DesignPatterns.Core.FactoryMethod
{
    class StaticFactoryMethod
    {
        private StaticFactoryMethod()
        { }

        public StaticFactoryMethod Create()
        {
            return new StaticFactoryMethod();
        }
    }
}
