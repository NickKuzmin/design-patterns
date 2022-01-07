using System;

namespace DesignPatterns.Core.CreationalPatterns.Builder.BuilderFacets
{
    public class Person
    {
        public Person()
        {
            Console.WriteLine("Creating an instance of Person");
        }

        // address
        public string StreetAddress, Postcode, City;

        // employment info
        public string CompanyName, Position;

        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}," +
                   $" {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}," +
                   $" {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    public class PersonBuilder // facade 
    {
        // the object we're going to build
        protected Person Person;

        public PersonBuilder() => Person = new Person();

        protected PersonBuilder(Person person) => Person = person;

        // Инициализация билдеров с захватом экзмепляра Person
        public PersonAddressBuilder Lives => new PersonAddressBuilder(Person);
        public PersonJobBuilder Works => new PersonJobBuilder(Person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.Person;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person) : base(person) { }

        public PersonJobBuilder At(string companyName)
        {
            Person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            Person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int annualIncome)
        {
            Person.AnnualIncome = annualIncome;
            return this;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person) : base(person) { }

        public PersonAddressBuilder At(string streetAddress)
        {
            Person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postcode)
        {
            Person.Postcode = postcode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            Person.City = city;
            return this;
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();

            // BuilderFacet. Возможность использования с fluent API - нескольких билдеров
            Person person = pb
              .Lives
                .At("123 London Road")
                .In("London")
                .WithPostcode("SW12BC")
              .Works
                .At("Fabrikam")
                .AsA("Engineer")
                .Earning(123000);

            Console.WriteLine(person);
        }
    }
}
