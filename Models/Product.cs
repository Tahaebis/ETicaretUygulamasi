using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretUygulamasi.Models
{
    [Table("Products")]
    public class Product : EntityBase
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal TaxRate { get; set; }
        public int Stock { get; set; }

        // Foreign Key kolon : ürünün ait oldugu category id si.
        public int CategoryId { get; set; }
        public int? TagGroupId { get; set; }


        // Navigation prop : bize kodlama da kategory nesnesini çekmede yardımcı olacak.
        public virtual Category Category { get; set; }
        public virtual TagGroup TagGroup { get; set; }
    }
}
