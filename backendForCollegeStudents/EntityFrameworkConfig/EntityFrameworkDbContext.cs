using backendForCollegeStudents.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace backendForCollegeStudents.EntityFrameworkConfig
{
    public class EntityFrameworkDbContext:DbContext
    {
        //public EntityFramework(DbContextOptions<EntityFramework> options)
        //{

        //}
        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    base.ConfigureConventions(configurationBuilder);
        //    configurationBuilder.
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connStr = "Server=.;Database=myshools;Encrypt=True;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connStr);
           

        }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<VerificationCode>  VerificationCodes { get; set; }
    }
}
