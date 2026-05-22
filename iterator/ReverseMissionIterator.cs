using System;
using System.Collections.Generic;

namespace IteratorPatternExample
{
    public class ReverseMissionIterator : IMissionIterator
    {
        private readonly List<MissionStage> _stages;
        private int _position;

        public ReverseMissionIterator(List<MissionStage> stages)
        {
            _stages = stages;
            _position = stages.Count - 1;
        }

        public bool HasNext() => _position >= 0;

        public MissionStage GetNext()
        {
            if (!HasNext())
                throw new InvalidOperationException("Не осталось стадий.");
            return _stages[_position--];
        }

        public void Reset() => _position = _stages.Count - 1;
    }
}