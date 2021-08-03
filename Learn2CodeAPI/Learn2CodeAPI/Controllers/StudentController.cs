using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.StudentDto;
using Learn2CodeAPI.Dtos.TutorDto;
using Learn2CodeAPI.IRepository.Generic;
using Learn2CodeAPI.IRepository.IRepositoryStudent;
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
        private IGenRepository<Message> MessageGenRepo;


        public StudentController(IStudent _studentRepo, UserManager<AppUser> userManager, IMapper mapper, AppDbContext appDbContext,
            AppDbContext _db, IGenRepository<Tutor> _TutorGenRepo, IGenRepository<Message> _Message)
        {
            studentRepo = _studentRepo;
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
            db = _db;
            TutorGenRepo = _TutorGenRepo;
            _Message = MessageGenRepo;

        }

        #region Student
        //registration action
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Post([FromBody]RegistrationDto model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
            
           var result = await studentRepo.Register(userIdentity, model);

            if (result == null)
            {
                return BadRequest("Unable to Create student");
            }
            else
            {
                return Ok("studentCreated Succesffully");
            }


        }

        [HttpPut]
        [Route("updatestudent")]
        public async Task<IActionResult> UpdateStudentAsync([FromBody] UpdateStudent dto)
        {

            var result = await studentRepo.UpdateProfile(dto);
            if (result == null)
            {
                return BadRequest("Unable to Create student");
            }
            else
            {
                return Ok("student updated Succesffully");
            }


        }

        #endregion

        #region Messaging
        //for creating a message
        [HttpGet]
        [Route("GetAllTutors")]
        public async Task<IActionResult> GetAllStudents()
        {

            var students = await TutorGenRepo.GetAll();
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
        public async Task<IActionResult> DeleteUniversity(int MessageId)
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


    }
}