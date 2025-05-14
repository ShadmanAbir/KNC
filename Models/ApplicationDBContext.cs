using KNC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KNC.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Certificates> Certificates { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<DropDown> DropDowns { get; set; }
        public DbSet<PaymentInfo> PaymentInfos { get; set; }
        public DbSet<Sessions> Sessions { get; set; }
    }
}
