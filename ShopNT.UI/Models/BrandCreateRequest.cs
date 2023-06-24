using System.ComponentModel.DataAnnotations;

namespace ShopNT.UI.Models
{
    public class BrandCreateRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
