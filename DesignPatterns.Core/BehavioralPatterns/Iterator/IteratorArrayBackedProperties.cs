using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Core.BehavioralPatterns.Iterator
{
    public class Creature : IEnumerable<int>
    {
        private readonly int[] _stats = new int[3];

        public IEnumerable<int> Stats => _stats;

        private const int strength = 0;

        public int Strength
        {
            get => _stats[strength];
            set => _stats[strength] = value;
        }

        public int Agility { get; set; }
        public int Intelligence { get; set; }

        public double AverageStat => _stats.Average();

        //public double AverageStat => SumOfStats / 3.0;

        //public double SumOfStats => Strength + Agility + Intelligence;
        public double SumOfStats => _stats.Sum();

        //public double MaxStat => Math.Max(
        //  Math.Max(Strength, Agility), Intelligence);

        public double MaxStat => _stats.Max();

        public IEnumerator<int> GetEnumerator()
        {
            return _stats.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int this[int index]
        {
            get => _stats[index];
            set => _stats[index] = value;
        }
    }

    public class IteratorArrayBackedPropertiesDemo
    {
        static void Main(string[] args)
        {
            var creature = new Creature();
            creature.Strength = 10;
            creature.Intelligence = 11;
            creature.Agility = 12;
            Console.WriteLine($"Creature has average stat = {creature.AverageStat}, " +
                              $"max stat = {creature.MaxStat}, " +
                              $"sum of stats = {creature.SumOfStats}.");
        }
    }
}
