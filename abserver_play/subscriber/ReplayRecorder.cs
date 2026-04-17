using System;
using System.Collections.Generic;

namespace MatchmakingObserver.subscriber
{
    // запись и сохранение реплеев матчей.
    public class ReplayRecorder : IEventListener
    {
        private readonly HashSet<string> _activeRecordings;

        private readonly List<string> _savedReplays;

        public ReplayRecorder()
        {
            _activeRecordings = new HashSet<string>();
            _savedReplays = new List<string>();
            Console.WriteLine("ReplayRecorder: Система записи реплеев инициализирована");
        }

        public void OnEventOccurred(string eventType, MatchEventData eventData)
        {
            switch (eventType)
            {
                case "MatchStarted":
                    StartRecording(eventData.MatchId);
                    break;

                case "MatchCompleted":
                    StopRecording(eventData.MatchId, eventData);
                    break;
            }
        }

        private void StartRecording(string matchId)
        {
            _activeRecordings.Add(matchId);
            Console.WriteLine($"Replay: Начата запись реплея: {matchId}");
        }

        private void StopRecording(string matchId, MatchEventData eventData)
        {
            if (_activeRecordings.Remove(matchId))
            {
                // сохранение файла
                string replayFile = $"replays/{matchId}_{DateTime.Now:yyyyMMdd_HHmmss}.replay";
                _savedReplays.Add(replayFile);

                Console.WriteLine($"Replay: Реплей {matchId} сохранён: {replayFile}");
                Console.WriteLine($"Replay:    Игроки: {string.Join(", ", eventData.Players)}");
            }
        }

        // Показать список сохранённых реплеев
        public void PrintSavedReplays()
        {
            Console.WriteLine($"\nReplayRecorder: Сохранено реплеев: {_savedReplays.Count}");
            foreach (var replay in _savedReplays)
            {
                Console.WriteLine($"ReplayRecorder:     {replay}");
            }
        }
    }
}