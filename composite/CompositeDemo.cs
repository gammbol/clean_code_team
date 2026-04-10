namespace CadCompositePattern
{
    public class CompositeDemo
    {
        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Паттерн Composite: CAD-система\n");

            // Создаём простые фигуры
            var circle1 = new Circle("Круг 1", 10, 10, 5);
            var circle2 = new Circle("Круг 2", 30, 20, 3);
            var rect1 = new Rectangle("Прям 1", 100, 100, 40, 20);
            var rect2 = new Rectangle("Прям 2", 200, 150, 30, 30);

            // Создаём группу и добавляем в неё фигуры
            var groupA = new Group("Группа A");
            groupA.Add(circle1);
            groupA.Add(rect1);
            
            var groupB = new Group("Группа B");
            groupB.Add(circle2);
            groupB.Add(rect2);

            // Создаём корневую группу (весь чертёж)
            var rootGroup = new Group("Чертёж");
            rootGroup.Add(groupA);
            rootGroup.Add(groupB);
            rootGroup.Add(new Rectangle("Отдельный прям", 50, 50, 20, 20));

            Console.WriteLine("\nОтрисовка всей иерархии");
            rootGroup.Draw();

            Console.WriteLine("\nВычисление общей площади");
            Console.WriteLine($"Общая площадь всех фигур: {rootGroup.GetArea():F2}");

            Console.WriteLine("\nПеремещение всей группы на (5, -5)");
            rootGroup.Move(5, -5);

            Console.WriteLine("\nОтрисовка после перемещения");
            rootGroup.Draw();

            Console.WriteLine("\nРабота с отдельным элементом через единый интерфейс");

            // Единообразное обращение к простой фигуре
            Graphic singleShape = circle1;
            singleShape.Draw();
            Console.WriteLine($"Площадь: {singleShape.GetArea():F2}");
            singleShape.Move(10, 0);
            
            // И к группе
            Graphic groupAsShape = groupA;
            groupAsShape.Draw();
            Console.WriteLine($"Площадь группы: {groupAsShape.GetArea():F2}");
            groupAsShape.Move(-5, 5);
        }
    }
}