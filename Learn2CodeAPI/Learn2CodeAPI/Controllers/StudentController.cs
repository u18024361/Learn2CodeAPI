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
        private IGenRepository<CourseBasketLine> CourseBasketLineGenRepo;
        private IGenRepository<SubScriptionBasketLine> SubScriptionBasketLineGenRepo;


        public StudentController(IStudent _studentRepo, UserManager<AppUser> userManager, IMapper mapper, AppDbContext appDbContext,
            AppDbContext _db, IGenRepository<Tutor> _TutorGenRepo, IGenRepository<Message>_Mess,IGenRepository<Message>_Mes, 
            IGenRepository<Module> _ModuleGenRepo, IGenRepository<CourseFolder> _CourseFolderGenRepo,
            IGenRepository<CourseBasketLine> _CourseBasketLineGenRepo, IGenRepository<SubScriptionBasketLine> _SubScriptionBasketLineGenRepo)
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
            CourseBasketLineGenRepo = _CourseBasketLineGenRepo;
            SubScriptionBasketLineGenRepo = _SubScriptionBasketLineGenRepo;

        }
        [HttpGet]
        [Route("Getstudent/{userid}")]
        public async Task<IActionResult> GetAllTutorsMessaging( string userid)
        {

            var student = await db.Students.Include(zz => zz.Identity).Include(zz => zz.StudentModule)
                .ThenInclude(zz => zz.Module.Degree.University).Where(zz => zz.UserId == userid)
                .FirstOrDefaultAsync();
            return Ok(student);

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
        [Route("GetBasket/{StudentId}")]
        public async Task<IActionResult> GetBasket(int StudentId)
        {

            var coursefolder = await studentRepo.GetBasket(StudentId);
            return Ok(coursefolder);

        }

        [HttpGet]
        [Route("GetModule")]
        public async Task<IActionResult> GetModule()
        {

            var coursefolder = await ModuleGenRepo.GetAll();
            return Ok(coursefolder);

        }

        [HttpGet]
        [Route("GetSubscription")]
        public async Task<IActionResult> GetSubscription()
        {

            var coursefolder = await studentRepo.GetSubscriptions();
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
        //for courses
        [HttpPost]
        [Route("AddCoursetoBasket")]
        public async Task<IActionResult> AddtoBasket([FromBody] CourseBuyDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var data = await studentRepo.BuyCourse(dto);
                result.data = data;
                result.message = "Course added";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong adding the course";
                return BadRequest(result.message);
            }

        }
        //for subscriptions
        [HttpPost]
        [Route("AddSubscriptiontoBasket")]
        public async Task<IActionResult> AddSubscriptiontoBasket([FromBody] SubscriptionBuyDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var data = await studentRepo.BuySubscription(dto);
                result.data = data;
                result.message = "Subscription added";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong adding the Subscription";
                return BadRequest(result.message);
            }

        }
        #endregion

        #region ViewBasket
        //forcourses
        [HttpGet]
        [Route("GetBasketCourses/{BasketId}")]
        public async Task<IActionResult> GetBasketCourses(int BasketId)
        {

            var courses = await db.CourseBasketLine.Where(zz => zz.BasketId == BasketId).Include(zz => zz.CourseSubCategory).ToListAsync();
            return Ok(courses);

        }
        //forsubscriptions
        [HttpGet]
        [Route("GetBasketSubscriptions/{BasketId}")]
        public async Task<IActionResult> GetBasketSubscriptions(int BasketId)
        {

            var Subscriptions = await db.SubScriptionBasketLine.Where(zz => zz.BasketId == BasketId).Include(zz => zz.Module).Include(zz => zz.Subscription).ToListAsync();
            return Ok(Subscriptions);

        }
        #endregion

        #region Checkout
        [HttpPost]
        [Route("Checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if(dto.Message != "Approved")
                {
                    result.message = "Error when Checking out";
                    return Ok(result);
                }



                var data = await studentRepo.Checkout(dto);
                result.data = data;
                result.message = "Checkout Successfull";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong While checking out";
                return BadRequest(result.message);
            }

        }
        #endregion

        #region viewCourses
        [HttpGet]
        [Route("GetStudentCourses/{StudentId}")]
        public async Task<IActionResult> GetStudentCourses(int StudentId)
        {

            var Courses = await db.CourseEnrol.Include(zz => zz.CourseEnrolLine)
                .ThenInclude(StudentModule => StudentModule.CourseSubCategory.courseFolder).Where(zz => zz.StudentId == StudentId).ToListAsync();
            return Ok(Courses);

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

        #region removeItem
        //for courses
        [HttpDelete]
        [Route("RemoveCourse/{CourseBasketLineId}")]
        public async Task<IActionResult> DeleteResourceCategory(int CourseBasketLineId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var data = await CourseBasketLineGenRepo.Delete(CourseBasketLineId);
                result.data = data;
                result.message = "Course Removed";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong removing the course";
                return BadRequest(result.message);
            }


        }

        // for subscriptions
        [HttpDelete]
        [Route("RemoveSubscription/{SubScriptionBasketLineId}")]
        public async Task<IActionResult> RemoveSubscription(int SubScriptionBasketLineId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var data = await SubScriptionBasketLineGenRepo.Delete(SubScriptionBasketLineId);
                result.data = data;
                result.message = "Subscription Removed";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong removing the subscription";
                return BadRequest(result.message);
            }


        }
        #endregion

        #region make individual

        //to get individual
        [HttpGet]
        [Route("GetbookingIndividual/{StudentId}")]
        public async Task<IActionResult> GetbookingIndividual(int StudentId)
        {
            dynamic result = new ExpandoObject();
            var today = DateTime.Now;
            var enrol = await db.EnrolLine.Include(zz => zz.Enrollment).Include(zz => zz.Module).Where(zz => zz.Enrollment.StudentId == StudentId
            && zz.EndDate >= today).ToListAsync();
            var sessionmodulelist = new List<dynamic>();
            var individualsession = await db.TutorSession.Include(zz => zz.SessionType).Where(zz => zz.SessionType.IsGroup == false).FirstOrDefaultAsync();
            foreach(var item in enrol)
            {
                dynamic modulesesion = new ExpandoObject();
                modulesesion.moduleId = item.ModuleId;
                item.Module.EnrolLine = null;
                modulesesion.module = item.Module;
                 var session = await db.SubscriptionTutorSession.Include(zz => zz.Subscription)
                    .Include(zz => zz.TutorSession).Where(zz => zz.SubscriptionId == item.SubscriptionId && zz.TutorSessionId == individualsession.Id).FirstOrDefaultAsync();
                modulesesion.tutorSessionId = session.TutorSessionId;
                session.TutorSession.SubscriptionTutorSession = null;
                modulesesion.tutorSession = session.TutorSession;
                sessionmodulelist.Add(modulesesion);

            }
            

            return Ok(sessionmodulelist);

        }

        [HttpGet]
        [Route("GetIndividualAvailable/{ModuleId}/{TutorSessionId}")]
        public async Task<IActionResult> GetbookingIndividual(int ModuleId, int TutorSessionId)
        {
            var available = await db.BookingInstance.Where(zz => zz.ModuleId == ModuleId && zz.TutorSessionId == TutorSessionId && zz.BookingStatus.bookingStatus == "Open").ToListAsync();
            return Ok(available);
        }

        [HttpGet]
        [Route("CreateIndividualBooking/{StudentId}/{BookingInstanceId}/{ModuleId}")]
        public async Task<IActionResult> CreateIndividualBooking(int StudentId, int BookingInstanceId, int ModuleId)
        {
            var today = DateTime.Now;
            var enrolineId = await db.EnrolLine
           .Where(zz => zz.Enrollment.StudentId == StudentId && zz.EndDate >= today && zz.ModuleId == ModuleId)
           .Select(zz => zz.Id).FirstOrDefaultAsync();

            // enrolmentid becomes enrollineid
            var ticketlist = await db.Ticket.Include(zz => zz.TicketStatus)
                .Where(zz => zz.EnrollmentId == enrolineId && zz.TicketStatus.ticketStatus == true).ToListAsync();
            
            if ( ticketlist.Count > 0)
            {
                var newBooking = new Booking();
                newBooking.StudentId = StudentId;
                await db.Booking.AddAsync(newBooking);
                await db.SaveChangesAsync();

                int bookedstatus = await db.BookingStatus.Where(zz => zz.bookingStatus == "Booked").Select(zz => zz.Id).FirstOrDefaultAsync();
                var instance = await db.BookingInstance.Where(zz => zz.Id == BookingInstanceId).FirstOrDefaultAsync();
                instance.BookingStatusId = bookedstatus;

                ticketlist[0].TicketStatus ==
                await db.SaveChangesAsync();
                return Ok(instance);
            }
            else
            {
                return BadRequest("no tickets");
            }


           
        }



        //to get group
        [HttpGet]
        [Route("GetbookingGroup/{StudentId}")]
        public async Task<IActionResult> GetbookingGroup(int StudentId)
        {
            dynamic result = new ExpandoObject();
            var today = DateTime.Now;
            var enrol = await db.EnrolLine.Include(zz => zz.Enrollment).Include(zz => zz.Module).Where(zz => zz.Enrollment.StudentId == StudentId
            && zz.EndDate >= today).ToListAsync();
            var sessionmodulelist = new List<dynamic>();
            var groupsession = await db.TutorSession.Include(zz => zz.SessionType).Where(zz => zz.SessionType.IsGroup == true).FirstOrDefaultAsync();
            foreach (var item in enrol)
            {
                dynamic modulesesion = new ExpandoObject();
                modulesesion.moduelId = item.ModuleId;
                modulesesion.Module = item.Module;
                var session = await db.SubscriptionTutorSession.Include(zz => zz.Subscription)
                   .Include(zz => zz.TutorSession).Where(zz => zz.SubscriptionId == item.SubscriptionId && zz.TutorSessionId == groupsession.Id).FirstOrDefaultAsync();
                modulesesion.TutorSessionId = session.TutorSessionId;
                modulesesion.TutorSession = session.TutorSession;
                sessionmodulelist.Add(modulesesion);

            }


            return Ok(sessionmodulelist);

        }

        #endregion

    }
}