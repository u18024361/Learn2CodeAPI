using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.StudentDto;
using Learn2CodeAPI.Dtos.TutorDto;
using Learn2CodeAPI.IRepository.Generic;
using Learn2CodeAPI.IRepository.IRepositoryStudent;
using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using Learn2CodeAPI.Repository.RepositoryStudent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learn2CodeAPI.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private IMapper _mapper;
        private readonly AppDbContext db;
        private IStudent studentRepo;
        private IGenRepository<Tutor> TutorGenRepo;
        private IGenRepository<Message> Mess;
        private IGenRepository<Module> ModuleGenRepo;
        private IGenRepository<CourseFolder> CourseFolderGenRepo;
        private IGenRepository<Message> MessageGenRepo;


        public StudentController(IStudent _studentRepo, UserManager<AppUser> userManager, IMapper mapper, AppDbContext appDbContext,
            AppDbContext _db, IGenRepository<Tutor> _TutorGenRepo, IGenRepository<Message>_Mess,IGenRepository<Message>_Mes, 
            IGenRepository<Module> _ModuleGenRepo, IGenRepository<CourseFolder> _CourseFolderGenRepo)
        {
            studentRepo = _studentRepo;
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
            db = _db;
            TutorGenRepo = _TutorGenRepo;
            MessageGenRepo = _Mess;
            Mess = _Mes;
            ModuleGenRepo = _ModuleGenRepo;
            CourseFolderGenRepo = _CourseFolderGenRepo;

        }

        #region Student
        //registration action
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Post([FromBody]RegistrationDto model)

        {

            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                 
                var user = db.Users.Where(zz => zz.Email == model.Email).FirstOrDefault();
                if (user != null)
                {
                    return BadRequest("Email already exists");
                }

                var username = db.Users.Where(zz => zz.UserName == model.UserName).FirstOrDefault();
                if (username != null)
                {
                    return BadRequest("Username is taken");
                }
                var userIdentity = _mapper.Map<AppUser>(model);
                var data = await studentRepo.Register(userIdentity, model);
                result.data = data;
                result.message = "Registration successfull";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong creating the university";
                return BadRequest(result.message);
            }



        }

        [HttpPut]
        [Route("updatestudent")]
        public async Task<IActionResult> UpdateStudentAsync([FromBody] UpdateStudent dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = db.Users.Where(zz => zz.Email == dto.Email && zz.Id != dto.UserId).FirstOrDefault();
                if (user != null)
                {
                    return BadRequest("Email already exists");
                }

                var username = db.Users.Where(zz => zz.UserName == dto.UserName && zz.Id != dto.UserId).FirstOrDefault();
                if (username != null)
                {
                    return BadRequest("Username is taken");
                }

                var data = await studentRepo.UpdateProfile(dto);
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

        #endregion

        #region Messaging
        //for creating a message
        [HttpGet]
        [Route("GetAllTutorsMessaging")]
        public async Task<IActionResult> GetAllTutorsMessaging()
        {

            var students = await studentRepo.GetTutors();
            return Ok(students);

        }

       

        [HttpGet]
        [Route("GetSentMessages/{UserId}")]
        public async Task<IActionResult> GetSentMessages(string UserId)
        {

            var messages = await studentRepo.GetSentMessages(UserId);
            return Ok(messages);

        }

        [HttpGet]
        [Route("GetRecievedMessages/{UserId}")]
        public async Task<IActionResult> GetRecievedMessages(string UserId)
        {

            var messages = await studentRepo.GetRecievedMessages(UserId);
            return Ok(messages);

        }

        //sender id will be equal to user id
        [HttpPost]
        [Route("CreateMessage")]
        public async Task<IActionResult> CreateMessage([FromBody] MessageDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var data = await studentRepo.CreateMessage(dto);
                result.data = data;
                result.message = "Message sent";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong sending the message";
                return BadRequest(result.message);
            }

        }

        [HttpDelete]
        [Route("DeleteMessage/{MessageId}")]
        public async Task<IActionResult> DeleteMessage(int MessageId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var message = await db.Message.Where(zz => zz.Id == MessageId).FirstOrDefaultAsync();
                DateTime oDate = Convert.ToDateTime(message.TimeStamp);
                var start = DateTime.Now;
                if ((start - oDate).TotalDays <= 1)
                {
                    result.message = "24 hours have not passed";
                    return BadRequest(result.message);
                }
                var data = await MessageGenRepo.Delete(MessageId);

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

        #region ViewTutors
        [HttpGet]
        [Route("GetAllTutors")]
        public async Task<IActionResult> GetAllTutors()
        {

            var tutors = await studentRepo.GetTutors();
            return Ok(tutors);

        }
        #endregion

        #region Viewsource
        [HttpGet]
        [Route("ViewModules")]
        public async Task<IActionResult> ViewModules()
        {

            var modules = await ModuleGenRepo.GetAll();
            return Ok(modules);

        }

        [HttpGet]
        [Route("ViewResources/{ModuleId}")]
        public async Task<IActionResult> ViewResources(int ModuleId)
        {

            var resources = await studentRepo.GetResource(ModuleId);
            return Ok(resources);

        }

        [HttpGet]
        [Route("DownloadResource/{Resourceid}")]
        public async Task<FileStreamResult> DownloadResource(int Resourceid)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                result.message = "Sorry error on our side";
                return Ok(result);
            }
            try
            {
                var entity = await db.Resource.Where(zz => zz.Id == Resourceid).Select(zz => zz.ResoucesName).FirstOrDefaultAsync();
                MemoryStream ms = new MemoryStream(entity);
                return new FileStreamResult(ms, "Application/pdf");
               
               
            }
            catch
            {

                result.message = "Something went wrong downloading the resource";
                return BadRequest(result.message);
            }

            
        }
        #endregion

        #region ViewShop
        [HttpGet]
        [Route("GetBasket{StudentId}")]
        public async Task<IActionResult> GetBasket(int StudentId)
        {

            var coursefolder = await CourseFolderGenRepo.GetAll();
            return Ok(coursefolder);

        }

        [HttpGet]
        [Route("GetCourseFolder")]
        public async Task<IActionResult> GetCourseFolder()
        {

            var coursefolder = await CourseFolderGenRepo.GetAll();
            return Ok(coursefolder);

        }

        [HttpGet]
        [Route("GetCourseSybCategory/{CourseFolderId}")]
        public async Task<IActionResult> GetCourseSybCategory(int CourseFolderId)
        {

            var coursefolder = await studentRepo.GetCourseSubCategory(CourseFolderId);
            return Ok(coursefolder);

        }
        #endregion

        #region Addtobasket
        //[HttpPost]
        //[Route("AddtoBasket")]
        //public async Task<IActionResult> AddtoBasket([FromBody] CourseSubCategory dto)
        //{
        //    dynamic result = new ExpandoObject();
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        var data = await studentRepo.CreateMessage(dto);
        //        result.data = data;
        //        result.message = "Message sent";
        //        return Ok(result);
        //    }
        //    catch
        //    {

        //        result.message = "Something went wrong sending the message";
        //        return BadRequest(result.message);
        //    }

        //}
        #endregion



    }
}