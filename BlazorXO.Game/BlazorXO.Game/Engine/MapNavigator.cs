using System;

namespace BlazorXO.Game.Engine
{
    internal class MapNavigator
    {
        public static MapNavigator N = new MapNavigator(pos =>
            new MapPosition
            {
                I = pos.I - 1,
                J = pos.J
            });

        public static MapNavigator NE = new MapNavigator(pos =>
            new MapPosition
            {
                I = pos.I - 1,
                J = pos.J + 1
            });

        public static MapNavigator E = new MapNavigator(pos =>
            new MapPosition
            {
                I = pos.I,
                J = pos.J + 1
            });

        public static MapNavigator SE = new MapNavigator(pos =>
            new MapPosition
            {
                I = pos.I + 1,
                J = pos.J + 1
            });

        public static MapNavigator S = new MapNavigator(pos =>
            new MapPosition
            {
                I = pos.I + 1,
                J = pos.J + 1
            });

        public static MapNavigator SW = new MapNavigator(pos =>
            new MapPosition
            {
                I = pos.I + 1,
                J = pos.J - 1
            });

        public static MapNavigator W = new MapNavigator(pos =>
            new MapPosition
            {
                I = pos.I,
                J = pos.J - 1
            });

        public static MapNavigator NW = new MapNavigator(pos =>
            new MapPosition
            {
                I = pos.I - 1,
                J = pos.J - 1
            });

        private Func<MapPosition, MapPosition> direction;

        private MapNavigator(Func<MapPosition, MapPosition> directionFunction)
        {
            if (directionFunction == null)
            {
                throw new ArgumentNullException();
            }

            this.direction = directionFunction;
        }

        public MapPosition Next(MapPosition position) => this.direction(position);
    }
}
