using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretUygulamasi.Models
{
    [Table("Orders")]
    public class Order : EntityBase
    {
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int DeliveryAddressId { get; set; }
        public int InvoiceAddressId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
