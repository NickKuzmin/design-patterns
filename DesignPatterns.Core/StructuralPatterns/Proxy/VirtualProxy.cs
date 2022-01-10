using System;

namespace DesignPatterns.Core.StructuralPatterns.Proxy
{
    interface IImage
    {
        void Draw();
    }

    class Bitmap : IImage
    {
        private readonly string _filename;

        public Bitmap(string filename)
        {
            this._filename = filename;
            Console.WriteLine($"Loading image from {filename}");
        }

        public void Draw()
        {
            Console.WriteLine($"Drawing image {_filename}");
        }
    }

    class LazyBitmap : IImage
    {
        private readonly string _filename;
        private Bitmap _bitmap;

        public LazyBitmap(string filename)
        {
            this._filename = filename;
        }


        public void Draw()
        {
            if (_bitmap == null)
                _bitmap = new Bitmap(_filename);

            _bitmap.Draw();
        }
    }

    class VirtualProxyDemo
    {
        public static void DrawImage(IImage img)
        {
            Console.WriteLine("About to draw the image");
            img.Draw();
            Console.WriteLine("Done drawing the image");

        }

        static void Main()
        {
            var img = new LazyBitmap("pokemon.png");
            DrawImage(img);
        }
    }
}
