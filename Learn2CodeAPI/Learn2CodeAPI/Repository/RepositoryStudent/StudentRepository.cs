﻿using AutoMapper;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.StudentDto;
using Learn2CodeAPI.IRepository.IRepositoryStudent;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Repository.RepositoryStudent
{
    public class StudentRepository : IStudent
    {
        private readonly AppDbContext db;
        private readonly UserManager<AppUser> _userManager;
        
        public StudentRepository(AppDbContext _db, UserManager<AppUser> userManager)
        {
            db = _db;
            _userManager = userManager;
            
        }
        public async Task<Student> Register(AppUser userIdentity, RegistrationDto model)
        {
            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
            {
                return null;
            }

            try
            {
                Student student = new Student();
                student.UserId = userIdentity.Id;
                student.StudentName = model.StudentName;
                student.StudentSurname = model.StudentSurname;
                student.StudentCell = model.StudentCell;
                await db.Students.AddAsync(student);
                await db.SaveChangesAsync();

                //await _appDbContext.Students.AddAsync(new Student { UserId = userIdentity.Id, StudentName = model.StudentName,
                //     StudentSurname = model.StudentSurname, StudentCell = model.StudentCell });
                await db.StudentModule.AddAsync(new StudentModule
                {
                    StudentId = student.Id,
                    ModuleId = model.ModuleId
                });
                //await _userManager.AddToRoleAsync(userIdentity, "Student");
                await db.SaveChangesAsync();


                return student;
            }
            catch (Exception )
            {
                return null;
            }
        }

        public async Task<Student> UpdateProfile(UpdateStudent dto)
        {
            //student table
            var student = db.Students.Where(zz => zz.Id == dto.StudentId).FirstOrDefault();
            student.StudentCell = dto.StudentCell;
            student.StudentName = dto.StudentName;
            student.StudentSurname = dto.StudentSurname;
            student.UserId = student.UserId;

            //StudentModule
            var studentModule = db.StudentModule.Where(zz => zz.StudentId == dto.StudentId).FirstOrDefault();
            studentModule.ModuleId = dto.ModuleId;
            studentModule.StudentId = dto.StudentId;

            //user table
            var user = db.Users.Where(zz => zz.Id == student.UserId).FirstOrDefault();
            user.Email = dto.Email;
            user.NormalizedEmail = dto.Email.ToUpper();
            user.NormalizedUserName = dto.UserName.ToUpper();
            user.UserName = dto.UserName;
            await db.SaveChangesAsync();
            return student;
        }
    }
}
