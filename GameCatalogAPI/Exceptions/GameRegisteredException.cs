using System;

namespace GameCatalogAPI.Exceptions
{
    public class GameRegisteredException : Exception
    {
        public GameRegisteredException() 
            : base("A game with this Name and Developer has already been registered") {}
    }
}