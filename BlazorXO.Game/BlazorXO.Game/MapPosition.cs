namespace BlazorXO.Game
{
    public struct MapPosition
    {
        public int I { get; }

        public int J { get; }

        public MapPosition(int i, int j)
        {
            I = i;
            J = j;
        }
    }
}
