using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretUygulamasi.Models
{
    [Table("TagGroups")]
    public class TagGroup : EntityBase
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string? Description { get; set; }
        public bool Hidden { get; set; }
        public bool Locked { get; set; }
    }
}
