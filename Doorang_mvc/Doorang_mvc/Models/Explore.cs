using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doorang_mvc.Models
{
    public class Explore
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string Title { get; set; } = null!;
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? imgFile { get; set; }
    }
}
