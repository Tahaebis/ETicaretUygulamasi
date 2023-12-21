using ETicaretUygulamasi.Models;

namespace ETicaretUygulamasi.Areas.Admin.Models.ProductModels
{
    public class ProductImageModel
    {
        public Product Product { get; set; }
        public List<ProductImage> Images { get; set; }
    }
}
