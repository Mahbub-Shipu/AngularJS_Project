using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngularJS_Project.Models
{
    public enum Designation { Chairman, Dean, Professor, AssistantProfessor, Lecturer }
    public class Department
    {
        public Department()
        {
            this.Teachers = new HashSet<Teacher>();
            this.Students = new HashSet<Student>();
        }
        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }
        [Required, StringLength(40), Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
    public class Teacher
    {
        [Display(Name = "Teacher Id")]
        public int TeacherId { get; set; }
        [Required, StringLength(40), Display(Name = "Teacher Name")]
        public string TeacherName { get; set; }
        [EnumDataType(typeof(Designation))]
        [JsonConverter(typeof(StringEnumConverter))]
        public Designation Designation { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
    public class Student
    {
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }
        [Required, StringLength(50), Display(Name = "Student Name")]
        public string StudentName { get; set; }
        [Required, Column(TypeName = "date"), DataType(DataType.Date), Display(Name = "Date of Birth"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
        [Required, EmailAddress, StringLength(40)]
        public string Email { get; set; }
        [Required, StringLength(50), Display(Name = "Address")]
        public string Address { get; set; }
        [Required, ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
    public class SubjectDbContext : DbContext
    {
        public SubjectDbContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}