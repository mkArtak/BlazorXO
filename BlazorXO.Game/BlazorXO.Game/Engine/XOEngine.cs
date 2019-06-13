using System;
using System.Collections.Generic;

namespace BlazorXO.Game.Engine
{
    public class XOEngine
    {
        private bool isXTurn = true;

        public BoardCell[,] Map { get; private set; }

        public bool IsGameFinished { get; private set; } = false;

        public int BoardHeigth { get => Map.GetLength(0); }

        public int BoardWidth { get => Map.GetLength(1); }

        public BoardCellType CurrentTurn { get => isXTurn ? BoardCellType.X : BoardCellType.O; }

        public GameOptions Options { get; }

        public XOEngine(GameOptions options)
        {
            this.Options = options;

            this.Initialize();
        }

        public void Initialize()
        {
            this.Map = new BoardCell[this.Options.BoardHeight, this.Options.BoardWidth];
            for (int i = 0; i < this.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.Map.GetLength(1); j++)
                {
                    this.Map[i, j] = new BoardCell { PositionX = i, PositionY = j };
                }
            }
        }

        public MoveResult Set(MapPosition position)
        {
            if (IsGameFinished)
            {
                throw new InvalidMoveException("Game is finished. No more moves are allowed.");
            }

            if (Map[position.I, position.J].CellType != BoardCellType.Empty)
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
            Map[position.I, position.J].CellType = value;

            MoveResult result = new MoveResult();

            if (TryGetWinPositions(value, position, out var winPositions))
            {
                result.WinPositions = winPositions;
                result.IsGameFinished = true;
            }

            if (!result.HasWinner)
            {
                bool isBoardFull = IsBoardFull();
                if (isBoardFull)
                {
                    result.IsGameFinished = true;
                }
            }
            else
            {
                foreach (var winPosition in winPositions)
                {
                    Map[winPosition.I, winPosition.J].IsHighlighted = true;
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
                if (!IsPositionOnMap(currentPosition))
                {
                    break;
                }

                if (this.Map[currentPosition.I, currentPosition.J].CellType != cellType)
                {
                    break;
                }

                matchingPositions.Add(currentPosition);
            } while (true);
        }

        private bool IsPositionOnMap(MapPosition currentPosition)
        {
            if (currentPosition.I < 0 || currentPosition.I >= this.Map.GetLength(0)
                || currentPosition.J < 0 || currentPosition.J >= this.Map.GetLength(1))
            {
                return false;
            }

            return true;
        }

        private bool IsBoardFull()
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
    }
}
