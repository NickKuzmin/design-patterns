﻿using System;

namespace DesignPatterns.Core.StructuralPatterns.Decorator
{
    public interface ICreature
    {
        int Age { get; set; }
    }

    public interface IBird : ICreature
    {
        void Fly();
    }

    public interface ILizard : ICreature
    {
        void Crawl();
    }

    public class Bird : IBird
    {
        public int Age { get; set; }
        public void Fly()
        {
            if (Age >= 10)
                Console.WriteLine("I am flying!");
        }
    }

    public class Lizard : ILizard
    {
        public int Age { get; set; }
        public void Crawl()
        {
            if (Age < 10)
                Console.WriteLine("I am crawling!");
        }
    }

    public class Dragon : IBird, ILizard
    {
        private readonly IBird bird;
        private readonly ILizard lizard;

        public Dragon(IBird bird, ILizard lizard)
        {
            this.bird = bird;
            this.lizard = lizard;
        }

        public void Crawl()
        {
            lizard.Crawl();
        }

        public void Fly()
        {
            bird.Fly();
        }

        public int Age
        {
            get => bird.Age;
            set => bird.Age = lizard.Age = value;
        }
    }
}
