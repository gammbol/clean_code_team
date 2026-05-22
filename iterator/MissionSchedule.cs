using System;
using System.Collections.Generic;

namespace IteratorPatternExample
{
    public class MissionSchedule
    {
        private readonly List<MissionStage> _stages = new();

        public void AddStage(MissionStage stage) => _stages.Add(stage);

        public IMissionIterator CreateIterator() => new MissionIterator(_stages);

        public IMissionIterator CreateTimeBoundedIterator(int maxTotalDays)
            => new TimeBoundedIterator(_stages, maxTotalDays);

        public IMissionIterator CreateFilteredIterator(Func<MissionStage, bool> predicate)
            => new PredicateIterator(_stages, predicate);
    }
}