using System.ComponentModel.DataAnnotations;

namespace ShopNT.UI.Models
{
    public class BrandUpdateRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
