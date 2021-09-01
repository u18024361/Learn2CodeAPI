using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Data.Mapper;
using Learn2CodeAPI.Dtos.AdminDto;
using Learn2CodeAPI.IRepository.Generic;
using Learn2CodeAPI.IRepository.IRepositoryAdmin;
using Learn2CodeAPI.IRepository.IRepositoryStudent;
using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Emailservice;
using Learn2CodeAPI.Dtos.StudentDto;

namespace Learn2CodeAPI.Controllers
{
    [Route("api/Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private IMapper mapper;
        private IGenRepository<University> universityGenRepo;
        private IGenRepository<Degree> DegreeGenRepo;
        private IGenRepository<Module> ModuleGenRepo;
        private IGenRepository<Student> StudentGenRepo;
        private IGenRepository<CourseFolder> CourseFolderGenRepo;
        private IGenRepository<CourseSubCategory> CourseSubCategoryGenRepo;
        private IGenRepository<SessionContentCategory> SessionContentCategoryRepo;
        private IGenRepository<Subscription> SubscriptionGenRepo;
        private IGenRepository<Tutor> TutorGenRepo;
        private IGenRepository<CourseContent> CourseContentRepo;
        private readonly AppDbContext db;
        private IAdmin AdminRepo;
        private readonly IEmailSender _emailsender;
        public AdminController(
            UserManager<AppUser> userManager,
            IMapper _mapper,
            IGenRepository<University> _universityGenRepo,
            IGenRepository<Degree> _DegreeGenRepo,
            IGenRepository<Module> _ModuleGenRepo,
            IGenRepository<CourseFolder> _CourseFolderGenRepo,
             IGenRepository<CourseSubCategory> _CourseSubCategoryGenRepo,
            IGenRepository<Student> _StudentGenRepo,
             IGenRepository<Tutor> _TutorGenRepo,
            IGenRepository<SessionContentCategory> _SessionContentCategoryRepo,
             IGenRepository<CourseContent> _CourseContentRepo,
            IGenRepository<Subscription> _SubscriptionGenRepo,
            IAdmin _AdminRepo,
            AppDbContext _db,
            IEmailSender emailsender

            )



        {
            _userManager = userManager;
            db = _db;
            CourseSubCategoryGenRepo = _CourseSubCategoryGenRepo;
            universityGenRepo = _universityGenRepo;
            mapper = _mapper;
            DegreeGenRepo = _DegreeGenRepo;
            ModuleGenRepo = _ModuleGenRepo;
            AdminRepo = _AdminRepo;
            CourseFolderGenRepo = _CourseFolderGenRepo;
            StudentGenRepo = _StudentGenRepo;
            TutorGenRepo = _TutorGenRepo;
            SessionContentCategoryRepo = _SessionContentCategoryRepo;
            SubscriptionGenRepo = _SubscriptionGenRepo;
            CourseContentRepo = _CourseContentRepo;
            _emailsender = emailsender;
        }

        [HttpGet]
        [Route("GetAdmin")]
        public async Task<IActionResult> GetAdmin()
        {
            var Admin = await db.Admin.Include(zz => zz.Identity).FirstOrDefaultAsync();
            return Ok(Admin);

        }

        #region University

        [HttpGet]
        [Route("GetUniversitybyId/{UniversityId}")]
        public async Task<IActionResult> GetUniversitybyId(int UniversityId)
        {
            var entity = await universityGenRepo.Get(UniversityId);

            return Ok(entity);
        }

        [HttpGet]
        [Route("SearchUniversity/{UniversityName}")]
        public async Task<IActionResult> SearchUniversity(string UniversityName)
        {
            var entity = await AdminRepo.GetByName(UniversityName);

            return Ok(entity);
        }

        [HttpGet]
        [Route("GetAllUniversities")]
        public async Task<IActionResult> GetAllUniversities()
        {
            var Universities = await universityGenRepo.GetAll();
            return Ok(Universities);

        }

        [HttpPost]
        [Route("CreateUniversity")]
        public async Task<IActionResult> CreateUniversity([FromBody] UniversityDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var check = db.University.Where(zz => zz.UniversityName == dto.UniversityName).FirstOrDefault();
                if (check != null)
                {
                    result.message = "University already exists";
                    return BadRequest(result.message);
                }
                University entity = mapper.Map<University>(dto);
                var data = await universityGenRepo.Add(entity);
                result.data = data;
                result.message = "university created";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong creating the university";
                return BadRequest(result.message);
            }

        }

        [HttpPut]
        [Route("EditUniversity")]
        public async Task<IActionResult> EditUniversity([FromBody] UniversityDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = db.University.Where(zz => zz.UniversityName == dto.UniversityName).FirstOrDefault();
                if (check != null)
                {
                    result.message = "University already exists";
                    return BadRequest(result.message);
                }
                University entity = mapper.Map<University>(dto);
                var data = await universityGenRepo.Update(entity);
                result.data = data;
                result.message = "university updated";
                return Ok(result);

            }
            catch
            {
                result.message = "Something went wrong updating the university";
                return BadRequest(result.message);

            }




        }



