using System.ComponentModel.DataAnnotations;

namespace Electro.Areas.AdminPanel.ViewModels
{
    public class CreateVM
    {
        [Required, Range(1, 1000)]
        public decimal price { get; set; }
        [Required, Length(3, 50)]
        public string title { get; set; }
        [Required, MaxLength(40)]
        public string category { get; set; }
        public string? imageURL { get; set; }
        public IFormFile? imageFile { get; set; }
    }
}
