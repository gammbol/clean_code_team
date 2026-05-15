namespace IteratorPatternExample
{
    // Конкретный итератор
    public class MissionIterator : IMissionIterator
    {
        private readonly List<MissionStage> _stages;

        private int _position;

        public MissionIterator(
            List<MissionStage> stages)
        {
            _stages = stages;

            _position = 0;
        }

        public bool HasNext()
        {
            return _position < _stages.Count;
        }

        public MissionStage GetNext()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException(
                    "No more mission stages.");
            }

            MissionStage stage =
                _stages[_position];

            _position++;

            return stage;
        }

        public void Reset()
        {
            _position = 0;
        }
    }
}