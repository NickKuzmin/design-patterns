using System;

namespace DesignPatterns.Core.FactoryMethod
{
    abstract class Exporter
    {
        public abstract void Export();
    }

    class XmlExporter : Exporter
    {
        public override void Export()
        { }
    }

    class PdfExporter : Exporter
    {
        public override void Export()
        { }
    }

    abstract class ExporterCreator
    {
        public abstract Exporter Create();
    }

    class PdfExporterCreator : ExporterCreator
    {
        public override Exporter Create() { return new PdfExporter(); }
    }

    class XmlExporterCreator : ExporterCreator
    {
        public override Exporter Create() { return new XmlExporter(); }
    }

    class Client
    {
        public void Run(string type)
        {
            ExporterCreator creator;
            if (type == "Pdf")
            {
                creator = new PdfExporterCreator();
            }
            else if (type == "Xml")
            {
                creator = new XmlExporterCreator();
            }
            else
                throw new ArgumentException();

            Export(creator);
        }

        private void Export(ExporterCreator creator)
        {
            var exporter = creator.Create();
            exporter.Export();
        }
    }
}