        [HttpDelete]
        [Route("DeleteUniversity/{UniversityId}")]
        public async Task<IActionResult> DeleteUniversity(int UniversityId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {  
                var tutors = await db.TutorModule.Where(zz => zz.Module.Degree.University.Id == UniversityId).ToListAsync();
                if (tutors.Count > 0) { return BadRequest("Cant delete this university as there are tutors registered for it"); };

                var students = await db.StudentModule.Where(zz => zz.Module.Degree.University.Id == UniversityId).ToListAsync();
                if (students.Count > 0) { return BadRequest("Cant delete this university as there are students registered for it"); };

                var enrolinee = await db.EnrolLine.Where(zz => zz.Module.Degree.University.Id == UniversityId).ToListAsync();
                if (enrolinee.Count > 0) { return BadRequest("Cant delete this university as there are Active subscriptions for its corresponding modules"); };

                var data = await universityGenRepo.Delete(UniversityId);
                result.data = data;
                result.message = "University deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong deleting the university";
                return BadRequest(result.message);
            }


        }

        #endregion

        #region Degree

        [HttpGet]
        [Route("GetDegreebyId/{DegreeId}")]
        public async Task<IActionResult> GetDegreebyId(int DegreeId)
        {
            var entity = await DegreeGenRepo.Get(DegreeId);


            return Ok(entity);
        }

        [HttpGet]
        [Route("SearchDegree/{DegreeName}")]
        public async Task<IActionResult> SearchDegree(string DegreeName)
        {
            var entity = await AdminRepo.GetByDegreeName(DegreeName);

            return Ok(entity);
        }


        [HttpGet]
        [Route("GetAllDegrees/{UniversityId}")]
        public async Task<IActionResult> GetAllDegrees(int UniversityId)
        {

            var degrees = await AdminRepo.GetAllDegrees(UniversityId);
            return Ok(degrees);

        }

