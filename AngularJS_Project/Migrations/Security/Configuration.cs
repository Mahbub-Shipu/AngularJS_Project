namespace AngularJS_Project.Migrations.Security
{
    using AngularJS_Project.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AngularJS_Project.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Security";
        }

        protected override void Seed(AngularJS_Project.Models.ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser();
            user.UserName = "Admin";
            UserManager.Create(user, "@Open1234");
            base.Seed(context);
        }
    }
}
