using System.ComponentModel.DataAnnotations;

namespace ETicaretUygulamasi.Areas.Admin.Models.CategoryModels
{
    public class CategoryCreate
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string? Description { get; set; }
        public bool Hidden { get; set; }
        public bool Locked { get; set; }
    }
}
