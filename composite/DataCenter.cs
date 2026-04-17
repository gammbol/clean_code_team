namespace ServerMonitoringComposite
{
    public class DataCenter : MonitoringComponent
    {
        private readonly string _name;
        private readonly List<MonitoringComponent> _clusters = new();

        public DataCenter(string name)
        {
            _name = name;
        }

        public override string Name => $"Дата-центр {_name}";

        public override void Add(MonitoringComponent component)
        {
            _clusters.Add(component);
        }

        public override void Remove(MonitoringComponent component)
        {
            _clusters.Remove(component);
        }

        public override double GetCurrentLoad()
        {
            if (_clusters.Count == 0) return 0;
            return _clusters.Average(c => c.GetCurrentLoad());
        }

        public override string GetStatus()
        {
            double load = GetCurrentLoad();
            if (load < 60) return "Оптимально";
            if (load < 75) return "Внимание";
            return "Перегрузка";
        }

        public override void Display(int indent = 0)
        {
            string padding = new(' ', indent * 2);
            Console.WriteLine($" {Name} (суммарная нагрузка: {GetCurrentLoad():F1}%) {GetStatus()}");
            foreach (var cluster in _clusters)
            {
                cluster.Display(indent + 1);
            }
        }
    }
}