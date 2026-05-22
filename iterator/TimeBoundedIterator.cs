using System;
using System.Collections.Generic;

namespace IteratorPatternExample
{
    public class TimeBoundedIterator : IMissionIterator
    {
        private readonly List<MissionStage> _stages;
        private readonly int _maxTotalDays;
        private int _position;
        private int _accumulatedDays;

        public TimeBoundedIterator(List<MissionStage> stages, int maxTotalDays)
        {
            _stages = stages;
            _maxTotalDays = maxTotalDays;
            _position = 0;
            _accumulatedDays = 0;
        }

        public bool HasNext()
        {
            if (_position >= _stages.Count)
                return false;

            var nextStage = _stages[_position];
            if (_accumulatedDays + nextStage.DurationInDays > _maxTotalDays)
                return false;

            return true;
        }

        public MissionStage GetNext()
        {
            if (!HasNext())
                throw new InvalidOperationException("Не осталось стадий в заданном лимите.");

            var stage = _stages[_position];
            _accumulatedDays += stage.DurationInDays;
            _position++;
            return stage;
        }

        public void Reset()
        {
            _position = 0;
            _accumulatedDays = 0;
        }
    }
}