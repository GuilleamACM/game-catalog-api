using System;

namespace GameCatalogAPI.Exceptions
{
    public class GameNotRegisteredException : Exception
    {
        public GameNotRegisteredException() 
            : base("This Game does not exists") {}
    }
}