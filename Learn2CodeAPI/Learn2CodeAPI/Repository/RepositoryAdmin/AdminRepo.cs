using Learn2CodeAPI.Data;
using Learn2CodeAPI.IRepository.IRepositoryAdmin;
using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Repository.RepositoryAdmin
{
    public class AdminRepo : IAdmin
    {
        private readonly AppDbContext db;
        private readonly UserManager<AppUser> _userManager;

        public AdminRepo(AppDbContext _db, UserManager<AppUser> userManager)
        {
            db = _db;
            _userManager = userManager;

        }

        #region university

        public async Task<University> GetByName(string Name)
        {
            var university = await db.University.Where(zz => zz.UniversityName == Name).FirstOrDefaultAsync(); ;
            return university;
        }

        #endregion

        #region Degree
        public async Task<IEnumerable<Degree>> GetAllDegrees(int UniversityId)
        {
            var degrees = await db.Degrees.Where(zz => zz.UniversityId == UniversityId).ToListAsync();
            return degrees;
        }

        public async Task<Degree> GetByDegreeName(string Name)
        {
            var degree = await db.Degrees.Where(zz => zz.DegreeName == Name).FirstOrDefaultAsync(); 
            return degree;
        }


        #endregion

        #region Module
        public async Task<Module> GetByModuleName(string Name)
        {
            var module = await db.Modules.Where(zz => zz.ModuleCode == Name).FirstOrDefaultAsync(); ;
            return module;
        }

        public async Task<IEnumerable<Module>> GetAllModules(int DegreeId)
        {
            var modules = await db.Modules.Where(zz => zz.DegreeId == DegreeId).ToListAsync();
            return modules;
        }


        #endregion

        #region CourseFolder
        public async Task<CourseFolder> GetByCourseFolderName(string Name)
        {
            var Coursefolder = await db.courseFolders.Where(zz => zz.CourseFolderName == Name).FirstOrDefaultAsync(); ;
            return Coursefolder;
        }


        #endregion

        #region Students
        public async Task<IEnumerable<Student>> GetAllStudents()
        {

            var Students = await db.Students.Include(zz => zz.StudentModule).ThenInclude(StudentModule => StudentModule.Module.Degree.University).ToListAsync(); 
            return Students;
        }
        #endregion








    }
}
