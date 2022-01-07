using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Core.CreationalPatterns.Builder.Builder
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int IndentSize = 2;

        public HtmlElement()
        {
        }

        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', IndentSize * indent);
            sb.Append($"{i}<{Name}>\n");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', IndentSize * (indent + 1)));
                sb.Append(Text);
                sb.Append("\n");
            }

            foreach (var e in Elements)
                sb.Append(e.ToStringImpl(indent + 1));

            sb.Append($"{i}</{Name}>\n");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

        public static HtmlBuilder Create(string name) => new HtmlBuilder(name);
    }

    public class HtmlBuilder
    {
        private readonly string _rootName;
        protected HtmlElement Root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            this._rootName = rootName;
            Root.Name = rootName;
        }

        // not fluent
        public void AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            Root.Elements.Add(e);
        }

        public HtmlBuilder AddChildFluent(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            Root.Elements.Add(e);
            return this;
        }

        public override string ToString() => Root.ToString();

        public void Clear()
        {
            Root = new HtmlElement { Name = _rootName };
        }

        public HtmlElement Build() => Root;

        public static implicit operator HtmlElement(HtmlBuilder builder)
        {
            return builder.Root;
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            {
                // if we want to build a simple HTML paragraph...
                var hello = "hello";
                var text = "";
                text += "<p>"; // <p> → text
                text += hello; // <p>hello → text
                text += "</p>"; // <p>hello</p> → text
                Console.WriteLine(text);

                // now we want an HTML list with 2 words in it
                var sb = new StringBuilder();
                var words = new[] {"hello", "world"};
                sb.Clear();
                sb.Append("<ul>");
                foreach (var word in words)
                {
                    sb.AppendFormat("<li>{0}</li>", word);
                }

                sb.Append("</ul>");
                Console.WriteLine(sb);

                // ordinary non-fluent builder
                var builder = new HtmlBuilder("ul");
                builder.AddChild("li", "hello");
                builder.AddChild("li", "world");
                Console.WriteLine(builder.ToString());

                // fluent builder
                sb.Clear();
                builder.Clear(); // disengage builder from the object it's building, then...
                builder.AddChildFluent("li", "hello").AddChildFluent("li", "world");
                Console.WriteLine(builder);
            }

            // with factory method
            {
                var builder = HtmlElement.Create("ul");
                builder.AddChildFluent("li", "hello")
                    .AddChildFluent("li", "world");
                Console.WriteLine(builder);
            }

            // with implicit operator
            {
                var root = HtmlElement
                    .Create("ul")
                    .AddChildFluent("li", "hello")
                    .AddChildFluent("li", "world");
                Console.WriteLine(root);
            }
        }
    }
}
