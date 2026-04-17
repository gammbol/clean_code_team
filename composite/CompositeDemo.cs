namespace ServerMonitoringComposite
{
    public class CompositeDemo
    {
        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Создаём серверы с метриками
            var webServer = new Server("WEB-01");
            webServer.Add(new CpuMetric("WEB-01"));
            webServer.Add(new RamMetric("WEB-01"));
            webServer.Add(new DiskMetric("WEB-01", "C:"));

            var dbServer = new Server("DB-01");
            dbServer.Add(new CpuMetric("DB-01"));
            dbServer.Add(new RamMetric("DB-01"));
            dbServer.Add(new DiskMetric("DB-01", "C:"));
            dbServer.Add(new DiskMetric("DB-01", "D:"));

            var cacheServer = new Server("REDIS-01");
            cacheServer.Add(new CpuMetric("REDIS-01"));
            cacheServer.Add(new RamMetric("REDIS-01"));

            // Группируем в кластер
            var productionCluster = new Cluster("Production");
            productionCluster.Add(webServer);
            productionCluster.Add(dbServer);
            productionCluster.Add(cacheServer);

            var backupCluster = new Cluster("Backup");
            var backupServer = new Server("BACKUP-01");
            backupServer.Add(new CpuMetric("BACKUP-01"));
            backupServer.Add(new DiskMetric("BACKUP-01", "BackupDisk"));
            backupCluster.Add(backupServer);

            // Дата-центр
            var dataCenter = new DataCenter("Москва");
            dataCenter.Add(productionCluster);
            dataCenter.Add(backupCluster);

            // Отображаем всю иерархию
            Console.WriteLine("Иерархия мониторинга");
            dataCenter.Display();

            // Работа с отдельным сервером через единый интерфейс
            Console.WriteLine("\nРабота с отдельным сервером через интерфейс MonitoringComponent");
            MonitoringComponent singleServer = webServer;
            singleServer.Display();

            // Вычисление суммарной нагрузки в дата-центре
            Console.WriteLine($"\nСуммарная нагрузка дата-центра: {dataCenter.GetCurrentLoad():F1}%");
        }
    }
}