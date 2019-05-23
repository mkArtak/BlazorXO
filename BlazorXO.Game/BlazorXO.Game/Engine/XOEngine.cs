namespace BlazorXO.Game.Engine
{
    public class XOEngine
    {
        private readonly BoardCell[,] map;

        public BoardCell[,] Map { get => this.map; }

        private bool isXTurn = true;

        public bool IsGameFinished { get; private set; } = false;

        public int BoardHeigth { get => Map.GetLength(0); }

        public int BoardWidth { get => Map.GetLength(1); }

        public BoardCellType CurrentTurn { get => isXTurn ? BoardCellType.X : BoardCellType.O; }

        public XOEngine(int boardHeigth, int boardWidth)
        {
            this.map = new BoardCell[boardHeigth, boardWidth];
            for (int i = 0; i < this.map.GetLength(0); i++)
            {
                for (int j = 0; j < this.map.GetLength(1); j++)
                {
                    this.map[i, j] = new BoardCell { PositionX = i, PositionY = j };
                }
            }
        }

        public MoveResult Set(int i, int j)
        {
            if (IsGameFinished)
            {
                throw new InvalidMoveException("Game is finished. No more moves are allowed.");
            }

            if (map[i, j].CellType != BoardCellType.Empty)
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
            map[i, j].CellType = value;

            bool gameEnded = true;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y].CellType == BoardCellType.Empty)
                    {
                        gameEnded = false;
                        break;
                    }
                }

                if (!gameEnded)
                {
                    break;
                }
            }

            MoveResult result = new MoveResult();

            if (gameEnded)
            {
                result.IsGameFinished = true;

                // TODO: Implement game end calculation logic here to determine the winner;
                map[i, j].IsHighlighted = true;
                result.HasWinner = true;
            }
            else
            {
                result.HasWinner = false;
            }

            return result;
        }
    }
}
