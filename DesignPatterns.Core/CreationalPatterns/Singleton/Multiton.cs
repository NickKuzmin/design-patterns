using System;
using System.Collections.Generic;

namespace DesignPatterns.Core.CreationalPatterns.Singleton
{
    // can only allow two components related to subsystems
    enum Subsystem
    {
        Main,
        Backup
    }

    class Printer
    {
        private Printer()
        {
        }

        public static Printer Get(Subsystem ss)
        {
            if (Instances.ContainsKey(ss))
                return Instances[ss];

            var instance = new Printer();
            Instances[ss] = instance;
            return instance;
        }

        private static readonly Dictionary<Subsystem, Printer> Instances
            = new Dictionary<Subsystem, Printer>();
    }

    class MultitonDemo
    {
        public static void Main(string[] args)
        {
            var primary = Printer.Get(Subsystem.Main);
            var backup = Printer.Get(Subsystem.Backup);

            var backupAgain = Printer.Get(Subsystem.Backup);

            Console.WriteLine(ReferenceEquals(backup, backupAgain));
        }
    }
}
