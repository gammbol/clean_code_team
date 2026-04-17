using MatchmakingObserver.subscriber;
using System;

namespace MatchmakingObserver
{
    class Program
    {
        static void Main(string[] args)
        {
            var simulation = new MatchmakingSimulationManager();
            simulation.Run();
        }
    }
}