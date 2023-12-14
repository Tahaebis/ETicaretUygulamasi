using System.ComponentModel.DataAnnotations;

namespace ETicaretUygulamasi.Models
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        [StringLength(50)]
        public string CreatedUserName { get; set; }

        [StringLength(50)]
        public string? ModifiedUserName { get; set; }
    }
}
