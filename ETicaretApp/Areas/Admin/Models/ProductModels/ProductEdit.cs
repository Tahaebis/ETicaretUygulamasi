using System.ComponentModel.DataAnnotations;

namespace ETicaretUygulamasi.Areas.Admin.Models.ProductModels
{
    public class ProductEdit
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }

        [Range(0,100)]
        public decimal TaxRate { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public int? TagGroupId { get; set; }
    }
}
