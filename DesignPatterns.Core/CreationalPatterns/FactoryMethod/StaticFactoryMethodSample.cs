namespace DesignPatterns.Core.FactoryMethod
{
    class Timespan
    {
        private Timespan(double value)
        { }

        public Timespan FromSeconds(double value)
        {
            return new Timespan(value * 1000);
        }

        public Timespan FromMilliseconds(double value)
        {
            return new Timespan(value * 100);
        }
    }
}
