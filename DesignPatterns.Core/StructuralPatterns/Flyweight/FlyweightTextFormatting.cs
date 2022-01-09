using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Core.StructuralPatterns.Flyweight
{
    public class FormattedText
    {
        private readonly string _plainText;

        public FormattedText(string plainText)
        {
            this._plainText = plainText;
            _capitalize = new bool[plainText.Length];
        }

        public void Capitalize(int start, int end)
        {
            for (int i = start; i <= end; ++i)
                _capitalize[i] = true;
        }

        private readonly bool[] _capitalize;

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _plainText.Length; i++)
            {
                var c = _plainText[i];
                sb.Append(_capitalize[i] ? char.ToUpper(c) : c);
            }
            return sb.ToString();
        }
    }

    public class BetterFormattedText
    {
        private readonly string _plainText;
        private readonly List<TextRange> _formatting
          = new List<TextRange>();

        public BetterFormattedText(string plainText)
        {
            this._plainText = plainText;
        }

        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange { Start = start, End = end };
            _formatting.Add(range);
            return range;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var i = 0; i < _plainText.Length; i++)
            {
                var c = _plainText[i];
                foreach (var range in _formatting)
                    if (range.Covers(i) && range.Capitalize)
                        c = char.ToUpperInvariant(c);
                sb.Append(c);
            }

            return sb.ToString();
        }

        public class TextRange
        {
            public int Start, End;
            public bool Capitalize, Bold, Italic;

            public bool Covers(int position)
            {
                return position >= Start && position <= End;
            }
        }
    }

    public class FlyweightTextFormattingDemo
    {
        static void Main(string[] args)
        {
            var ft = new FormattedText("This is a brave new world");
            ft.Capitalize(10, 15);
            Console.WriteLine(ft);

            var bft = new BetterFormattedText("This is a brave new world");
            bft.GetRange(10, 15).Capitalize = true;
            Console.WriteLine(bft);
        }
    }
}
