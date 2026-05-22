using System;
using System.Collections.Generic;

namespace IteratorPatternExample
{
    // Итератор в обратном порядке
    public class ReverseMissionIterator : IMissionIterator
    {
        private readonly List<MissionStage> _stages;

        private int _position;

        public ReverseMissionIterator(
            List<MissionStage> stages)
        {
            _stages = stages;

            _position = _stages.Count - 1;
        }

        public bool HasNext()
        {
            return _position >= 0;
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

            _position--;

            return stage;
        }

        public void Reset()
        {
            _position = _stages.Count - 1;
        }
    }
}