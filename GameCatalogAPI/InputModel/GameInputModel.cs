using System.ComponentModel.DataAnnotations;

namespace GameCatalogAPI.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The Game title must contain between 3 and 100 characters")]
        public string Title { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The Developer name must contain between 3 and 100 characters")]
        public string Developer { get; set; }
        
        [Required]
        [Range(1, 1000, ErrorMessage = "The Game price must be a minimum of 1 and a maximum of 1000")]
        public double Price { get; set; }
    }
}