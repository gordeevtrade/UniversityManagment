using Microsoft.EntityFrameworkCore;

namespace University.DAL
{
    public class UniversityContext : DbContext
    {
        public DbSet<Groups> Groups { get; set; } = null!;
        public DbSet<Cours> Courses { get; set; } = null!;
        public DbSet<Students> Students { get; set; } = null!;

        public UniversityContext(DbContextOptions<UniversityContext> options)
          : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    Cours english = new Cours { Course_ID = 1, Name = "Englis", Description = "AAA" };
        //    Groups group1 = new Groups { Group_ID = 1, Name = "Sr-001", Course_ID = 1 };
        //    Students student1 = new Students { Student_ID = 1, First_Name = "Alex", Last_Name = "Smith", Group_ID = 1 };

        //    modelBuilder.Entity<Cours>().HasData(english);
        //    modelBuilder.Entity<Groups>().HasData(group1);
        //    modelBuilder.Entity<Students>().HasData(student1);
        //}
    }
}