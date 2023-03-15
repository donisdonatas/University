using Microsoft.EntityFrameworkCore;
using UniExam.Repository.Models;

namespace UniExam.Repository.Data
{
    public class UniContext : DbContext
    {
        public UniContext(DbContextOptions<UniContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
