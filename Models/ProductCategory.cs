using System.Collections.Generic;

namespace AzureSqlDotnetCore.Models
{
    public class ProductCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public List<ProductSubCategory> SubCategoryItems { get; set; }
    }
}