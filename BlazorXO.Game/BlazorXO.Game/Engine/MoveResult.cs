namespace BlazorXO.Game.Engine
{
    public class MoveResult
    {
        public bool IsGameFinished { get; set; }

        public bool HasWinner { get; set; }

        public int[][] WinPositions { get; set; }
    }
}
