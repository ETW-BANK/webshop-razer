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
           _context.Update(product);
        }
    }
}
