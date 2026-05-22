using System;
using System.Collections.Generic;

namespace IteratorPatternExample
{
    // Итератор только для чётных индексов
    public class EvenStageIterator : IMissionIterator
    {
        private readonly List<MissionStage> _stages;

        private int _position;

        public EvenStageIterator(
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

            _position += 2;

            return stage;
        }

        public void Reset()
        {
            _position = 0;
        }
    }
}