using System.Collections.Generic;

namespace BlazorXO.Game.Engine
{
    public class MoveResult
    {
        public bool IsGameFinished { get; set; }

        public bool HasWinner { get => WinPositions != null; }

        public IEnumerable<MapPosition> WinPositions { get; set; }
    }
}
