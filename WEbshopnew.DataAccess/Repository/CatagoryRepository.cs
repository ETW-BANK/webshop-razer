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
  public class CatagoryRepository:Repository<Catagory>,ICatagoryRepository 
    {

        private ApplicationDbContext _context;

        public CatagoryRepository(ApplicationDbContext context ):base(context) 
        {
            _context = context;
        }

      

        public void Update(Catagory catagory)
        {
           _context.Update(catagory);
        }
    }
}
