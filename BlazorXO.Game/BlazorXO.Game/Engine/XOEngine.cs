using System.Collections.Generic;

namespace BlazorXO.Game.Engine
{
    public class XOEngine
    {
        private bool isXTurn = true;

        public bool IsGameFinished { get; private set; } = false;

        public BoardCellType CurrentTurn { get => isXTurn ? BoardCellType.X : BoardCellType.O; }

        public GameOptions Options { get; }

        public Board Board { get; private set; }

        public XOEngine(GameOptions options)
        {
            this.Options = options;

            this.Board = new Board(options);
        }

        public MoveResult Set(MapPosition position)
        {
            if (IsGameFinished)
            {
                throw new InvalidMoveException("Game is finished. No more moves are allowed.");
            }

            if (this.Board[position].CellType != BoardCellType.Empty)
            {
                throw new InvalidMoveException("Cell is used");
            }

            MoveResult result = UpdateCellState(position, CurrentTurn);
            isXTurn = !isXTurn;
            IsGameFinished = result.IsGameFinished;

            return result;
        }

        private MoveResult UpdateCellState(MapPosition position, BoardCellType value)
        {
            this.Board[position].CellType = value;

            MoveResult result = new MoveResult();

            if (TryGetWinPositions(value, position, out var winPositions))
            {
                result.WinPositions = winPositions;
                result.IsGameFinished = true;
            }

            if (!result.HasWinner)
            {
                bool isBoardFull = this.Board.IsBoardFull();
                if (isBoardFull)
                {
                    result.IsGameFinished = true;
                }
            }
            else
            {
                foreach (var winPosition in winPositions)
                {
                    this.Board[winPosition].IsHighlighted = true;
                }
            }

            return result;
        }

        private bool TryGetWinPositions(BoardCellType value, MapPosition position, out IEnumerable<MapPosition> winPositions)
        {
            if (TryGetWinPositionsOnDirections(value, position, MapNavigator.N, MapNavigator.S, out winPositions)
                || TryGetWinPositionsOnDirections(value, position, MapNavigator.NE, MapNavigator.SW, out winPositions)
                || TryGetWinPositionsOnDirections(value, position, MapNavigator.E, MapNavigator.W, out winPositions)
                || TryGetWinPositionsOnDirections(value, position, MapNavigator.SE, MapNavigator.NW, out winPositions)
                )
            {
                return true;
            }

            return false;
        }

        private bool TryGetWinPositionsOnDirections(BoardCellType value, MapPosition position, MapNavigator direction, MapNavigator oppositeDirection, out IEnumerable<MapPosition> winPositions)
        {
            IList<MapPosition> potentialWinPositions = new List<MapPosition>();
            potentialWinPositions.Add(position);

            GetMatchingPositionsOnDirection(value, position, direction, potentialWinPositions);
            GetMatchingPositionsOnDirection(value, position, oppositeDirection, potentialWinPositions);

            if (potentialWinPositions.Count >= this.Options.WinSequenceSize)
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
                if (!this.Board.IsPositionOnMap(currentPosition))
                {
                    break;
                }

                if (this.Board[currentPosition].CellType != cellType)
                {
                    break;
                }

                matchingPositions.Add(currentPosition);
            } while (true);
        }
    }
}
