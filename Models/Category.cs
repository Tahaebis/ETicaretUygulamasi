using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretUygulamasi.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        [StringLength(50)]
        public string CreatedUserName { get; set; }

        [StringLength(50)]
        public string? ModifiedUserName { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string? Description { get; set; }
        public bool Hidden { get; set; }
        public bool Locked { get; set; }
    }
}
