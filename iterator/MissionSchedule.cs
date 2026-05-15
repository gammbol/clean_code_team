namespace IteratorPatternExample
{
    // Коллекция
    public class MissionSchedule
    {
        private readonly List<MissionStage> _stages;

        public MissionSchedule()
        {
            _stages = new List<MissionStage>();
        }

        public void AddStage(MissionStage stage)
        {
            _stages.Add(stage);
        }

        public IMissionIterator CreateIterator()
        {
            return new MissionIterator(_stages);
        }
    }
}