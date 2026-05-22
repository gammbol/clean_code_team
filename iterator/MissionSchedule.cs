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

        public IMissionIterator CreateReverseIterator()
        {
            return new ReverseMissionIterator(_stages);
        }

        public IMissionIterator CreateEvenIterator()
        {
            return new EvenStageIterator(_stages);
        }

        public IMissionIterator CreateLongDurationIterator(
            int minimumDays)
        {
            return new LongDurationIterator(
                _stages,
                minimumDays);
        }
    }
}