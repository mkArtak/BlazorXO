using System;

namespace BlazorXO.Game.Engine
{
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException() { }
        public InvalidMoveException(string message) : base(message) { }
    }
}
