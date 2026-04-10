namespace CadCompositePattern
{
    public abstract class Graphic
    {
        public abstract string Name { get; }
        public abstract void Draw();
        public abstract void Move(int dx, int dy);
        public abstract double GetArea();
        
        // Виртуальные методы для управления дочерними элементами
        // (в листовых узлах они либо пусты, либо выбрасывают исключение)
        public virtual void Add(Graphic graphic)
        {
            throw new NotSupportedException("Нельзя добавить дочерний элемент к простой фигуре");
        }

        public virtual void Remove(Graphic graphic)
        {
            throw new NotSupportedException("У простой фигуры нет дочерних элементов");
        }

        public virtual Graphic GetChild(int index)
        {
            throw new NotSupportedException("У простой фигуры нет дочерних элементов");
        }
    }
}