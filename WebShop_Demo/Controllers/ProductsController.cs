using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop_Demo.DAL;
using WebShop_Demo.Models;
using WebShop_Demo.Repository;

namespace WebShop_Demo.Controllers
{
    public class ProductsController : Controller
    {
        private GenericUnit _unitOfWork = new GenericUnit();
        private Product productList = new Product();

        [HttpGet]
        public ActionResult Products()
        {
            productList.Products = GetProductFromDB();

            //Add data in memory
            if (System.Web.HttpContext.Current.Cache["product"] == null)
            {
                System.Web.HttpContext.Current.Cache["product"] = productList.Products;
            }

            return View(productList);
        }

        [HttpPost]
        public ActionResult Products(Product product)
        {
            bool Cache = product.DataFromDB;
            ModelState.Clear();

            if (Cache)
            {
                product.Products = ((List<WebShop_Demo.DAL.Products>)System.Web.HttpContext.Current.Cache["product"]);
            }
            else
            {
                product.Products = GetProductFromDB();
            }

            return View(product);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(WebShop_Demo.DAL.Products product)
        {
            ViewBag.ErrorMessage = "";

            int lastRowID = GetLastRowIdFromDB();

            product.RowId = lastRowID +1;
            product.CategoryId = 1;
            product.Created = DateTime.Now;
            product.LastModified = DateTime.Now;
            product.Active = true;

            bool rowAdded = AddRowInDB(product);

            if (rowAdded)
            {
                ViewBag.ErrorMessage = "OK";
                ModelState.Clear();
                product = new Products();
            }
            else
            {
                ViewBag.ErrorMessage = "Error";
            }

            return View(product);
        }


        //Obtain Data from DB
        private List<WebShop_Demo.DAL.Products> GetProductFromDB()
        {
            List<WebShop_Demo.DAL.Products> products =
                _unitOfWork.GetInstanceRepository<WebShop_Demo.DAL.Products>()
                    .GetAllRowsIQueryable()
                    .OrderBy(i => i.ProductId)
                    .ToList();

            return products;
        }

        //Obtain Last RowID from DB
        private int GetLastRowIdFromDB()
        {
            productList.Products = GetProductFromDB();
            int lastRowId = productList.Products.Max(i => i.RowId);
            return lastRowId;
        }

        //Add row in DB 
        private bool AddRowInDB(WebShop_Demo.DAL.Products product)
        {
            _unitOfWork.GetInstanceRepository<WebShop_Demo.DAL.Products>().Add(product);
            int row = _unitOfWork.SaveChanges();
            if (row == 0) return true;
            return false;
        }

    }
}