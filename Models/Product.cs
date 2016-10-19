using System.Collections.Generic;
namespace AzureSqlDotnetCore.Models
{
    public class Product
    {
        public string Id { get; set; }

        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<ProductSpecification> ProductSpecifications { get; set; }
        public string GroupNumber { get; set; }
        public string Brand { get; set; }
        public string ProductColor { get; set; }
    }
}