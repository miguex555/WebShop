using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop_Demo.Repository
{
    public interface IRepository<Table_Entity> where Table_Entity : class
    {
        IEnumerable<Table_Entity> GetAllRows();

        IQueryable<Table_Entity> GetAllRowsIQueryable();

        IQueryable<Table_Entity> GetLastRowIQueryable();

        int Add(Table_Entity entity);
    }
}