using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretUygulamasi.Models
{
    [Table("Addresses")]
    public class Address : EntityBase
    {
        [Required]
        [StringLength(40)]
        public string Title { get; set; }

        [Required]
        [StringLength(150)]
        public string Description { get; set; }

        [Required]
        [StringLength(25)]
        public string City { get; set; }

        [Required]
        [StringLength(25)]
        public string Town { get; set; }

        [Required]
        [StringLength(30)]
        public string DeliveryName { get; set; }

        [Required]
        [StringLength(30)]
        public string DeliverySurname { get; set; }

        [Required]
        [StringLength(30)]
        public string DeliveryPhone { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
