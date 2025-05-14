using KNC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KNC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Faculty> Faculty { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }

        public virtual DbSet<EducationPrograms> EducationPrograms { get; set; }
        public virtual DbSet<PaymentInfo> PaymentInfo { get; set; }
        public DbSet<KNC.Models.Certificates> Certificates { get; set; } = default!;
/*        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }*/
    }
    

}
