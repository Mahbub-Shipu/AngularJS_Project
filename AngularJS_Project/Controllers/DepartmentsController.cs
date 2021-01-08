using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AngularJS_Project.Models;

namespace AngularJS_Project.Controllers
{
    public class DepartmentsController : ApiController
    {
        private SubjectDbContext db = new SubjectDbContext();

        // GET: api/Departments
        public IQueryable<Department> GetDepartments()
        {
            return db.Departments;
        }

        // GET: api/Departments/5
        [ResponseType(typeof(Department))]
        public IHttpActionResult GetDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // PUT: api/Departments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartment(int id, Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            var ext = db.Departments.Include(x => x.Students).Include(y => y.Teachers).First(x => x.DepartmentId == department.DepartmentId);
            ext.DepartmentName = department.DepartmentName;
            //ext.Address = company.Address;
            //ext.Eshtablished = company.Eshtablished;
            //ext.ContactEmail = company.ContactEmail;
            //ext.Web = company.Web;
            if (department.Students != null && department.Students.Count > 0)
            {
                var prs = department.Students.ToArray();
                for (var i = 0; i < prs.Length; i++)
                {
                    var temp = ext.Students.FirstOrDefault(c => c.StudentId == prs[i].StudentId);
                    if (temp != null)
                    {
                        temp.StudentName = prs[i].StudentName;
                        temp.Birthday = prs[i].Birthday;
                        temp.Email = prs[i].Email;
                        temp.Address = prs[i].Address;
                    }
                    else
                    {
                        ext.Students.Add(prs[i]);
                    }
                }
                prs = ext.Students.ToArray();
                for (var i = 0; i < prs.Length; i++)
                {
                    var temp = department.Students.FirstOrDefault(x => x.StudentId == prs[i].StudentId);
                    if (temp == null)
                        db.Entry(prs[i]).State = EntityState.Deleted;
                }
            }
            if (department.Teachers != null && department.Teachers.Count > 0)
            {
                var srvs = department.Teachers.ToArray();
                for (var i = 0; i < srvs.Length; i++)
                {
                    var temp = ext.Teachers.FirstOrDefault(c => c.TeacherId == srvs[i].TeacherId);
                    if (temp != null)
                    {
                        temp.TeacherName = srvs[i].TeacherName;
                        temp.Designation = srvs[i].Designation;
                    }
                    else
                    {
                        ext.Teachers.Add(srvs[i]);
                    }
                }
                srvs = ext.Teachers.ToArray();
                for (var i = 0; i < srvs.Length; i++)
                {
                    var temp = department.Teachers.FirstOrDefault(x => x.TeacherId == srvs[i].TeacherId);
                    if (temp == null)
                        db.Entry(srvs[i]).State = EntityState.Deleted;
                }
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Departments
        [ResponseType(typeof(Department))]
        public IHttpActionResult PostDepartment(Department department)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.Departments.Add(department);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/5
        [ResponseType(typeof(Department))]
        public IHttpActionResult DeleteDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            db.Departments.Remove(department);
            db.SaveChanges();

            return Ok(department);
        }
        [Route("custom/Departments")]
        public IQueryable<Department> GetDepartmentsWithRelated()
        {
            return db.Departments
                .Include("Students")
                .Include("Teachers");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [Route("custom/Departments/{id}")]
        public IHttpActionResult GetDepartmentWithRelated(int id)
        {
            return Ok(db.Departments
                    .Include(x => x.Students)
                    .Include(x => x.Teachers)
                    .FirstOrDefault(x => x.DepartmentId == id));
        }
        private bool DepartmentExists(int id)
        {
            return db.Departments.Count(e => e.DepartmentId == id) > 0;
        }
    }
}