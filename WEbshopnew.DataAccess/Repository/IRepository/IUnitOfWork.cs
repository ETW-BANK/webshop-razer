using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEbshopnew.DataAccess.Repository.IRepository
{
  public interface IUnitOfWork
    {
        ICatagoryRepository Catagory { get; }
        IProductsRepository Product { get; }
        void Save();
    }
}
