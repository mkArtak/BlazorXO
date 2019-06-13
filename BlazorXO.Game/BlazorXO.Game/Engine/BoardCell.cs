namespace BlazorXO.Game.Engine
{
    public class BoardCell
    {
        public MapPosition Position { get; set; }

        public BoardCellType CellType { get; set; } = BoardCellType.Empty;

        public bool IsHighlighted { get; set; } = false;
    }
}
