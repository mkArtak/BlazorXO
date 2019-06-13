using System.Collections.Generic;

namespace BlazorXO.Game.Engine
{
    public interface ISolutionStrategy
    {
        bool TryGetWinPositions(Board board, BoardCellType cellType, MapPosition position, out IEnumerable<MapPosition> winPositions);
    }
}
