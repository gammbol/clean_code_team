namespace MatchmakingObserver
{
    // Интерфейс наблюдателя (для подписчиков)
    public interface IEventListener
    {
        void OnEventOccurred(string eventType, MatchEventData eventData);
    }
}