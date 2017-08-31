using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop_Demo.DAL;

namespace WebShop_Demo.Models
{
    public class Product
    {
        public bool DataFromDB { get; set; }
        public bool ProductCreated { get; set; }

        public List<WebShop_Demo.DAL.Products> Products { get; set; }
    }
}