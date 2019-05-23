namespace BlazorXO.Game.Engine
{
    public class BoardCell
    {
        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public BoardCellType CellType { get; set; } = BoardCellType.Empty;

        public bool IsHighlighted { get; set; } = false;
    }
}
