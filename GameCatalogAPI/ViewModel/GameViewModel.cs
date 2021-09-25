using System;

namespace GameCatalogAPI.ViewModel
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Developer { get; set; }
        
        public double Price { get; set; }
    }
}