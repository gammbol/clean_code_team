namespace ServerMonitoringComposite
{
    public class RamMetric : MonitoringComponent
    {
        private readonly string _serverName;
        private readonly Random _random = new();

        public RamMetric(string serverName)
        {
            _serverName = serverName;
        }

        public override string Name => $"RAM сервера {_serverName}";

        public override double GetCurrentLoad()
        {
            return _random.NextDouble() * 100;
        }

        public override string GetStatus()
        {
            double load = GetCurrentLoad();
            if (load < 75) return "Норма";
            if (load < 90) return "Предупреждение";
            return "Критично";
        }

        public override void Display(int indent = 0)
        {
            string padding = new(' ', indent * 2);
            double load = GetCurrentLoad();
            Console.WriteLine($"{padding} {Name}: {load:F1}% {GetStatus()}");
        }
    }
}