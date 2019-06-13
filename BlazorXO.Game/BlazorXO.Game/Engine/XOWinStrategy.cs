using System.Collections.Generic;

namespace BlazorXO.Game.Engine
{
    internal class XOWinStrategy : ISolutionStrategy
    {
        public int WinSequenceSize { get; }

        public XOWinStrategy(GameOptions options)
        {
            this.WinSequenceSize = options.WinSequenceSize;
        }

        public bool TryGetWinPositions(Board board, BoardCellType cellType, MapPosition position, out IEnumerable<MapPosition> winPositions)
        {
            if (TryGetWinPositionsOnDirections(board, cellType, position, MapNavigator.N, MapNavigator.S, out winPositions)
                || TryGetWinPositionsOnDirections(board, cellType, position, MapNavigator.NE, MapNavigator.SW, out winPositions)
                || TryGetWinPositionsOnDirections(board, cellType, position, MapNavigator.E, MapNavigator.W, out winPositions)
                || TryGetWinPositionsOnDirections(board, cellType, position, MapNavigator.SE, MapNavigator.NW, out winPositions)
                )
            {
                return true;
            }

            return false;
        }

        private bool TryGetWinPositionsOnDirections(Board board, BoardCellType value, MapPosition position, MapNavigator direction, MapNavigator oppositeDirection, out IEnumerable<MapPosition> winPositions)
        {
            IList<MapPosition> potentialWinPositions = new List<MapPosition>();
            potentialWinPositions.Add(position);

            GetMatchingPositionsOnDirection(board, value, position, direction, potentialWinPositions);
            GetMatchingPositionsOnDirection(board, value, position, oppositeDirection, potentialWinPositions);

            if (potentialWinPositions.Count >= this.WinSequenceSize)
            {
                winPositions = potentialWinPositions;
                return true;
            }

            winPositions = null;
            return false;
        }

        private void GetMatchingPositionsOnDirection(Board board, BoardCellType cellType, MapPosition position, MapNavigator directionNavigator, IList<MapPosition> matchingPositions)
        {
            MapPosition currentPosition = position;
            do
            {
                currentPosition = directionNavigator.Next(currentPosition);
                if (!board.IsPositionOnMap(currentPosition))
                {
                    break;
                }

                if (board[currentPosition].CellType != cellType)
                {
                    break;
                }

                matchingPositions.Add(currentPosition);
            } while (true);
        }
    }
}
