namespace CadCompositePattern
{
    public class Rectangle : Graphic
    {
        private string _name;
        private int _x, _y;
        private double _width, _height;

        public Rectangle(string name, int x, int y, double width, double height)
        {
            _name = name;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public override string Name => _name;

        public override void Draw()
        {
            Console.WriteLine($"  Прямоугольник '{_name}': левый верхний({_x},{_y}), ширина={_width}, высота={_height}");
        }

        public override void Move(int dx, int dy)
        {
            _x += dx;
            _y += dy;
            Console.WriteLine($"Прямоугольник '{_name}' перемещён на ({dx},{dy}) → ({_x},{_y})");
        }

        public override double GetArea() => _width * _height;
    }
}