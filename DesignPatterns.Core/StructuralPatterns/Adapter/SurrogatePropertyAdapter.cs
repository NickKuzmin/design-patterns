using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DesignPatterns.Core.StructuralPatterns.Adapter
{
    public class CountryStats
    {
        [XmlIgnore]
        public Dictionary<string, string> Capitals { get; set; }
            = new Dictionary<string, string>();

        public (string, string)[] CapitalsSerializable
        {
            get
            {
                return Capitals.Keys.Select(country =>
                    (country, Capitals[country])).ToArray();
            }
            set
            {
                Capitals = value.ToDictionary(x => x.Item1, x => x.Item2);
            }
        }
    }

    public class SurrogatePropertyAdapterDemo
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var stats = new CountryStats();
            stats.Capitals.Add("France", "Paris");
            var xs = new XmlSerializer(typeof(CountryStats));
            var stringBuilder = new StringBuilder();
            var stringWriter = new StringWriter(stringBuilder);
            xs.Serialize(stringWriter, stats);

            var newStats = (CountryStats)xs.Deserialize(
                new StringReader(stringWriter.ToString()));
            Console.WriteLine(newStats.Capitals["France"]);
        }
    }
}
