using System;

using Microsoft.Extensions.DependencyInjection;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {


            IServiceCollection services = new ServiceCollection();
            Startup start = new Startup(args);
            start.ConfigureServices(services);

            IServiceProvider provider = services.BuildServiceProvider();
            var dbContext = provider.GetRequiredService<EFContext>();
            dbContext.Database.EnsureCreated();
            LoadData(provider.GetRequiredService<IOptions<XMLReadConfig>>().Value, dbContext);
        }

        private static void LoadData(XMLReadConfig config, EFContext db)
        {

            XDocument xdocProduct = XDocument.Load(config.ProductFile);
            XNamespace ns = "http://schemas.datacontract.org/2004/07/WAAD.POC.ProductCatalog.DataModels";

            int i = 0;
            Product objProduct = new Product();
            List<Product> lstProduct
               = (from _product in xdocProduct.Element(ns + "ArrayOfProduct").Elements(ns + "Product")
                  select new Product
                  {
                      Brand = _product.Element(ns + "Brand").Value,
                      Category = _product.Element(ns + "Category").Value,
                      Description = _product.Element(ns + "Description").Value,
                      GroupNumber = _product.Element(ns + "GroupNumber").Value,
                      Id = _product.Element(ns + "Id").Value,
                      ImagePath = _product.Element(ns + "ImagePath").Value,
                      Name = _product.Element(ns + "Name").Value,
                      Price = Decimal.Parse(_product.Element(ns + "Price").Value),
                      ProductColor = _product.Element(ns + "ProductColor").Value,
                      SubCategory = _product.Element(ns + "SubCategory").Value,

                      ProductSpecifications = (from _productSpecifications in _product.Element(ns + "ProductSpecifications").Elements(ns + "ProductSpecification")
                                               select new ProductSpecification
                                               {
                                                   Id = ++i,
                                                   AllowComparision = Boolean.Parse(_productSpecifications.Element(ns + "AllowComparision").Value),
                                                   AllowFiltering = Boolean.Parse(_productSpecifications.Element(ns + "AllowFiltering").Value),
                                                   Name = _productSpecifications.Element(ns + "Name").Value,
                                                   Value = _productSpecifications.Element(ns + "Value").Value,
                                               }).ToList()

                  }).ToList();

            XDocument xdocProductCategory = XDocument.Load(config.CategoryFile);
            ProductCategory objProductCategory = new ProductCategory();
            List<ProductCategory> lstProductCategory
               = (from _productCategory in xdocProductCategory.Element(ns + "ArrayOfProductCategory").Elements(ns + "ProductCategory")
                  select new ProductCategory
                  {
                      Id = _productCategory.Element(ns + "Id").Value,
                      Name = _productCategory.Element(ns + "Name").Value,
                      ImagePath = _productCategory.Element(ns + "ImagePath").Value,
                      SubCategoryItems = (from _productSubCategory in _productCategory.Element(ns + "SubCategoryItems").Elements(ns + "ProductSubCategory")
                                          select new ProductSubCategory
                                          {
                                              Id = _productSubCategory.Element(ns + "Id").Value,
                                              Name = _productSubCategory.Element(ns + "Name").Value,
                                              ImagePath = _productSubCategory.Element(ns + "ImagePath").Value,
                                              ProductCount = Int32.Parse(_productSubCategory.Element(ns + "ProductCount").Value),
                                          }).ToList()
                  }).ToList();

            foreach (Product p in lstProduct)
            {
                db.Products.Add(p);
            }
            foreach (ProductCategory pc in lstProductCategory)
            {
                db.ProductCategories.Add(pc);
            }
            db.SaveChanges();
        }

    }
}
