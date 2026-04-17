namespace ServerMonitoringComposite
{
    public class DiskMetric : MonitoringComponent
    {
        private readonly string _serverName;
        private readonly string _diskName;
        private readonly Random _random = new();

        public DiskMetric(string serverName, string diskName)
        {
            _serverName = serverName;
            _diskName = diskName;
        }

        public override string Name => $"Диск {_diskName} сервера {_serverName}";

        public override double GetCurrentLoad()
        {
            return _random.NextDouble() * 100;
        }

        public override string GetStatus()
        {
            double load = GetCurrentLoad();
            if (load < 80) return "Норма";
            if (load < 95) return "Предупреждение";
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