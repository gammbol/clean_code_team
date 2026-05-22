namespace IteratorPatternExample
{
    // Класс этапа миссии
    public class MissionStage
    {
        public string StageName { get; }

        public int DurationInDays { get; }

        public MissionStage(
            string stageName,
            int durationInDays)
        {
            StageName = stageName;

            DurationInDays = durationInDays;
        }

        public override string ToString()
        {
            return $"Стадия: {StageName}, Продолжительность: {DurationInDays} дней";
        }
    }
}