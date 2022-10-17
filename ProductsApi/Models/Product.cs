using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Models
{
    public enum ProductType
    {
        Books,
        Electronics,
        Food,
        Furniture,
        Toys
    }

    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public ProductType Type { get; set; }
    }
}
