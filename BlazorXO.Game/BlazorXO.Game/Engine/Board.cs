namespace BlazorXO.Game.Engine
{
    public class Board
    {
        private BoardCell[,] Map { get; set; }

        public int BoardHeigth { get => Map.GetLength(0); }

        public int BoardWidth { get => Map.GetLength(1); }

        public Board(GameOptions options)
        {
            this.Map = new BoardCell[options.BoardHeight, options.BoardWidth];

            for (int i = 0; i < this.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.Map.GetLength(1); j++)
                {
                    this.Map[i, j] = new BoardCell { PositionX = i, PositionY = j };
                }
            }
        }

        public BoardCell this[MapPosition position]
        {
            get => this[position.I, position.J];
        }

        public BoardCell this[int i, int j]
        {
            get => this.Map[i, j];
        }

        public bool IsBoardFull()
        {
            for (int x = 0; x < Map.GetLength(0); x++)
            {
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    if (Map[x, y].CellType == BoardCellType.Empty)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsPositionOnMap(MapPosition currentPosition)
        {
            if (currentPosition.I < 0 || currentPosition.I >= this.Map.GetLength(0)
                || currentPosition.J < 0 || currentPosition.J >= this.Map.GetLength(1))
            {
                return false;
            }

            return true;
        }
    }
}
