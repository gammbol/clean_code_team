namespace ServerMonitoringComposite
{
    public class Cluster : MonitoringComponent
    {
        private readonly string _name;
        private readonly List<MonitoringComponent> _servers = new();

        public Cluster(string name)
        {
            _name = name;
        }

        public override string Name => $"Кластер {_name}";

        public override void Add(MonitoringComponent component)
        {
            _servers.Add(component);
        }

        public override void Remove(MonitoringComponent component)
        {
            _servers.Remove(component);
        }

        public override double GetCurrentLoad()
        {
            if (_servers.Count == 0) return 0;
            return _servers.Average(s => s.GetCurrentLoad());
        }

        public override string GetStatus()
        {
            double avgLoad = GetCurrentLoad();
            if (avgLoad < 65) return "Стабильно";
            if (avgLoad < 80) return "Повышенная нагрузка";
            return "Требуется масштабирование";
        }

        public override void Display(int indent = 0)
        {
            string padding = new(' ', indent * 2);
            Console.WriteLine($"{padding} {Name} (общая нагрузка: {GetCurrentLoad():F1}%) {GetStatus()}");
            foreach (var server in _servers)
            {
                server.Display(indent + 1);
            }
        }
    }
}