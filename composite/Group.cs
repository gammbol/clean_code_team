namespace CadCompositePattern
{
    //Контейнер (составной элемент)
    public class Group : Graphic
    {
        private string _name;
        private List<Graphic> _children = new();

        public Group(string name)
        {
            _name = name;
        }

        public override string Name => _name;

        public override void Draw()
        {
            Console.WriteLine($"Группа '{_name}' состоит из {_children.Count} элементов:");
            foreach (var child in _children)
            {
                child.Draw();
            }
        }

        public override void Move(int dx, int dy)
        {
            Console.WriteLine($"Перемещение группы '{_name}' на ({dx},{dy})");
            foreach (var child in _children)
            {
                child.Move(dx, dy);
            }
        }

        public override double GetArea() => _children.Sum(child => child.GetArea());

        public override void Add(Graphic graphic)
        {
            _children.Add(graphic);
            Console.WriteLine($"В группу '{_name}' добавлен элемент '{graphic.Name}'");
        }

        public override void Remove(Graphic graphic)
        {
            _children.Remove(graphic);
            Console.WriteLine($"Из группы '{_name}' удалён элемент '{graphic.Name}'");
        }

        public override Graphic GetChild(int index) => _children[index];
    }
}