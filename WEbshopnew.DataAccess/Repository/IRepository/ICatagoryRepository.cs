using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEbshopnew.Models;

namespace WEbshopnew.DataAccess.Repository.IRepository
{
  public interface ICatagoryRepository:IRepository<Catagory>
    {
        void Update(Catagory catagory);
        void Save();

    }
}
