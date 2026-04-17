namespace ServerMonitoringComposite
{
    public abstract class MonitoringComponent
    {
        public abstract string Name { get; }
        public abstract double GetCurrentLoad();     // текущая нагрузка (0-100%)
        public abstract string GetStatus();          // "Норма", "Предупреждение", "Критично"
        public abstract void Display(int indent = 0);

        // Виртуальные методы для работы с дочерними элементами
        public virtual void Add(MonitoringComponent component)
        {
            throw new NotSupportedException("Нельзя добавить компонент к листовому узлу");
        }

        public virtual void Remove(MonitoringComponent component)
        {
            throw new NotSupportedException("У листового узла нет дочерних элементов");
        }
    }
}