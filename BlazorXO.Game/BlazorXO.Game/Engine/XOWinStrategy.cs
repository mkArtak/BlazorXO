using System.Collections.Generic;

namespace BlazorXO.Game.Engine
{
    internal partial class XOWinStrategy : ISolutionStrategy
    {
        private int WinSequenceSize { get; }

        private Board GameBoard { get; }

        public XOWinStrategy(GameOptions options, Board board)
        {
            this.WinSequenceSize = options.WinSequenceSize;
            this.GameBoard = board;
        }

        public bool TryGetWinPositions(BoardCellType cellType, MapPosition position, out IEnumerable<MapPosition> winPositions) =>
            TryGetWinPositionsOnDirections(cellType, position, MapNavigator.N, MapNavigator.S, out winPositions)
                || TryGetWinPositionsOnDirections(cellType, position, MapNavigator.NE, MapNavigator.SW, out winPositions)
                || TryGetWinPositionsOnDirections(cellType, position, MapNavigator.E, MapNavigator.W, out winPositions)
                || TryGetWinPositionsOnDirections(cellType, position, MapNavigator.SE, MapNavigator.NW, out winPositions);

        private bool TryGetWinPositionsOnDirections(BoardCellType value, MapPosition position, MapNavigator direction, MapNavigator oppositeDirection, out IEnumerable<MapPosition> winPositions)
        {
            IList<MapPosition> potentialWinPositions = new List<MapPosition>();
            potentialWinPositions.Add(position);

            GetMatchingPositionsOnDirection(value, position, direction, potentialWinPositions);
            GetMatchingPositionsOnDirection(value, position, oppositeDirection, potentialWinPositions);

            if (potentialWinPositions.Count >= this.WinSequenceSize)
            {
                winPositions = potentialWinPositions;
                return true;
            }

            winPositions = null;
            return false;
        }

        private void GetMatchingPositionsOnDirection(BoardCellType cellType, MapPosition position, MapNavigator directionNavigator, IList<MapPosition> matchingPositions)
        {
            MapPosition currentPosition = position;
            do
            {
                currentPosition = directionNavigator.Next(currentPosition);
                if (!this.GameBoard.IsPositionOnMap(currentPosition))
                {
                    break;
                }

                if (this.GameBoard[currentPosition].CellType != cellType)
                {
                    break;
                }

                matchingPositions.Add(currentPosition);
            } while (true);
        }
    }
}
