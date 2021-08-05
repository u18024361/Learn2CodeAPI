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
                .HasOne(bc => bc.Tutor)
                .WithMany(c => c.StudentModule)
                .HasForeignKey(bc => bc.ModuleId);

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<StudentModule>()
            //.HasKey(bc => new { bc.StudentId, bc.ModuleId });
            modelBuilder.Entity<Message>()
                .HasOne(bc => bc.student)
                .WithMany(b => b.message)
                .HasForeignKey(bc => bc.StudentId);
            modelBuilder.Entity<Message>()
                .HasOne(bc => bc.tutor)
                .WithMany(c => c.message)
                .HasForeignKey(bc => bc.TutorId);



            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<StudentModule>()
            //.HasKey(bc => new { bc.StudentId, bc.ModuleId });
            modelBuilder.Entity<TutorModule>()
                .HasOne(bc => bc.Tutor)
                .WithMany(b => b.TutorModule)
                .HasForeignKey(bc => bc.TutorId);
            modelBuilder.Entity<TutorModule>()
                .HasOne(bc => bc.Module)
                .WithMany(c => c.TutorModule)
                .HasForeignKey(bc => bc.ModuleId);

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<StudentModule>()
            //.HasKey(bc => new { bc.StudentId, bc.ModuleId });
            modelBuilder.Entity<TutorSessionModule>()
                .HasOne(bc => bc.TutorSession)
                .WithMany(b => b.TutorSessionModule)
                .HasForeignKey(bc => bc.TutorSessionId);
            modelBuilder.Entity<TutorSessionModule>()
                .HasOne(bc => bc.Module)
                .WithMany(c => c.TutorSessionModule)
                .HasForeignKey(bc => bc.ModuleId);

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<StudentModule>()
            //.HasKey(bc => new { bc.StudentId, bc.ModuleId });
            modelBuilder.Entity<SubscriptionTutorSession>()
                .HasOne(bc => bc.TutorSession)
                .WithMany(b => b.SubscriptionTutorSession)
                .HasForeignKey(bc => bc.TutorSessionId);
            modelBuilder.Entity<SubscriptionTutorSession>()
                .HasOne(bc => bc.Subscription)
                .WithMany(c => c.SubscriptionTutorSession)
                .HasForeignKey(bc => bc.SubscriptionId);

        

            //modelBuilder.Entity<StudentModule>()
            //.HasKey(bc => new { bc.StudentId, bc.ModuleId });

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<StudentModule>()
            //.HasKey(bc => new { bc.StudentId, bc.ModuleId });
            modelBuilder.Entity<Resource>()
                .HasOne(bc => bc.Module)
                .WithMany(b => b.Resource)
                .HasForeignKey(bc => bc.ModuleId);
            modelBuilder.Entity<Resource>()
                .HasOne(bc => bc.ResourceCategory)
                .WithMany(c => c.Resource)
                .HasForeignKey(bc => bc.ResourceCategoryId);




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

        public DbSet<Message> Message { get; set; }
        public DbSet<CourseContent> CourseContent { get; set; }

        public DbSet<TutorSessionModule> TutorSessionModule { get; set; }
        public DbSet<TutorSession> TutorSession { get; set; }
        public DbSet<SessionType> SessionType { get; set; }

        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<SubscriptionTutorSession> SubscriptionTutorSession { get; set; }
        public DbSet<TutorModule> TutorModule { get; set; }
        public DbSet<ResourceCategory> ResourceCategory { get; set; }
        public DbSet<TutorSessionModuleTutor> TutorSessionModuleTutor { get; set; }
        public DbSet<SessionTime> SessionTime { get; set; }
        public DbSet<BookingStatus> BookingStatus { get; set; }
        public DbSet<BookingInstance> BookingInstance { get; set; }
        public DbSet<Payment> Payment{ get; set; }

        public DbSet<Resource> Resource { get; set; }


    }
}
