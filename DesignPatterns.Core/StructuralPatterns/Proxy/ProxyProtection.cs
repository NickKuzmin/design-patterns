using System;

namespace DesignPatterns.Core.StructuralPatterns.Proxy
{
    public interface ICar
    {
        void Drive();
    }

    public class Car : ICar
    {
        public void Drive()
        {
            Console.WriteLine("Car being driven");
        }
    }

    public class CarProxy : ICar
    {
        private readonly Car _car = new Car();
        private readonly Driver _driver;

        public CarProxy(Driver driver)
        {
            this._driver = driver;
        }

        public void Drive()
        {
            if (_driver.Age >= 16)
                _car.Drive();
            else
            {
                Console.WriteLine("Driver too young");
            }
        }
    }

    public class Driver
    {
        public int Age { get; set; }

        public Driver(int age)
        {
            Age = age;
        }
    }

    public class ProxyProtectionDemo
    {
        static void Main(string[] args)
        {
            ICar car = new CarProxy(new Driver(12)); // 22
            car.Drive();
        }
    }
}
