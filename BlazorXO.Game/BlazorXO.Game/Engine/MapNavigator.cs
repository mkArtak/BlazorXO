using System;

namespace BlazorXO.Game.Engine
{
    internal partial class XOWinStrategy
    {
        private class MapNavigator
        {
            public static MapNavigator N = new MapNavigator(pos => new MapPosition(pos.I - 1, pos.J));

            public static MapNavigator NE = new MapNavigator(pos => new MapPosition(pos.I - 1, pos.J + 1));

            public static MapNavigator E = new MapNavigator(pos => new MapPosition(pos.I, pos.J + 1));

            public static MapNavigator SE = new MapNavigator(pos => new MapPosition(pos.I + 1, pos.J + 1));

            public static MapNavigator S = new MapNavigator(pos => new MapPosition(pos.I + 1, pos.J + 1));

            public static MapNavigator SW = new MapNavigator(pos => new MapPosition(pos.I + 1, pos.J - 1));

            public static MapNavigator W = new MapNavigator(pos => new MapPosition(pos.I, pos.J - 1));

            public static MapNavigator NW = new MapNavigator(pos => new MapPosition(pos.I - 1, pos.J - 1));

            private readonly Func<MapPosition, MapPosition> direction;

            private MapNavigator(Func<MapPosition, MapPosition> directionFunction)
            {
                this.direction = directionFunction ?? throw new ArgumentNullException();
            }

            public MapPosition Next(MapPosition position) => this.direction(position);
        }
    }
}
