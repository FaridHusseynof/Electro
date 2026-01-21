using System.ComponentModel.DataAnnotations;

namespace Electro.Models
{
    public class Product: BaseModel
    {
        [Required, Range(1,1000)]
        public decimal Price { get; set; }
        [Required, Length(3, 50)]
        public string Title { get; set; }
        [Required, MaxLength(40)]
        public string Category { get; set; }
        public string ImageURL { get; set; }
    }
}
