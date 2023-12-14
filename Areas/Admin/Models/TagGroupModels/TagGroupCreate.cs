using System.ComponentModel.DataAnnotations;

namespace ETicaretUygulamasi.Areas.Admin.Models.TagGroups
{
    public class TagGroupCreate
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
