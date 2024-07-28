using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WEbshopnew.DataAccess.Data;
using WEbshopnew.DataAccess.Repository.IRepository;

namespace WEbshopnew.DataAccess.Repository
{
    public class Repository <T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        internal DbSet<T> set;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.set=_context.Set<T>();
            _context.Products.Include(u => u.Category);
        }
        public void Add(T entity)
        {
          set.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeproperties = null)
        {
            IQueryable<T> query = set;
            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeproperties))
            {
                if (!string.IsNullOrEmpty(includeproperties))
                {

                    foreach (var item in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(item);
                    }

                }
            }
       

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeproperties=null)
        {

         
           IQueryable<T> query=set;
            if (!string.IsNullOrEmpty(includeproperties))
            {

                foreach(var item in includeproperties.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries)) 
                {
                 query=query.Include(item);
                }
               
            }
            return query.ToList();
        }

       

        public void Remove(T entity)
        {
         set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            set.RemoveRange(entities);
        }
    }
}
