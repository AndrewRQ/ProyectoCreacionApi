using System.Security.Cryptography.X509Certificates;

namespace Proyecto.Domain
{
    public class ProductsModel
    {
        public int? Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime? CreationDate { get; set; }

    }
}
