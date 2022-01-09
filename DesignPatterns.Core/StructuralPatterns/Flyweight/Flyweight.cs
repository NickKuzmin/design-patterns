using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Core.StructuralPatterns.Flyweight
{
    public class User
    {
        public string FullName { get; }

        public User(string fullName)
        {
            FullName = fullName;
        }
    }

    public class User2
    {
        static readonly List<string> Strings = new List<string>();
        private readonly int[] _names;

        public User2(string fullName)
        {
            int GetOrAdd(string s)
            {
                var idx = Strings.IndexOf(s);
                if (idx != -1)
                    return idx;

                Strings.Add(s);
                return Strings.Count - 1;
            }

            _names = fullName.Split(' ').Select(GetOrAdd).ToArray();
        }

        public string FullName => string.Join(" ", _names.Select(i => Strings[i]));
    }

   // [TestFixture]
    public class Demo
    {
        static void Main(string[] args)
        {

        }

        public void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public static string RandomString()
        {
            Random rand = new Random();
            return new string(
              Enumerable.Range(0, 10).Select(i => (char)('a' + rand.Next(26))).ToArray());
        }

        //[Test]
        public void TestUser()
        {
            var users = new List<User>();

            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            foreach (var firstName in firstNames)
                foreach (var lastName in lastNames)
                    users.Add(new User($"{firstName} {lastName}"));

            ForceGC();

            // dotMemory.Check(memory =>
            // {
            //     WriteLine(memory.SizeInBytes);
            // });
        }

        //[Test]
        public void TestUser2()
        {
            var users = new List<User2>();

            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            foreach (var firstName in firstNames)
                foreach (var lastName in lastNames)
                    users.Add(new User2($"{firstName} {lastName}"));

            ForceGC();

            // dotMemory.Check(memory =>
            // {
            //     WriteLine(memory.SizeInBytes);
            // });
        }
    }
}
