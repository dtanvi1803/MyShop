using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["ProductCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }
        public void Commit()
        {
            cache["ProductCategories"] = productCategories;
        }
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }
        public void update(ProductCategory productCategory)
        {
            ProductCategory productCatToUpdate = productCategories.Find(p => p.Id == productCategory.Id);
            if (productCatToUpdate != null)
            {
                productCatToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
        public ProductCategory Find(string Id)
        {
            ProductCategory productCat = productCategories.Find(p => p.Id == Id);
            if (productCat != null)
            {
                return productCat;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }
        public void Delete(string Id)
        {
            ProductCategory productCatToDelete = productCategories.Find(p => p.Id == Id);
            if (productCatToDelete != null)
            {
                productCategories.Remove(productCatToDelete);
            }
        }

    }
}
