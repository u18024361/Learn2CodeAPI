using AutoMapper;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.AdminDto;
using Learn2CodeAPI.IRepository.IRepositoryAdmin;
using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
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
        private IMapper mapper;
        private readonly AppDbContext db;
        private readonly UserManager<AppUser> _userManager;

        public AdminRepo(AppDbContext _db, UserManager<AppUser> userManager, IMapper _mapper)
        {
            mapper = _mapper;
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

        public async Task<Module> CreateModule(Module module)
        {
            await db.Modules.AddAsync(module);
            await db.SaveChangesAsync();

            int idTrue = await db.TutorSession.Where(zz => zz.SessionType.IsGroup == true).Select(zz => zz.Id).FirstOrDefaultAsync();
            int idFalse = await db.TutorSession.Where(zz => zz.SessionType.IsGroup == false).Select(zz => zz.Id).FirstOrDefaultAsync(); 

            TutorSessionModule TSM = new TutorSessionModule();
            TSM.ModuleId = module.Id;
            TSM.TutorSessionId = idTrue;
            TutorSessionModule TM = new TutorSessionModule();
            TM.ModuleId = module.Id;
            TM.TutorSessionId = idFalse;
            await db.TutorSessionModule.AddAsync(TSM);
            await db.TutorSessionModule.AddAsync(TM);
            await db.SaveChangesAsync();
            return module;

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

            var Students = await db.Students.Include(zz => zz.Identity).Include(zz => zz.StudentModule).ThenInclude(StudentModule => StudentModule.Module.Degree.University).ToListAsync(); 
            return Students;
        }



        #endregion

        #region Tutor
        public async Task<IEnumerable<Tutor>> GetAllApplications()
        {

            var Applicants = await db.Tutor.Include(zz => zz.TutorStatus).Include(zz =>zz.File).Include(zz =>zz.TutorModule).ThenInclude(zz =>zz.Module)
                .Where(zz => zz.TutorStatus.TutorStatusDesc == "Applied").ToListAsync();
            return Applicants;
        }

        public async Task<IEnumerable<Tutor>> GetAllTutors()
        {

            var Tutors = await db.Tutor.Include(zz => zz.TutorStatus).Include(zz => zz.File).Include(zz => zz.TutorModule).ThenInclude(zz => zz.Module).Where(zz => zz.TutorStatus.TutorStatusDesc == "Accepted").ToListAsync();
            return Tutors;
        }

        public async Task<Tutor> Reject(Tutor tutor)
        {
            int idreject = await db.TutorStatus.Where(zz => zz.TutorStatusDesc == "Rejected").Select(zz => zz.Id).FirstOrDefaultAsync();
            var tutorreject = await db.Tutor.Where(zz => zz.Id == tutor.Id).FirstOrDefaultAsync();
            tutorreject.TutorStatusId = idreject;
           await db.SaveChangesAsync();
           return tutorreject;

        }

        public async Task<Tutor> CreateTutor(AppUser userIdentity, CreateTutorDto tutor)
        {
            
            var result = await  _userManager.CreateAsync(userIdentity, tutor.Password);

            if (!result.Succeeded)
            {
                return null;
            }
            int idAccepted = await db.TutorStatus.Where(zz => zz.TutorStatusDesc == "Accepted").Select(zz => zz.Id).FirstOrDefaultAsync();

            var AcceptedTutor = await db.Tutor.Where(zz => zz.Id == tutor.Id).FirstOrDefaultAsync();
            AcceptedTutor.UserId = userIdentity.Id;
            AcceptedTutor.TutorStatusId = idAccepted;
           await db.SaveChangesAsync();


            int idTrue = await db.TutorSession.Where(zz => zz.SessionType.IsGroup == true).Select(zz => zz.Id).FirstOrDefaultAsync();
            int idFalse = await db.TutorSession.Where(zz => zz.SessionType.IsGroup == false).Select(zz => zz.Id).FirstOrDefaultAsync();

            TutorSessionModuleTutor td = new TutorSessionModuleTutor();
            td.TutorId = AcceptedTutor.Id;
            td.ModuleId = tutor.ModuleId;
            td.TutorSessionId = idTrue;
            await db.TutorSessionModuleTutor.AddAsync(td);
            await db.SaveChangesAsync();
            TutorSessionModuleTutor tdg = new TutorSessionModuleTutor();
            tdg.TutorId = AcceptedTutor.Id;
            tdg.ModuleId = tutor.ModuleId;
            tdg.TutorSessionId = idFalse;
            await db.TutorSessionModuleTutor.AddAsync(tdg);
            await db.SaveChangesAsync();
            return AcceptedTutor;
        }





        #endregion


        #region Subscription
        public async Task<Subscription> CreateSubscription(SubscriptionDto subscription)
        {
            Subscription entity = mapper.Map<Subscription>(subscription);
            await db.Subscription.AddAsync(entity);
            await db.SaveChangesAsync();

            SubscriptionTutorSession subscriptionTutorSession = new SubscriptionTutorSession();
            subscriptionTutorSession.SubscriptionId = entity.Id;
            subscriptionTutorSession.TutorSessionId = subscription.TutorSessionId;
            subscriptionTutorSession.Quantity = subscription.Quantity;
            await db.SubscriptionTutorSession.AddAsync(subscriptionTutorSession);
            await db.SaveChangesAsync();

            return entity;
        }

        public async Task<Subscription> UpdateSubscription(SubscriptionDto subscription)
        {
            Subscription entity = mapper.Map<Subscription>(subscription);
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();

            var tutorsession = await db.SubscriptionTutorSession.Where(zz => zz.SubscriptionId == subscription.Id).FirstOrDefaultAsync();
            tutorsession.TutorSessionId = subscription.TutorSessionId;
            tutorsession.Quantity = subscription.Quantity;
            await db.SaveChangesAsync();
            return entity;
        }


        #endregion

        #region csv
        public async Task<IEnumerable<Payment>> GetPayments()
        {
            var payments = await db.Payment.ToListAsync();
                return payments;
        }
        #endregion


    }
}
