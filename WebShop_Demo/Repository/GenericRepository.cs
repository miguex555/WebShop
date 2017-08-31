using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebShop_Demo.DAL;
using System.Data.Entity;

namespace WebShop_Demo.Repository
{
    public class GenericRepository<Table_Entity> : IRepository<Table_Entity> where Table_Entity : class
    {
        DbSet<Table_Entity> _dbSet;
        private WebDemoDBEntities _dbEntity;

        public GenericRepository(WebDemoDBEntities dbEntity)
        {
            _dbEntity = dbEntity;
            _dbSet = _dbEntity.Set<Table_Entity>();
        }

        public IQueryable<Table_Entity> GetAllRowsIQueryable()
        {
            return _dbSet;
        }

        public IQueryable<Table_Entity> GetLastRowIQueryable()
        {
            return _dbSet;
        }

        public IEnumerable<Table_Entity> GetAllRows()
        {
            return _dbSet.ToList();
        }

        public int Add(Table_Entity entity)
        {
            int row;
            _dbSet.Add(entity);
            try
            {
                row = _dbEntity.SaveChanges();
            }
            catch (Exception e)
            {
                row = -1;
            }
            
            return row;
        }
    }
}