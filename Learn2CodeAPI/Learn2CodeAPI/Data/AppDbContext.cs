using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using Microsoft.AspNetCore.Identity;
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

            //modelBuilder.Entity<StudentModule>()
            //.HasKey(bc => new { bc.StudentId, bc.ModuleId });
            modelBuilder.Entity<StudentModule>()
                .HasOne(bc => bc.Students)
                .WithMany(b => b.StudentModule)
                .HasForeignKey(bc => bc.StudentId);
            modelBuilder.Entity<StudentModule>()
                .HasOne(bc => bc.Module)
                .WithMany(c => c.StudentModule)
                .HasForeignKey(bc => bc.ModuleId);

            //create user
            var appUser = new AppUser
            {
                Id = "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                Email = "Admin@gmail.com",
                UserName = "Admin",
                NormalizedUserName ="ADMIN",
                NormalizedEmail = "ADMIN@GMAIL.COM"
            };
            //set user password
            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "TutorDevOpsAdmin21!");

            //seed user
            modelBuilder.Entity<AppUser>().HasData(appUser);


            modelBuilder.Entity<Admin>() .HasData(
             new
             {
                 Id = 1,
                 UserId = "02174cf0–9412–4cfe - afbf - 59f706d72cf6"
             });


        }


        public DbSet<University> University { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentModule> StudentModule { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<CourseFolder> courseFolders { get; set; }

        public DbSet<CourseSubCategory> courseSubCategory { get; set; }
        public DbSet<SessionContentCategory> SessionContentCategory { get; set; }

        public DbSet<Tutor> Tutor{ get; set; }
        public DbSet<TutorStatus> TutorStatus { get; set; }
        public DbSet<File> File { get; set; }

    }
}
