using System;

namespace GameCatalogAPI.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Developer { get; set; }
        
        public double Price { get; set; }
    }
}