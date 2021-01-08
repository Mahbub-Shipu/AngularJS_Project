namespace AngularJS_Project.Migrations.Subject
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subject_InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false, maxLength: 50),
                        Birthday = c.DateTime(nullable: false, storeType: "date"),
                        Email = c.String(nullable: false, maxLength: 40),
                        Address = c.String(nullable: false, maxLength: 50),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        TeacherName = c.String(nullable: false, maxLength: 40),
                        Designation = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Students", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Teachers", new[] { "DepartmentId" });
            DropIndex("dbo.Students", new[] { "DepartmentId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Departments");
        }
    }
}
