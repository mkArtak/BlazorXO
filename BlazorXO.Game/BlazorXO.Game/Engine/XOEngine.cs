namespace BlazorXO.Game.Engine
{
    public class XOEngine
    {
        private BoardCellType[,] map;

        private bool isXTurn = true;

        public bool IsGameFinished { get; private set; } = false;

        public int BoardHeigth { get => map.GetLength(0); }

        public int BoardWidth { get => map.GetLength(1); }

        public BoardCellType CurrentTurn { get => isXTurn ? BoardCellType.X : BoardCellType.O; }

        public XOEngine(int boardHeigth, int boardWidth)
        {
            this.map = new BoardCellType[boardHeigth, boardWidth];
        }

        public MoveResult Set(int i, int j)
        {
            if (IsGameFinished)
            {
                throw new InvalidMoveException("Game is finished. No more moves are allowed.");
            }

            if (map[i, j] != BoardCellType.Empty)
            {
                throw new InvalidMoveException("Cell is used");
            }

            MoveResult result = UpdateCellState(i, j, CurrentTurn);
            isXTurn = !isXTurn;
            IsGameFinished = result.IsGameFinished;

            return result;
        }

        private MoveResult UpdateCellState(int i, int j, BoardCellType value)
        {
            map[i, j] = value;

            bool gameEnded = true;
            for (int x = 0; x < map.GetLength(0) || !gameEnded; x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == BoardCellType.Empty)
                    {
                        gameEnded = false;
                        break;
                    }
                }
            }

            MoveResult result = new MoveResult();

            if (gameEnded)
            {
                // TODO: Implement game end calculation logic here to determine the winner;
            }
            else
            {
                result.HasWinner = false;
                result.IsGameFinished = false;
            }

            return result;
        }
    }
}
