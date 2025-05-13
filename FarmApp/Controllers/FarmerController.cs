using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using FarmApp.Models;
using Microsoft.AspNet.Identity;

namespace FarmApp.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase productImage)
        {
            var userId = User.Identity.GetUserId();
            product.FarmerId = userId;

            if (productImage != null && productImage.ContentLength > 0)
            {
                var fileName = Path.GetFileName(productImage.FileName);
                var folderPath = Server.MapPath("~/Uploads");
                var filePath = Path.Combine(folderPath, fileName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                productImage.SaveAs(filePath);
                product.ImagePath = "/Uploads/" + fileName;
            }
            db.Products.Add(product);
            db.SaveChanges();
            ViewBag.Message = "Product added successfully.";

            return RedirectToAction("MyProducts");
        }

        public ActionResult MyProducts()
        {
            var userId = User.Identity.GetUserId();
            var products = db.Products.Where(p => p.FarmerId == userId).ToList();
            return View(products);
        }


    }
}