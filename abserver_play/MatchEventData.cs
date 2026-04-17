using System;

namespace MatchmakingObserver
{
    // Класс данных события матча.
    public class MatchEventData : EventArgs
    {
        // Тип события (MatchFound, MatchStarted, MatchCompleted и т.д.)
        public string EventType { get; set; } = string.Empty;

        public string MatchId { get; set; } = string.Empty;

        public string[] Players { get; set; } = Array.Empty<string>();

        // Доп инф (победитель, длительность)
        public string? AdditionalInfo { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public MatchEventData(string eventType, string matchId, string[] players, string? additionalInfo = null)
        {
            EventType = eventType;
            MatchId = matchId;
            Players = players;
            AdditionalInfo = additionalInfo;
            Timestamp = DateTime.Now;
        }
    }
}