        [HttpPost]
        [Route("CreateDegree")]
        public async Task<IActionResult> CreateDegree([FromBody] DegreeDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var check = db.Degrees.Where(zz => zz.DegreeName == dto.DegreeName).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Degree already exists";
                    return BadRequest(result.message);
                }
                Degree entity = mapper.Map<Degree>(dto);
                var data = await DegreeGenRepo.Add(entity);
                result.data = data;
                result.message = "Degree created";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong creating the degree";
                return BadRequest(result.message);
            }

        }


        [HttpPut]
        [Route("EditDegree")]
        public async Task<IActionResult> EditDegree([FromBody] DegreeDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = db.Degrees.Where(zz => zz.DegreeName == dto.DegreeName).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Degree already exists";
                    return BadRequest(result.message);
                }
                Degree entity = mapper.Map<Degree>(dto);
                var data = await DegreeGenRepo.Update(entity);
                result.data = data;
                result.message = "Degree updated";
                return Ok(result);

            }
            catch
            {
                result.message = "something went wrong updating the degree";
                return BadRequest(result.message);

            }

        }




        [HttpDelete]
        [Route("DeleteDegree/{DegreeId}")]
        public async Task<IActionResult> DeleteDegree(int DegreeId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var tutors = await db.TutorModule.Where(zz => zz.Module.Degree.Id == DegreeId).ToListAsync();
                if (tutors.Count > 0) { return BadRequest("Cant delete this Degree as there are tutors registered for it"); };

                var students = await db.StudentModule.Where(zz => zz.Module.Degree.Id == DegreeId).ToListAsync();
                if (students.Count > 0) { return BadRequest("Cant delete this Degree as there are students registered for it"); };

                var enrolinee = await db.EnrolLine.Where(zz => zz.Module.Degree.Id == DegreeId).ToListAsync();
                if (enrolinee.Count > 0) { return BadRequest("Cant delete this Degree as there are Active subscriptions for it corresponding module"); };
                var data = await DegreeGenRepo.Delete(DegreeId);
                result.data = data;
                result.message = "Degree deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong deleting the degree";
                return BadRequest(result.message);
            }

        }

        #endregion

        #region Module

        [HttpGet]
        [Route("SearchModule/{ModuleName}")]
        public async Task<IActionResult> SearchModule(string ModuleName)
        {
            var entity = await AdminRepo.GetByModuleName(ModuleName);

            return Ok(entity);
        }

        [HttpGet]
        [Route("GetModulebyId/{ModuleId}")]
        public async Task<IActionResult> GetModulebyId(int ModuleId)
        {
            var entity = await ModuleGenRepo.Get(ModuleId);


            return Ok(entity);
        }



        [HttpGet]
        [Route("GetAllModules/{DegreeId}")]
        public async Task<IActionResult> GetAllModules(int DegreeId)
        {
            var modules = await AdminRepo.GetAllModules(DegreeId);
            return Ok(modules);

        }

        [HttpPost]
        [Route("CreateModule")]
        public async Task<IActionResult> CreateModule([FromBody] Module dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var check = db.Modules.Where(zz => zz.ModuleCode == dto.ModuleCode).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Module already exists";
                    return BadRequest(result.message);
                }
                Module entity = mapper.Map<Module>(dto);
                var data = await AdminRepo.CreateModule(entity);
                result.data = data;
                result.message = "Module created";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong creating the module";
                return BadRequest(result.message);
            }


        }

        [HttpPut]
        [Route("EditModule")]
        public async Task<IActionResult> EditModule([FromBody] Module dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = db.Modules.Where(zz => zz.ModuleCode == dto.ModuleCode).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Module already exists";
                    return BadRequest(result.message);
                }
                Module entity = mapper.Map<Module>(dto);
                var data = await ModuleGenRepo.Update(entity);
                result.data = data;
                result.message = "Module updated";
                return Ok(result);

            }
            catch
            {
                result.message = "something went wrong updating the module";
                return BadRequest(result.message);

            }
        }



        [HttpDelete]
        [Route("DeleteModule/{ModuleId}")]
        public async Task<IActionResult> DeleteModule(int ModuleId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var tutors = await db.TutorModule.Where(zz => zz.ModuleId == ModuleId).ToListAsync();
                if (tutors.Count > 0) { return BadRequest("Cant delete this Module as there are tutors registered for it"); };

                var students = await db.StudentModule.Where(zz => zz.ModuleId == ModuleId).ToListAsync();
                if (students.Count > 0) { return BadRequest("Cant delete this Module as there are students registered for it"); };

                var enrolinee = await db.EnrolLine.Where(zz => zz.ModuleId == ModuleId).ToListAsync();
                if (enrolinee.Count > 0) { return BadRequest("Cant delete this Module as there are Active subscriptions for it "); };
                var data = await ModuleGenRepo.Delete(ModuleId);
                result.data = data;
                result.message = "Module deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong deleting the module";
                return BadRequest(result.message);
            }

        }

        #endregion

        #region CourseFolder

        [HttpPost]
        [Route("CreateCourseFolder")]
        public async Task<IActionResult> CreateCourseFolder([FromBody] CourseFolderDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var check = db.courseFolders.Where(zz => zz.CourseFolderName == dto.CourseFolderName).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Course folder already exists";
                    return BadRequest(result.message);
                }
                CourseFolder entity = mapper.Map<CourseFolder>(dto);
                var data = await CourseFolderGenRepo.Add(entity);
                result.data = data;
                result.message = "Course Folder created";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong creating the course folder";
                return BadRequest(result.message);
            }

        }

        [HttpGet]
        [Route("GetAllCourseFolder")]
        public async Task<IActionResult> GetAllCourseFolder()
        {
            var entity = await CourseFolderGenRepo.GetAll();
            return Ok(entity);

        }

        [HttpGet]
        [Route("SearchCourseFolder/{CourseFolderName}")]
        public async Task<IActionResult> SearchCourseFolder(string CourseFolderName)
        {
            var entity = await AdminRepo.GetByCourseFolderName(CourseFolderName);

            return Ok(entity);
        }

        [HttpPut]
        [Route("EditCourseFolder")]
        public async Task<IActionResult> EditCourseFolder([FromBody] CourseFolderDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var check = db.courseFolders.Where(zz => zz.CourseFolderName == dto.CourseFolderName).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Course folder already exists";
                    return BadRequest(result.message);
                }
                CourseFolder entity = mapper.Map<CourseFolder>(dto);
                var data = await CourseFolderGenRepo.Update(entity);
                result.data = data;
                result.message = "Course Folder updated";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong updating the course folder";
                return BadRequest(result.message);
            }

        }

        [HttpDelete]
        [Route("DeleteCourseFolder/{CourseFolderId}")]
        public async Task<IActionResult> DeleteCourseFolder(int CourseFolderId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var courseenroline = await db.CourseEnrolLine.Where(zz => zz.CourseSubCategory.CourseFolderId == CourseFolderId).ToListAsync();
                if (courseenroline.Count > 0) { return BadRequest("cant delete this course folder as there are purchased courses"); };
                var data = await CourseFolderGenRepo.Delete(CourseFolderId);
                result.data = data;
                result.message = "Module deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong deleting the module";
                return BadRequest(result.message);
            }


        }


        #endregion

        #region Student
        [HttpGet]
        [Route("GetAllStudents")]
        public async Task<IActionResult> GeAllStudents()
        {
            var entity = await AdminRepo.GetAllStudents();

            return Ok(entity);
        }



        //userid is in the aspnet users table
        [HttpDelete]
        [Route("DeleteStudent/{userId}")]
        public IActionResult search(string userId)
        {

            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            try
            {
               

                var student = db.Students.Where(zz => zz.UserId == userId).FirstOrDefault();
                var enrol = db.EnrolLine.Where(zz => zz.Enrollment.StudentId == student.Id).ToList();
                var enrolcourse = db.CourseEnrolLine.Where(zz => zz.CourseEnrol.StudentId == student.Id).ToList();
                if (enrol.Count > 0 )
                {
                    return BadRequest("cant delete student as the student is enrolled in a subscription");
                }

                if (enrolcourse.Count > 0)
                {
                    return BadRequest("cant delete student as the student  has bought courses");
                }


                var user =  db.Users.Where(zz => zz.Id == student.UserId).FirstOrDefault();
                var reg = db.RegisteredStudent.Where(zz => zz.StudentId == student.Id).ToList();
                foreach (var item in reg)
                {
                    db.RegisteredStudent.Remove(item);
                }
                db.Students.Remove(student);
                db.Users.Remove(user);
                db.SaveChanges();
                result.message = "Student Deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong deleting the student";
                return BadRequest(result.message);
            }


        }


        #endregion

        #region SessionContentCategory
        [HttpGet]
        [Route("GetAllSessionContentCategory")]
        public async Task<IActionResult> GetAllSessionContentCategory()
        {
            var Universities = await SessionContentCategoryRepo.GetAll();
            return Ok(Universities);

        }

        [HttpPost]
        [Route("CreateSessionContentCategory")]
        public async Task<IActionResult> CreateSessionContentCategory([FromBody] SessionContentCategoryDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var check = db.SessionContentCategory.Where(zz => zz.SessionContentCategoryName == dto.SessionContentCategoryName).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Category already exists";
                    return BadRequest(result.message);
                }
                SessionContentCategory entity = mapper.Map<SessionContentCategory>(dto);
                var data = await SessionContentCategoryRepo.Add(entity);
                result.data = data;
                result.message = "Category created";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong creating the category";
                return BadRequest(result.message);
            }


        }

        [HttpPut]
        [Route("EditSessionContentCategory")]
        public async Task<IActionResult> EditSessionContentCategory([FromBody] SessionContentCategoryDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var check = db.SessionContentCategory.Where(zz => zz.SessionContentCategoryName == dto.SessionContentCategoryName).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Category already exists";
                    return BadRequest(result.message);
                }
                SessionContentCategory entity = mapper.Map<SessionContentCategory>(dto);
                var data = await SessionContentCategoryRepo.Update(entity);
                result.data = data;
                result.message = "Category updated";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong updating the category";
                return BadRequest(result.message);
            }

        }

        [HttpDelete]
        [Route("DeleteSessionContentCategory/{SessionContentCategoryId}")]
        public async Task<IActionResult> DeleteSessionContentCategory(int SessionContentCategoryId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var data = await SessionContentCategoryRepo.Delete(SessionContentCategoryId);
                result.data = data;
                result.message = "Category deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong deleting the category";
                return BadRequest(result.message);
            }

        }
        #endregion

        #region CourseContentCategory
        [HttpGet]
        [Route("GetAllCourseSubCategory/{CourseFolderId}")]
        public async Task<IActionResult> GetAllCourseSubCategory(int CourseFolderId)
        {
            var subcategories = await db.courseSubCategory.Where(zz => zz.CourseFolderId == CourseFolderId).ToListAsync();
            return Ok(subcategories);

        }


        [HttpPost]
        [Route("CreateCourseSubCategory")]
        public async Task<IActionResult> CreateCourseSubCategory([FromBody] CoursSubCategoryDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = db.courseSubCategory.Where(zz => zz.CourseSubCategoryName == dto.CourseSubCategoryName).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Subcategory already exists";
                    return BadRequest(result.message);
                }

                CourseSubCategory entity = mapper.Map<CourseSubCategory>(dto);
                var data = await CourseSubCategoryGenRepo.Add(entity);
                result.data = data;
                result.message = "course subcategory created";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong creating the subcategory";
                return BadRequest(result.message);
            }

        }

        [HttpPut]
        [Route("EditCourseSubCategory")]
        public async Task<IActionResult> EditCourseSubCategory([FromBody] CoursSubCategoryDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = db.courseSubCategory.Where(zz => zz.CourseSubCategoryName == dto.CourseSubCategoryName && zz.Id != dto.Id).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Category already exists";
                    return BadRequest(result.message);
                }

                CourseSubCategory entity = mapper.Map<CourseSubCategory>(dto);
                var data = await CourseSubCategoryGenRepo.Update(entity);
                result.data = data;
                result.message = "course subcategory updated";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong updating the subcategory";
                return BadRequest(result.message);
            }

        }

        [HttpDelete]
        [Route("DeleteCourseSubCategory/{CourseSubCategoryId}")]
        public async Task<IActionResult> DeleteCourseSubCategory(int CourseSubCategoryId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var courseenroline = await db.CourseEnrolLine.Where(zz => zz.CourseSubCategoryId == CourseSubCategoryId).ToListAsync();
                if (courseenroline.Count > 0) { return BadRequest("cant delete this course sub category as there are purchased courses"); };
                var data = await CourseSubCategoryGenRepo.Delete(CourseSubCategoryId);
                result.data = data;
                result.message = "Subcategory deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong deleting the subcategory";
                return BadRequest(result.message);
            }

        }

        #endregion

        #region Tutor
        [HttpGet]
        [Route("GetAllAplications")]
        public async Task<IActionResult> GetAllAplications()
        {
            var applications = await AdminRepo.GetAllApplications();
            return Ok(applications);

        }

        [HttpGet]
        [Route("GetTutorbyId/{TutorId}")]
        public async Task<IActionResult> GetTutorbyId(int TutorId)
        {
            var entity = await TutorGenRepo.Get(TutorId);

            return Ok(entity);
        }

        [HttpGet]
        [Route("DownTutorApplication/{Fileid}")]
        public async Task<FileStreamResult> DownloadResource(int Fileid)
        {
            var entity = await db.File.Where(zz => zz.Id == Fileid).Select(zz => zz.FileName).FirstOrDefaultAsync();
            MemoryStream ms = new MemoryStream(entity);
            return new FileStreamResult(ms, "Application/pdf");
        }

        //email will be done at later stage
        [HttpPut]
        [Route("RejectTutor")]
        public async Task<IActionResult> RejectTutor([FromBody] TutorDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                var data = await AdminRepo.Reject(dto);
                string subject = "Application Outcome:" ;
                string content = "Dear Applicant" + Environment.NewLine +
                    "We regret to inform you that your application to become a tutor was unsuccessful. " +
                    "Firstly we would like to say thank you for taking the time to apply and showing interest in TutorDevOps." + Environment.NewLine +
                    "When screening through the applicants there are a variety of criteria that need to met and due to our strict standards it is not unusual for an application to be dismissed."
                    + Environment.NewLine + "If you have any questions please feel free to eamil back" + Environment.NewLine +"Regards TutorDevOps";
               
                var message = new Emailservice.Message(new string[] { dto.TutorEmail}, subject, content);
                await _emailsender.SendEmailAsync(message);

                result.data = data;
                result.message = "Tutor rejected";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong while rejectin the tutor";
                return BadRequest(result.message);
            }

        }

        [HttpPut]
        [Route("CreateTutor")]
        public async Task<IActionResult> CreateTutor([FromBody] CreateTutorDto dto)
        {

            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = db.Users.Where(zz => zz.UserName == dto.UserName).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Username is taken";
                    return BadRequest(result.message);
                }

                var userIdentity = mapper.Map<AppUser>(dto);
                userIdentity.Email = dto.TutorEmail;
                var data = await AdminRepo.CreateTutor(userIdentity, dto);
                string subject = "Application Outcome:";
                string content = "Dear Applicant" + Environment.NewLine +
                    "Your application to become a tutor for TutorDevOps has been successfull! " + Environment.NewLine +
                    "On behalf of the TutorDevOps team we would like to say welcome and thank you for wanting to help us achieve our goals" + Environment.NewLine +  Environment.NewLine +
                   "Below are your temporary user/login details:"
                    + Environment.NewLine +
                    "Password:"+ dto.Password
                    + Environment.NewLine +
                    "Username:" + dto.UserName
                     + Environment.NewLine +
                      Environment.NewLine +
                      "Regards TutorDevOps";

                var message = new Emailservice.Message(new string[] { dto.TutorEmail }, subject, content);
                await _emailsender.SendEmailAsync(message);
                result.data = data;
                result.message = "Tutor created";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong while creating the tutor";
                return BadRequest(result.message);
            }


        }


        [HttpGet]
        [Route("GetAllTutors")]
        public async Task<IActionResult> GetAllTutors()
        {
            var Tutors = await AdminRepo.GetAllTutors();
            return Ok(Tutors);

        }

        [HttpDelete]
        [Route("DeleteTutor/{userId}")]
        public IActionResult DeleteTutor(string userId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            try
            {
                var user = db.Users.Where(zz => zz.Id == userId).FirstOrDefault();
                var tutor = db.Tutor.Where(zz => zz.UserId == userId).FirstOrDefault();
                var sessions =  db.BookingInstance.Where(zz => zz.TutorId == tutor.Id).ToList();
                var messages =  db.Message.Where(zz => zz.TutorId == tutor.Id).ToList();
                var datelist = new List<deleteindividualDto>();
                var datenow = DateTime.Now;
                foreach (var item in sessions)
                {
                    deleteindividualDto dlist = new deleteindividualDto();
                    DateTime oDate = Convert.ToDateTime(item.Date);
                    dlist.date = oDate;
                    datelist.Add(dlist);
                }

                var individualcheck = datelist.Where(zz => zz.date > datenow).ToList();
                if (individualcheck.Count > 0)
                {
                    return BadRequest("Unable to delete tutor as he/she has upcoming sessions");
                }


                foreach (var item in messages)
                {

                    db.Message.Remove(item);
                }
                db.SaveChanges();
                var x = db.File.Where(zz => zz.Id == tutor.FileId).FirstOrDefault();
                db.Users.Remove(user);
                db.File.Remove(x);
                db.SaveChanges();
                result.data = tutor;
                result.message = "Tutor deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "something went wrong while deleting the tutor";
                return BadRequest(result.message);
            }

        }


        #endregion

        #region coursecontent

        [HttpGet]
        [Route("GetContent/{CourseCategoryId}")]
        public async Task<IActionResult> GetContent(int CourseCategoryId)
        {
            var content = await db.CourseContent.Include(zz => zz.ContentType).Include(zz => zz.CourseSubCategory).Where(zz => zz.CourseSubCategoryId == CourseCategoryId).ToListAsync();
            return Ok(content);

        }
        [HttpGet]
        [Route("GetContenttype")]
        public async Task<IActionResult> GetContenttype()
        {
            var contenttype = await db.ContentType.ToListAsync();
            return Ok(contenttype);

        }

        [HttpPost]
        [Route("CreatContent")]
        public async Task<IActionResult> CreatContent([FromForm] CourseContentDto dto)
        {
            //   [FromForm(Name = "file")]
            //IFormFile files

            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var check = await db.CourseContent.Where(zz => zz.FileName == dto.Content.FileName && zz.CourseSubCategoryId == dto.CourseSubCategoryId).FirstOrDefaultAsync();
                if (check != null)
                {
                    result.message = "Content already exists";
                    return BadRequest(result.message);
                }


                var objfiles = new CourseContent()
                {
                    Id = 0,
                    ContentTypeId = dto.ContentTypeId,
                    CourseSubCategoryId = dto.CourseSubCategoryId,
                    FileName = dto.Content.FileName,

                };
                using (var target = new MemoryStream())
                {
                    dto.Content.CopyTo(target);
                    objfiles.Content = target.ToArray();
                }

                await db.CourseContent.AddAsync(objfiles);
               await  db.SaveChangesAsync();

                result.data = objfiles;
                result.message = "Content Added";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong while adding the content";
                return BadRequest(result.message);
            }

        }

        [HttpPut]
        [Route("EditContent")]
        public async Task<IActionResult> EditContent([FromForm] CourseContentDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = db.CourseContent.Where(zz => zz.FileName == dto.Content.FileName && zz.Id !=  dto.Id).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Content already exists";
                    return BadRequest(result.message);
                }
                var content = await db.CourseContent.Where(zz => zz.Id == dto.Id).FirstOrDefaultAsync();
                content.FileName = dto.Content.FileName;
                content.CourseSubCategoryId = dto.CourseSubCategoryId;
                content.ContentTypeId = dto.ContentTypeId;
                using (var target = new MemoryStream())
                {
                    dto.Content.CopyTo(target);
                    content.Content = target.ToArray();
                }
                await db.SaveChangesAsync();
                result.data = content;
                result.message = "Content updated";
                return Ok(result);

            }
            catch
            {
                result.message = "Something went wrong updating the content";
                return BadRequest(result.message);

            }




        }

        [HttpDelete]
        [Route("DeleteContent/{ContentId}")]
        public async Task<IActionResult> DeleteResource(int ContentId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var data = await CourseContentRepo.Delete(ContentId);

                result.data = data;
                result.message = "Resource deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong deleting the Content";
                return BadRequest(result.message);
            }


        }

        [HttpGet]
        [Route("Video/{id}")]
        public async Task<FileStreamResult> Video(int id)
        {
            var entity = await db.CourseContent.Where(zz => zz.Id == id).FirstOrDefaultAsync();
            MemoryStream ms = new MemoryStream(entity.Content);
            return new FileStreamResult(ms, "video/mp4");
        }

        [HttpGet]
        [Route("DownloadRContentPdf/{id}")]
        public async Task<FileStreamResult> DownloadRContentPdf(int id)
        {
            var entity = await db.CourseContent.Where(zz => zz.Id == id).Select(zz => zz.Content).FirstOrDefaultAsync();
            MemoryStream ms = new MemoryStream(entity);
            return new FileStreamResult(ms, "Application/pdf");
        }


        #endregion

        #region Subscription

        [HttpGet]
        [Route("GetAllTutorSessions")]
        public async Task<IActionResult> GetAllTutorSessions()
        {
            var GetAllTutorSessions = await db.TutorSession.Include(zz => zz.SessionType).ToListAsync();
            return Ok(GetAllTutorSessions);

        }

        [HttpGet]
        [Route("GetAllSubscriptions")]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            var Subscriptions = await db.Subscription.Include(zz => zz.SubscriptionTutorSession).ToListAsync();
            return Ok(Subscriptions);

        }

        [HttpPost]
        [Route("CreateSubscription")]
        public async Task<IActionResult> CreateSubscription([FromBody] SubscriptionDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var check = db.Subscription.Where(zz => zz.SubscriptionName == dto.SubscriptionName).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Subscription already exists";
                    return BadRequest(result.message);
                }
               
                var data = await AdminRepo.CreateSubscription(dto);
                result.data = data;
                result.message = "Subscription created";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong creating the Subscription";
                return BadRequest(result.message);
            }

        }

        [HttpPut]
        [Route("EditSubscription")]
        public async Task<IActionResult> EditSubscription([FromBody]  SubscriptionDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
            
               
                var data = await AdminRepo.UpdateSubscription(dto);
                result.data = data;
                result.message = "Subscription updated";
                return Ok(result);

            }
            catch
            {
                result.message = "Something went wrong updating the Subscription";
                return BadRequest(result.message);

            }


        }

        [HttpDelete]
        [Route("DeleteSubscription/{SubscriptionId}")]
        public async Task<IActionResult> DeleteSubscription(int SubscriptionId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            { var date = DateTime.Now;
                var enroline = await db.EnrolLine.Where(zz => zz.SubscriptionId == SubscriptionId && zz.EndDate > date).ToListAsync();
                if (enroline.Count > 0) { return BadRequest("cant delete this subscription as there are active subscriptions"); };
                var data = await SubscriptionGenRepo.Delete(SubscriptionId);
                result.data = data;
                result.message = "Subscription deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong deleting the Subscription";
                return BadRequest(result.message);
            }


        }

        #endregion

        #region CSV
        // dont call this one
        //[HttpPost]
        //[Route("Excell")]
        //public async Task<List<Payment>> Import([FromForm(Name = "file")]IFormFile file)
        //{
        //    var list = new List<Payment>();
        //    using(var stream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(stream);
        //        using(var package = new ExcelPackage(stream))
        //        {
        //            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
        //            var rowcount = worksheet.Dimension.Rows;
        //            for(int row = 2; row <= rowcount; row++)
        //            {
        //                list.Add(new Payment { PaymentAmount = worksheet.Cells[row, 3].Value.ToString().Trim() });
        //            }
        //        }
        //    }
        //    foreach (var item in list)
        //    {
        //        await db.Payment.AddAsync(item);
                    
        //    }
        //    await db.SaveChangesAsync();
        //    return list;
        //}

        [HttpPost]
        [Route("CSVUpload")]
        public IActionResult Importt([FromForm(Name = "file")]IFormFile file)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var list = new List<Payment>();
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);

                    stream.Seek(0, SeekOrigin.Begin);

                    using (var reader = new StreamReader(stream))
                    {

                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {

                            var records = csv.GetRecords<Payment>().ToList();
                            records.RemoveAt(records.Count - 1);
                            foreach (var item in records)
                            {
                                db.Payment.Add(item);
                                db.SaveChanges();
                            }


                        };
                    }

                }
                result.message = "File uploaded";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong uploading the file";
                return BadRequest(result.message);
            }
        }

        [HttpGet]
        [Route("GetPayments")]
        public async Task<IActionResult> GetUniversitybyId()
        {
            var entity = await AdminRepo.GetPayments();

            return Ok(entity);
        }


        #endregion


    }




}

