﻿// <auto-generated />
using System;
using Learn2CodeAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Learn2CodeAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Admin");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UserId = "02174cf0–9412–4cfe - afbf - 59f706d72cf6"
                        });
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.ContentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContentType");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.CourseContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContentTypeId")
                        .HasColumnType("int");

                    b.Property<int>("CourseSubCategoryId")
                        .HasColumnType("int");

                    b.Property<byte[]>("DataFiles")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Filepath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContentTypeId");

                    b.HasIndex("CourseSubCategoryId");

                    b.ToTable("CourseContent");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.CourseFolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("CourseFolderName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("courseFolders");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.CourseSubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseFolderId")
                        .HasColumnType("int");

                    b.Property<string>("CourseSubCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CourseFolderId");

                    b.ToTable("courseSubCategory");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.Degree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DegreeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UniversityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UniversityId");

                    b.ToTable("Degrees");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseFolderId")
                        .HasColumnType("int");

                    b.Property<int>("DegreeId")
                        .HasColumnType("int");

                    b.Property<string>("ModuleCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseFolderId");

                    b.HasIndex("DegreeId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.SessionContentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("SessionContentCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("SessionContentCategory");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.University", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UniversityName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("University");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Login.Identity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1ca836d3-53e1-4669-acf5-bde2690092b3",
                            Email = "Admin@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEBPlptz73t1n/fVo8IYDFmVe/vuuH5UCH3lJbj4dpUvWNKX+j/ap5QBN/nD91n61Ug==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "51e534d8-4bd5-48c6-af72-b53459ce8174",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Student.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StudentCell")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Student.StudentModule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentModule");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Tutor.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("File");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Tutor.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MessageSent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("TimeStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TutorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TutorId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Tutor.Tutor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FileId")
                        .HasColumnType("int");

                    b.Property<string>("TutorAbout")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorCell")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TutorStatusId")
                        .HasColumnType("int");

                    b.Property<string>("TutorSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("TutorStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Tutor");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Tutor.TutorStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TutorStatusDesc")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TutorStatus");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.Admin", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Login.Identity.AppUser", "Identity")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Identity");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.CourseContent", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Admin.ContentType", "ContentType")
                        .WithMany("CourseContent")
                        .HasForeignKey("ContentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learn2CodeAPI.Models.Admin.CourseSubCategory", "CourseSubCategory")
                        .WithMany("CourseContent")
                        .HasForeignKey("CourseSubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContentType");

                    b.Navigation("CourseSubCategory");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.CourseFolder", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Admin.Admin", "admin")
                        .WithMany("courseFolder")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("admin");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.CourseSubCategory", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Admin.CourseFolder", "courseFolder")
                        .WithMany()
                        .HasForeignKey("CourseFolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("courseFolder");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.Degree", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Admin.University", "University")
                        .WithMany("Degree")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("University");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.Module", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Admin.CourseFolder", null)
                        .WithMany("Module")
                        .HasForeignKey("CourseFolderId");

                    b.HasOne("Learn2CodeAPI.Models.Admin.Degree", "Degree")
                        .WithMany("Module")
                        .HasForeignKey("DegreeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Degree");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.SessionContentCategory", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Admin.Admin", "admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("admin");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Student.Student", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Login.Identity.AppUser", "Identity")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Identity");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Student.StudentModule", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Admin.Module", "Module")
                        .WithMany("StudentModule")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learn2CodeAPI.Models.Student.Student", "Students")
                        .WithMany("StudentModule")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Tutor.Message", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Student.Student", "student")
                        .WithMany("message")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learn2CodeAPI.Models.Tutor.Tutor", "tutor")
                        .WithMany("message")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("student");

                    b.Navigation("tutor");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Tutor.Tutor", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Tutor.File", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learn2CodeAPI.Models.Tutor.TutorStatus", "TutorStatus")
                        .WithMany("Tutors")
                        .HasForeignKey("TutorStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learn2CodeAPI.Models.Login.Identity.AppUser", "Identity")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("File");

                    b.Navigation("Identity");

                    b.Navigation("TutorStatus");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Login.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Login.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learn2CodeAPI.Models.Login.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Learn2CodeAPI.Models.Login.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.Admin", b =>
                {
                    b.Navigation("courseFolder");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.ContentType", b =>
                {
                    b.Navigation("CourseContent");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.CourseFolder", b =>
                {
                    b.Navigation("Module");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.CourseSubCategory", b =>
                {
                    b.Navigation("CourseContent");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.Degree", b =>
                {
                    b.Navigation("Module");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.Module", b =>
                {
                    b.Navigation("StudentModule");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Admin.University", b =>
                {
                    b.Navigation("Degree");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Student.Student", b =>
                {
                    b.Navigation("message");

                    b.Navigation("StudentModule");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Tutor.Tutor", b =>
                {
                    b.Navigation("message");
                });

            modelBuilder.Entity("Learn2CodeAPI.Models.Tutor.TutorStatus", b =>
                {
                    b.Navigation("Tutors");
                });
#pragma warning restore 612, 618
        }
    }
}
