using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Core.StructuralPatterns.Proxy
{
    public class Property<T> where T : new()
    {
        private T _value;
        private readonly string _name;

        public T Value
        {
            get => _value;

            set
            {
                if (Equals(this._value, value)) return;
                Console.WriteLine($"Assigning {value} to {_name}");
                this._value = value;
            }
        }

        public Property(T value, string name = "")
        {
            _value = value;
            _name = name;
        }

        public static implicit operator T(Property<T> property)
        {
            return property.Value; // int n = p_int;
        }

        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value); // Property<int> p = 123;
        }

        protected bool Equals(Property<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_value, other._value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Property<T>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(_value);
        }
    }

    public class Creature
    {
        public readonly Property<int> agility
            = new Property<int>(10, nameof(Agility));

        public int Agility
        {
            get => agility.Value;
            set => agility.Value = value;
        }
    }

    public class PropertyProxyDemo
    {
        static void Main(string[] args)
        {
            var c = new Creature
            {
                Agility = 12 // c.set_agility()
            };
            // c.Agility = new Property<int>(10);
            //c.agility = 12;
        }
    }
}
