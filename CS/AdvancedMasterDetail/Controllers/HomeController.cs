using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using AdvancedMasterDetail.Models;

namespace AdvancedMasterDetail.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
      
        NorthwindClassesDataContext context = new NorthwindClassesDataContext();
        public ActionResult Index()
        {
           
            return View();
        }
      
        [ValidateInput(false)]
        public ActionResult MasterGridViewPartial()
        {
            
            var model = context.Suppliers;
            return PartialView("_MasterGridViewPartial", model);
        }
        public ActionResult PageControlPartial(int key)
        {
            ViewData["key"] = key;
            var model = context.Suppliers.Where(item => item.SupplierID == key).FirstOrDefault();
            return PartialView("DetailPageControl", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MasterGridViewPartialAddNew(AdvancedMasterDetail.Models.Supplier item)
        {
            var model = context.Suppliers;
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    context.Suppliers.InsertOnSubmit(item);
                    context.SubmitChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_MasterGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MasterGridViewPartialUpdate(AdvancedMasterDetail.Models.Supplier item)
        {
            var model = context.Suppliers;
            if (ModelState.IsValid)
            {
                try
                {
                    var existingItem = context.Suppliers.Where(x => x.SupplierID == item.SupplierID).FirstOrDefault();
                    existingItem.CompanyName = item.CompanyName;
                    existingItem.ContactName = item.ContactName;
                    existingItem.Country = item.Country;
                    existingItem.City = item.City;
                    context.SubmitChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_MasterGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MasterGridViewPartialDelete(System.Int32 SupplierID)
        {
            var model = context.Suppliers;
            if (SupplierID >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    var existingItem = context.Suppliers.Where(x => x.SupplierID == SupplierID).FirstOrDefault();
                    context.Suppliers.DeleteOnSubmit(existingItem);
                    context.SubmitChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_MasterGridViewPartial", model);
        }



        [ValidateInput(false)]
        public ActionResult ProductsGridViewPartial(int key)
        {
            var model = context.Products.Where(item => item.SupplierID == key);
            ViewData["productKey"] = key;
            return PartialView("_ProductsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProductsGridViewPartialAddNew(AdvancedMasterDetail.Models.Product item, int key)
        {
            var model = context.Products;
            ViewData["productKey"] = key;
            if (ModelState.IsValid)
            {
                try
                {
                    item.SupplierID = key;
                    item.Supplier = context.Suppliers.Where(x => x.SupplierID == key).FirstOrDefault();
                    item.Category = context.Categories.Where(category => category.CategoryID == item.CategoryID).FirstOrDefault();
                    model.InsertOnSubmit(item);
                    context.SubmitChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var querymodel = context.Products.Where(it => it.SupplierID == key);
            return PartialView("_ProductsGridViewPartial", querymodel);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProductsGridViewPartialUpdate(AdvancedMasterDetail.Models.Product item, int key)
        {

            var model = context.Products;
            ViewData["productKey"] = key;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.ProductID == item.ProductID);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        context.SubmitChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var querymodel = context.Products.Where(it => it.SupplierID == key);
            return PartialView("_ProductsGridViewPartial", querymodel);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProductsGridViewPartialDelete(System.Int32 ProductID, int key)
        {

            var model = context.Products;
            ViewData["productKey"] = key;
            if (ProductID >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.ProductID == ProductID);
                    if (item != null)
                        model.DeleteOnSubmit(item);
                    context.SubmitChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var querymodel = context.Products.Where(item => item.SupplierID == key);
            return PartialView("_ProductsGridViewPartial", querymodel);
        }


        [ValidateInput(false)]
        public ActionResult CategoryGridViewPartial(int key)
        {
            ViewData["categoryKey"] = key;
            var model = from a in context.Categories
                        where
                            (from b in context.Products
                             where
                                 b.CategoryID == a.CategoryID && b.SupplierID == key
                             select 1).Any()
                        select a;

            return PartialView("_CategoryGridViewPartial", model);
        }


        [ValidateInput(false)]
        public ActionResult OrdersGridViewPartial(int key)
        {
            ViewData["orderKey"] = key;
            var model = context.Order_Details.Where(item => item.ProductID == key);
            return PartialView("_OrdersGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult OrdersGridViewPartialAddNew(AdvancedMasterDetail.Models.Order_Detail item, int key)
        {
            var model = context.Order_Details;
            ViewData["orderKey"] = key;
            if (ModelState.IsValid)
            {
                try
                {
                    item.ProductID = key;
                    item.Product = context.Products.Where(productItem => productItem.ProductID == key).FirstOrDefault();
                    model.InsertOnSubmit(item);
                    context.SubmitChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var querymodel = context.Order_Details.Where(order => order.ProductID == key);

            return PartialView("_OrdersGridViewPartial", querymodel);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult OrdersGridViewPartialUpdate(AdvancedMasterDetail.Models.Order_Detail item, int key)
        {
            var model = context.Order_Details;
            ViewData["orderKey"] = key;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.OrderID == item.OrderID);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        context.SubmitChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            var querymodel = context.Order_Details.Where(order => order.ProductID == key);
            return PartialView("_OrdersGridViewPartial", querymodel);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult OrdersGridViewPartialDelete(System.Int32 OrderID, int key)
        {
            var model = context.Order_Details;
            ViewData["orderKey"] = key;
            if (OrderID >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.OrderID == OrderID);
                    if (item != null)
                        model.DeleteOnSubmit(item);
                    context.SubmitChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var querymodel = context.Order_Details.Where(order => order.ProductID == key);
            return PartialView("_OrdersGridViewPartial", querymodel);
        }
        public static IQueryable GetCategories()
        {
            return new NorthwindClassesDataContext().Categories;
        }
    }

}
