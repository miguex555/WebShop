using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop_Demo.DAL;

namespace WebShop_Demo.Repository
{
    public class GenericUnit : IDisposable
    {
        private WebDemoDBEntities dbEntity = new WebDemoDBEntities();

        public IRepository<Table_Entity> GetInstanceRepository<Table_Entity>() where Table_Entity : class
        {
            return new GenericRepository<Table_Entity>(dbEntity);
        }

        public int SaveChanges()
        {
            int row;
            try
            {
                row = dbEntity.SaveChanges();
            }
            catch (Exception e)
            {
                row = -1;
            }
            return row;
        }

        public void Dispose()
        {
            dbEntity.Dispose();
        }
    }
}