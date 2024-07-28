using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEbshopnew.DataAccess.Data;
using WEbshopnew.DataAccess.Repository.IRepository;
using WEbshopnew.Models;

namespace WEbshopnew.DataAccess.Repository
{
  public class ProductsRepository :Repository<Products>, IProductsRepository

    {

        private ApplicationDbContext _context;

        public ProductsRepository(ApplicationDbContext context ):base(context) 
        {
            _context = context;
        }




        public void Update(Products product)
        {
            var objFromDb = _context.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (objFromDb != null)
            {
                objFromDb.ProductName = product.ProductName;
                objFromDb.Description = product.Description;
                objFromDb.Manufacturer = product.Manufacturer;
                objFromDb.Price = product.Price;
                objFromDb.Size = product.Size;
                objFromDb.Instructions = product.Instructions;
                objFromDb.Ingredients = product.Ingredients;
                objFromDb.ExpiryDate = product.ExpiryDate;
                objFromDb.StockQuantity = product.StockQuantity;
                objFromDb.Usage = product.Usage;
                objFromDb.IsPrescriptionRequired = product.IsPrescriptionRequired;
                objFromDb.CatagoryId = product.CatagoryId;

              
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    objFromDb.ImageUrl = product.ImageUrl;
                }

              
                
            }
        }

    }
}
