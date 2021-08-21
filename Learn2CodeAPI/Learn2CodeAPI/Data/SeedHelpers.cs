using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Login.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Data
{


    public class SeedHelpers 

    {

        public static void SeedDb(AppDbContext context, UserManager<AppUser> userManeger)
        {

            if (context.Roles.ToList().Count == 0)
            {
                IdentityRole role = new IdentityRole
                {

                    Name = "Student",
                    NormalizedName = "STUDENT"
                };
                context.Roles.Add(role);
                IdentityRole tutor = new IdentityRole
                {

                    Name = "Tutor",
                    NormalizedName = "TUTOR"
                };
                context.Roles.Add(tutor);
                IdentityRole admin = new IdentityRole
                {

                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };
                context.Roles.Add(admin);
                context.SaveChangesAsync().Wait();


            }


            if (context.Admin.ToList().Count == 0)
            {
                var name = new University
                {
                    UniversityName = "sssssssssss"
                };
                context.University.Add(name);
                var appUser = new AppUser
                {
                    Id = "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    NormalizedEmail = "ADMIN@GMAIL.COM"
                };

                userManeger.CreateAsync(appUser, "TutorDevOpsAdmin21!!").Wait();
                userManeger.AddToRoleAsync(appUser, "Admin").Wait();

                var adminuser = new Admin
                {

                    UserId = "02174cf0–9412–4cfe - afbf - 59f706d72cf6"
                };

                context.Admin.Add(adminuser);


                userManeger.AddToRoleAsync(appUser, "Admin");
                context.SaveChangesAsync().Wait();
               
            }

        }
    }
}
