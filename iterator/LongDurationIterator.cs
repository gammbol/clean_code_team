using System;
using System.Collections.Generic;

namespace IteratorPatternExample
{
    // Итератор только для длинных этапов
    public class LongDurationIterator : IMissionIterator
    {
        private readonly List<MissionStage> _stages;

        private readonly int _minimumDays;

        private int _position;

        public LongDurationIterator(
            List<MissionStage> stages,
            int minimumDays)
        {
            _stages = stages;

            _minimumDays = minimumDays;

            _position = 0;
        }

        public bool HasNext()
        {
            while (_position < _stages.Count)
            {
                if (_stages[_position].DurationInDays >=
                    _minimumDays)
                {
                    return true;
                }

                _position++;
            }

            return false;
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