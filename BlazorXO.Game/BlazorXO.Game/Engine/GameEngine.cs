namespace BlazorXO.Game.Engine
{
    public class GameEngine
    {
        private bool isXTurn = true;

        public bool IsGameFinished { get; private set; } = false;

        public BoardCellType CurrentTurn { get => isXTurn ? BoardCellType.X : BoardCellType.O; }

        public GameOptions Options { get; }

        public Board Board { get; private set; }

        public ISolutionStrategy SolutionStrategy { get; }

        public GameEngine(GameOptions options, ISolutionStrategy strategy)
        {
            this.Options = options;

            this.Board = new Board(options);
            this.SolutionStrategy = strategy;
        }

        public MoveResult Set(MapPosition position)
        {
            if (IsGameFinished)
            {
                throw new InvalidMoveException("Game is finished. No more moves are allowed.");
            }

            if (this.Board[position].CellType != BoardCellType.Empty)
            {
                throw new InvalidMoveException("Cell is used");
            }

            MoveResult result = UpdateCellState(position, CurrentTurn);
            isXTurn = !isXTurn;
            IsGameFinished = result.IsGameFinished;

            return result;
        }

        private MoveResult UpdateCellState(MapPosition position, BoardCellType value)
        {
            this.Board[position].CellType = value;

            MoveResult result = new MoveResult();

            if (this.SolutionStrategy.TryGetWinPositions(this.Board, value, position, out var winPositions))
            {
                result.WinPositions = winPositions;
                result.IsGameFinished = true;
            }

            if (!result.HasWinner)
            {
                bool isBoardFull = this.Board.IsBoardFull();
                if (isBoardFull)
                {
                    result.IsGameFinished = true;
                }
            }
            else
            {
                foreach (var winPosition in winPositions)
                {
                    this.Board[winPosition].IsHighlighted = true;
                }
            }

            return result;
        }
    }
}
