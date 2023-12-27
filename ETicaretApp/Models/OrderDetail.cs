using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretUygulamasi.Models
{
    [Table("OrderDetails")]
    public class OrderDetail : EntityBase
    {
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductDiscountedPrice { get; set; }
        public decimal ProductTaxRate { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
