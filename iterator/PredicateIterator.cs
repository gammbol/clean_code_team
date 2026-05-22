using System;
using System.Collections.Generic;

namespace IteratorPatternExample
{
    public class PredicateIterator : IMissionIterator
    {
        private readonly List<MissionStage> _stages;
        private readonly Func<MissionStage, bool> _predicate;
        private int _position;

        public PredicateIterator(List<MissionStage> stages, Func<MissionStage, bool> predicate)
        {
            _stages = stages;
            _predicate = predicate;
            _position = 0;
        }

        public bool HasNext()
        {
            while (_position < _stages.Count && !_predicate(_stages[_position]))
                _position++;
            return _position < _stages.Count;
        }

        public MissionStage GetNext()
        {
            if (!HasNext())
                throw new InvalidOperationException("Не осталось стадий.");
            return _stages[_position++];
        }

        public void Reset() => _position = 0;
    }
}