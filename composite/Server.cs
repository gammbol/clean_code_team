namespace ServerMonitoringComposite
{
    // Контейнеры (группы)
    public class Server : MonitoringComponent
    {
        private readonly string _name;
        private readonly List<MonitoringComponent> _metrics = new();

        public Server(string name)
        {
            _name = name;
        }

        public override string Name => $"Сервер {_name}";

        public override void Add(MonitoringComponent component)
        {
            _metrics.Add(component);
        }

        public override void Remove(MonitoringComponent component)
        {
            _metrics.Remove(component);
        }

        public override double GetCurrentLoad()
        {
            if (_metrics.Count == 0) return 0;
            return _metrics.Average(m => m.GetCurrentLoad());
        }

        public override string GetStatus()
        {
            double avgLoad = GetCurrentLoad();
            if (avgLoad < 70) return "Норма";
            if (avgLoad < 85) return "Внимание";
            return "Критическая нагрузка";
        }

        public override void Display(int indent = 0)
        {
            string padding = new(' ', indent * 2);
            Console.WriteLine($"{padding} {Name} (средняя нагрузка: {GetCurrentLoad():F1}%) {GetStatus()}");
            foreach (var metric in _metrics)
            {
                metric.Display(indent + 1);
            }
        }
    }
}