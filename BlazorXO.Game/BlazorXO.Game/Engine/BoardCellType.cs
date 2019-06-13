namespace BlazorXO.Game.Engine
{
    public enum BoardCellType
    {
        /// <summary>
        /// Identifies a cell on the board, which is blocked and no activity can happen on that cell
        /// </summary>
        Disabled,

        /// <summary>
        /// Identifies an empty/available cell.
        /// </summary>
        Empty,

        /// <summary>
        /// Identifies a cell with an `X` content
        /// </summary>
        X,


        /// <summary>
        /// Identifies a cell with an `O` content
        /// </summary>
        O,
    }
}
