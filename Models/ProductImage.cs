using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretUygulamasi.Models
{
    [Table("ProductImages")]
    public class ProductImage : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
