using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FarmApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace FarmApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CreateRolesAndUsers();
        }

        private static void CreateRolesAndUsers()
        {
            var context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //creates emmployee role
            if (!roleManager.RoleExists("Employee"))
                roleManager.Create(new IdentityRole("Employee"));

            //creates farmer role
            if (!roleManager.RoleExists("Farmer"))
                roleManager.Create(new IdentityRole("Farmer"));

            //creates admin account with employee role
            if (userManager.FindByName("admin@farm.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@farm.com",
                    Email = "admin@farm.com"
                };

                var result = userManager.Create(user, "Admin@123");
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Employee");
                }
            }
        }
    }
}
