using AzureSqlDotnetCore.Infrastructure;
using AzureSqlDotnetCore.Models;
namespace AzureSqlDotnetCore
{
    public class DataImporter
    {
        private readonly IProduceItems<Product> _productProducer;
        private readonly IProduceItems<ProductCategory> _categoryProducer;
        private readonly EFContext _db;
        public DataImporter(IProduceItems<Product> productProducer, IProduceItems<ProductCategory> categoryProducer, EFContext db)
        {
            _productProducer = productProducer;
            _categoryProducer = categoryProducer;
            _db = db;
        }

        public void ImportData()
        {
            _db.Products.AddRange(_productProducer.GetItems());

            _db.ProductCategories.AddRange(_categoryProducer.GetItems());

            _db.SaveChanges();
        }
    }
}