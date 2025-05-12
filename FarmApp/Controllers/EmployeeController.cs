using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel;

namespace FarmApp.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult AddFarmer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFarmer(string email, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser { UserName = email, Email = email };
            var result = userManager.Create(user, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, "Farmer");
                ViewBag.Message = "Farmer user created successfully.";
            }
            else
            {
                ViewBag.Message = "Error creating farmer: " + string.Join(", ", result.Errors);
            }
            return View();
        }

        public ActionResult ViewAll(string farmerId = null, string category = null, DateTime? production = null)
        {
            var farmers = db.Users.Where(u => u.Roles.Any(r => r.RoleId == "Farmer")).ToList();
            ViewBag.Farmers = new SelectList(farmers, "Id", "Email");

            IQueryable<Product> products = db.Products.Include("Farmer");
            if (!string.IsNullOrEmpty(farmerId))
            {
                products = products.Where(p => p.FarmerId == farmerId);
            }

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.Contains(category));
            }

            if (production.HasValue)
            {
                products = products.Where(p => p.ProductionDate.Date == production.Value.Date);
            }

            return View(products.ToList());
        }
    }
}