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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICatagoryRepository Catagory { get; private set; }
        public IProductsRepository Product { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Catagory=new CatagoryRepository(_context);
            Product=new ProductsRepository(_context);
        }
       

        public void Save()
        {
           _context.SaveChanges();
        }
    }
}
