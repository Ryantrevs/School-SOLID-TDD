using Case_School.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Case_School.Data
{
    public class CaseSchoolContext : DbContext
    {
        
        public CaseSchoolContext(DbContextOptions options) :base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            const string ConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=SchoolDb;Integrated Security=SSPI;Integrated Security=True";
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //class "Class" mapping
            builder.Entity<Class>().HasKey(x => x.Id);
            builder.Entity<Class>().HasMany(x => x.Students).WithOne(z => z.Class);

            //class "Student" mapping
            builder.Entity<Student>().HasKey(x => x.Registration);
            builder.Entity<Student>().HasMany(x => x.StudentAverages).WithOne(z => z.Student);
            builder.Entity<Student>().HasMany(x => x.StudentGrades).WithOne(z => z.Student);

            //class "StudentAverages" mapping
            builder.Entity<StudentAverages>().HasKey(x => x.Id);
            builder.Entity<StudentAverages>().HasOne(x => x.Subject).WithOne(z => z.StudentAverage).HasForeignKey<StudentAverages>(y => y.SubjectId);

            //class "StudentGrade" mapping
            builder.Entity<StudentGrade>().HasKey(x => x.Id);
            builder.Entity<StudentGrade>().HasOne(x => x.Subject).WithOne(z => z.StudentGrade).HasForeignKey<StudentGrade>(y => y.SubjectId);
            
            //class "Subject" mapping
            builder.Entity<Subject>().HasKey(x => x.Id);
            builder.Entity<Subject>().HasMany(x => x.WeightProof).WithOne(z => z.Subject);

            //class "WeightProof" mapping
            builder.Entity<WeightProof>().HasIndex(x => x.Id);

        }

        public DbSet<Class> Class { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentAverages> StudentAverages { get; set; }
        public DbSet<StudentGrade> StudentGrade { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<WeightProof> WeightProof { get; set; }

    }
}
