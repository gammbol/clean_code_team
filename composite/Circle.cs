namespace CadCompositePattern
{
    //Листовые элементы (простые фигуры)
    public class Circle : Graphic
    {
        private string _name;
        private int _x, _y;
        private double _radius;

        public Circle(string name, int x, int y, double radius)
        {
            _name = name;
            _x = x;
            _y = y;
            _radius = radius;
        }

        public override string Name => _name;

        public override void Draw()
        {
            Console.WriteLine($"  Окружность '{_name}': центр({_x},{_y}), радиус={_radius}");
        }

        public override void Move(int dx, int dy)
        {
            _x += dx;
            _y += dy;
            Console.WriteLine($"Окружность '{_name}' перемещена на ({dx},{dy}) → центр({_x},{_y})");
        }

        public override double GetArea() => Math.PI * _radius * _radius;
    }
}