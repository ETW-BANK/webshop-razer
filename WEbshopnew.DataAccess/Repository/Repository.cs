using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }
        public void Add(T entity)
        {
          set.Add(entity);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = set;
            query=query.Where(filter);

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
           IQueryable<T> query=set;
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
