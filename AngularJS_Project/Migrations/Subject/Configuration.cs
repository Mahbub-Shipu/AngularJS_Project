namespace AngularJS_Project.Migrations.Subject
{
    using AngularJS_Project.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AngularJS_Project.Models.SubjectDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Subject";
        }

        protected override void Seed(AngularJS_Project.Models.SubjectDbContext db)
        {
            Department d = new Department { DepartmentName = "Accounting"};
            d.Teachers.Add(new Teacher { TeacherName = "Md. Muinuddin Khan", Designation = Designation.Chairman });
            d.Teachers.Add(new Teacher { TeacherName = "Md. Iqbal Hossain", Designation = Designation.Dean });
            d.Teachers.Add(new Teacher { TeacherName = "Md. Saifullah", Designation = Designation.Professor });
            d.Students.Add(new Student { StudentName = "Md. Mahbubul Alom", Birthday = DateTime.Parse("1992-08-12").Date, Address = "Dhaka",Email = "shipu0159@gmail.com",});
            db.Departments.Add(d);
            db.SaveChanges();
        }
    }
}
