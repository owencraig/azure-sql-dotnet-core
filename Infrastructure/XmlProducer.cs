using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using AzureSqlDotnetCore.Models;

namespace AzureSqlDotnetCore.Infrastructure
{
    public class XmlProducer : IProduceItems<Product>, IProduceItems<ProductCategory>
    {
        private readonly XNamespace _ns;
        private readonly Task<XDocument> _productsDocument;
        private readonly Task<XDocument> _categoriesDocument;
        public XmlProducer(IOptions<Config.XMLRead> options)
        {
            var config = options.Value;
            _ns = config.Namespace;
            _productsDocument = LoadXmlDoc(config.ProductFile);
            _categoriesDocument = LoadXmlDoc(config.CategoryFile);
        }

        IEnumerable<Product> IProduceItems<Product>.GetItems()
        {
            return GetProducts().Result;
        }
        async Task<IEnumerable<Product>> IProduceItems<Product>.GetItemsAsync()
        {
            return await GetProducts();
        }

        async Task<IEnumerable<ProductCategory>> IProduceItems<ProductCategory>.GetItemsAsync()
        {
            return await GetProductCategories();
        }

        IEnumerable<ProductCategory> IProduceItems<ProductCategory>.GetItems()
        {
            return GetProductCategories().Result;
        }

        private async Task<IEnumerable<Product>> GetProducts()
        {
            XDocument xdocProduct = await _productsDocument;

            return xdocProduct.Element(_ns + "ArrayOfProduct").Elements(_ns + "Product").Select(CreateProduct);
        }

        private async Task<IEnumerable<ProductCategory>> GetProductCategories()
        {
            XDocument xdocProductCategory = await _categoriesDocument;

            return xdocProductCategory.Element(_ns + "ArrayOfProductCategory").Elements(_ns + "ProductCategory").Select(CreateProductCategory);
        }

        private Task<XDocument> LoadXmlDoc(string path)
        {
            return Task.Run<XDocument>((() => XDocument.Load(path)));
        }

        private Product CreateProduct(XElement elem)
        {
            string id = elem.Element(_ns + "Id").Value;
            return new Product
            {
                Brand = elem.Element(_ns + "Brand").Value,
                Category = elem.Element(_ns + "Category").Value,
                Description = elem.Element(_ns + "Description").Value,
                GroupNumber = elem.Element(_ns + "GroupNumber").Value,
                Id = id,
                ImagePath = elem.Element(_ns + "ImagePath").Value,
                Name = elem.Element(_ns + "Name").Value,
                Price = Decimal.Parse(elem.Element(_ns + "Price").Value),
                ProductColor = elem.Element(_ns + "ProductColor").Value,
                SubCategory = elem.Element(_ns + "SubCategory").Value,
                ProductSpecifications = elem.Element(_ns + "ProductSpecifications").Elements(_ns + "ProductSpecification").Select(CreateSpecification).ToList()
            };
        }

        private ProductSpecification CreateSpecification(XElement elem)
        {
            return new ProductSpecification()
            {
                AllowComparision = Boolean.Parse(elem.Element(_ns + "AllowComparision").Value),
                AllowFiltering = Boolean.Parse(elem.Element(_ns + "AllowFiltering").Value),
                Name = elem.Element(_ns + "Name").Value,
                Value = elem.Element(_ns + "Value").Value,
            };
        }

        private ProductCategory CreateProductCategory(XElement elem)
        {
            return new ProductCategory
            {
                Id = elem.Element(_ns + "Id").Value,
                Name = elem.Element(_ns + "Name").Value,
                ImagePath = elem.Element(_ns + "ImagePath").Value,
                SubCategoryItems = elem.Element(_ns + "SubCategoryItems").Elements(_ns + "ProductSubCategory").Select(CreateSubCategory).ToList()
            };
        }

        private ProductSubCategory CreateSubCategory(XElement elem)
        {
            return new ProductSubCategory
            {
                Id = elem.Element(_ns + "Id").Value,
                Name = elem.Element(_ns + "Name").Value,
                ImagePath = elem.Element(_ns + "ImagePath").Value,
                ProductCount = Int32.Parse(elem.Element(_ns + "ProductCount").Value),
            };
        }
    }
}