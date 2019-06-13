namespace BlazorXO.Game.Engine
{
    public class Board
    {
        private BoardCell[,] Map { get; set; }

        public int BoardHeigth { get; }

        public int BoardWidth { get; }

        public Board(GameOptions options)
        {
            this.Map = new BoardCell[options.BoardHeight, options.BoardWidth];
            this.BoardHeigth = options.BoardHeight;
            this.BoardWidth = options.BoardWidth;

            for (int i = 0; i < this.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.Map.GetLength(1); j++)
                {
                    this.Map[i, j] = new BoardCell { Position = new MapPosition(i, j) };
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
            for (int x = 0; x < this.BoardHeigth; x++)
            {
                for (int y = 0; y < this.BoardWidth; y++)
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
            if (currentPosition.I < 0 || currentPosition.I >= this.BoardHeigth
                || currentPosition.J < 0 || currentPosition.J >= this.BoardWidth)
            {
                return false;
            }

            return true;
        }
    }
}
