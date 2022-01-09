using System;

namespace DesignPatterns.Core.StructuralPatterns.Decorator
{
    public abstract class Shape
    {
        public virtual string AsString() => string.Empty;
    }

    public sealed class Circle : Shape
    {
        private float _radius;

        public Circle() : this(0)
        {

        }

        public Circle(float radius)
        {
            this._radius = radius;
        }

        public void Resize(float factor)
        {
            _radius *= factor;
        }

        public override string AsString() => $"A circle of radius {_radius}";
    }

    public sealed class Square : Shape
    {
        private readonly float _side;

        public Square() : this(0)
        {

        }

        public Square(float side)
        {
            this._side = side;
        }

        public override string AsString() => $"A square with side {_side}";
    }

    // dynamic
    public class ColoredShape : Shape
    {
        private readonly Shape _shape;
        private readonly string _color;

        public ColoredShape(Shape shape, string color)
        {
            this._shape = shape;
            this._color = color;
        }

        public override string AsString() => $"{_shape.AsString()} has the color {_color}";
    }

    public class TransparentShape : Shape
    {
        private readonly Shape _shape;
        private readonly float _transparency;

        public TransparentShape(Shape shape, float transparency)
        {
            this._shape = shape;
            this._transparency = transparency;
        }

        public override string AsString() =>
          $"{_shape.AsString()} has {_transparency * 100.0f}% transparency";
    }

    // CRTP cannot be done
    //public class ColoredShape2<T> : T where T : Shape { }

    public class ColoredShape<T> : Shape
      where T : Shape, new()
    {
        private readonly string color;
        private readonly T shape = new T();

        public ColoredShape() : this("black")
        {

        }

        public ColoredShape(string color) // no constructor forwarding
        {
            this.color = color;
        }

        public override string AsString()
        {
            return $"{shape.AsString()} has the color {color}";
        }
    }

    public class TransparentShape<T> : Shape where T : Shape, new()
    {
        private readonly float _transparency;
        private readonly T _shape = new T();

        public TransparentShape(float transparency)
        {
            _transparency = transparency;
        }

        public override string AsString()
        {
            return $"{_shape.AsString()} has transparency {_transparency * 100.0f}";
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            var circle = new Circle(2);
            Console.WriteLine(circle.AsString());

            var redCircle = new ColoredShape(circle, "red");
            Console.WriteLine(redCircle.AsString());

            var redHalfTransparentSquare = new TransparentShape(redCircle, 0.5f);
            Console.WriteLine(redHalfTransparentSquare.AsString());

            // static
            ColoredShape<Circle> blueCircle = new ColoredShape<Circle>("blue");
            Console.WriteLine(blueCircle.AsString());
            // A circle of radius 0 has the color blue

            TransparentShape<ColoredShape<Square>> blackHalfSquare = new TransparentShape<ColoredShape<Square>>(0.4f);
            Console.WriteLine(blackHalfSquare.AsString());
            // A square with side 0 has the color black has transparency 40
        }
    }
}
