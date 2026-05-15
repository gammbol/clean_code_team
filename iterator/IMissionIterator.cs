namespace IteratorPatternExample
{
    // Интерфейс итератора
    public interface IMissionIterator
    {
        bool HasNext();

        MissionStage GetNext();

        void Reset();
    }
}