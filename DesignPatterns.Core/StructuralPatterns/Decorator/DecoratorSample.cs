using System;

namespace DesignPatterns.Core.Structural_patterns.Decorator
{
    abstract class Car
    {
        public Car(string name)
        {
            Name = name;
        }

        public string Name { get; protected set; }

        public abstract int GetPrice();
    }

    class ElectricCar : Car
    {
        public ElectricCar() : base("Electric car")
        { }

        public override int GetPrice()
        {
            return 70000;
        }
    }

    class PetrolCar : Car
    {
        public PetrolCar() : base("Petrol car")
        { }

        public override int GetPrice()
        {
            return 25000;
        }
    }

    class DieselCar : Car
    {
        public DieselCar() : base("Diesel car")
        { }

        public override int GetPrice()
        {
            return 35000;
        }
    }

    abstract class CarDecorator : Car
    {
        protected Car Car;

        public CarDecorator(string n, Car car) : base(n)
        {
            Car = car;
        }
    }

    class FourWheelDriveCar : CarDecorator
    {
        public FourWheelDriveCar(Car car)
            : base(car.Name + ", с полным приводом", car)
        { }

        public override int GetPrice()
        {
            return Car.GetPrice() + 3000;
        }
    }

    class LongBaseCar : CarDecorator
    {
        public LongBaseCar(Car car)
            : base(car.Name + ", with long base", car)
        { }

        public override int GetPrice()
        {
            return Car.GetPrice() + 17000;
        }
    }

    class Client
    {
        public void Run()
        {
            Car electricCar = new ElectricCar();
            electricCar = new LongBaseCar(electricCar);
            Console.WriteLine("Name: {0}", electricCar.Name);
            Console.WriteLine("Price: {0}", electricCar.GetPrice());

            Car petrolCar = new PetrolCar();
            petrolCar = new FourWheelDriveCar(petrolCar);
            petrolCar = new LongBaseCar(petrolCar);
            Console.WriteLine("Name: {0}", petrolCar.Name);
            Console.WriteLine("Price: {0}", petrolCar.GetPrice());

            Car dieselCar = new DieselCar();
            dieselCar = new FourWheelDriveCar(dieselCar);
            Console.WriteLine("Name: {0}", petrolCar.Name);
            Console.WriteLine("Price: {0}", petrolCar.GetPrice());
        }
    }
}
