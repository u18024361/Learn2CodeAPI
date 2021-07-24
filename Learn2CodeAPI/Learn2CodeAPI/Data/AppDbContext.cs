using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentModule>()
                .HasKey(bc => new { bc.StudentId, bc.ModuleId });
            modelBuilder.Entity<StudentModule>()
                .HasOne(bc => bc.Student)
                .WithMany(b => b.StudentModule)
                .HasForeignKey(bc => bc.StudentId);
            modelBuilder.Entity<StudentModule>()
                .HasOne(bc => bc.Module)
                .WithMany(c => c.StudentModule)
                .HasForeignKey(bc => bc.ModuleId);
        }

        public DbSet<University> University { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentModule> StudentModule { get; set; }
    }
}
