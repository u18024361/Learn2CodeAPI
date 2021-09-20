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
using Emailservice;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

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
        private IGenRepository<Models.Tutor.Message> Mess;
        private IGenRepository<Module> ModuleGenRepo;
        private IGenRepository<University> UniversityGenRepo;
        private IGenRepository<CourseFolder> CourseFolderGenRepo;
        private IGenRepository<Models.Tutor.Message> MessageGenRepo;
        private IGenRepository<CourseBasketLine> CourseBasketLineGenRepo;
        private IGenRepository<SubScriptionBasketLine> SubScriptionBasketLineGenRepo;
        private readonly IEmailSender _emailsender;
        private readonly ITwilioRestClient _client;


        public StudentController(IStudent _studentRepo, UserManager<AppUser> userManager, IMapper mapper, AppDbContext appDbContext,
            AppDbContext _db, IGenRepository<Tutor> _TutorGenRepo, IGenRepository<Models.Tutor.Message> _Mess,IGenRepository<Models.Tutor.Message> _Mes, 
            IGenRepository<Module> _ModuleGenRepo, IGenRepository<CourseFolder> _CourseFolderGenRepo,
            IGenRepository<CourseBasketLine> _CourseBasketLineGenRepo, IGenRepository<University> _UniversityGenRepo,
            IGenRepository<SubScriptionBasketLine> _SubScriptionBasketLineGenRepo,IEmailSender emailsender, ITwilioRestClient client)
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
            _emailsender = emailsender;
            UniversityGenRepo = _UniversityGenRepo;
            _client = client;

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

                result.message = "Something went wrong whith registartion";
                return BadRequest(result.message);
            }



        }

        [HttpGet]
        [Route("GetUniRegister")]
        public async Task<IActionResult> GetUniRegister()
        {

            var uni = await UniversityGenRepo.GetAll();
            return Ok(uni);

        }
        [HttpGet]
        [Route("GetDegreeRegister/{UniId}")]
        public async Task<IActionResult> GetDegreeRegister(int UniId)
        {

            var uni = await studentRepo.GetDegree(UniId);
            return Ok(uni);

        }
        [HttpGet]
        [Route("GetModuleRegister/{DegreeId}")]
        public async Task<IActionResult> GetModuleRegister(int DegreeId)
        {

            var uni = await studentRepo.GetModule(DegreeId);
            return Ok(uni);

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
                result.message = "Profile updated";
                return Ok(result);

            }
            catch
            {
                result.message = "Something went wrong updating while updating your Profile";
                return BadRequest(result.message);

            }

           
            


        }

        [HttpDelete]
        [Route("DeleteStudent/{StudentId}")]
        public async Task<IActionResult> DeleteStudent(int StudentId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var individualbooking = await db.BookingInstance.Where(zz => zz.Booking.StudentId == StudentId).ToListAsync();
                var tickets = await db.Ticket.Where(zz => zz.Enrollment.StudentId == StudentId).ToListAsync();
                var datelist = new List<deleteindividualDto>();
                var datenow = DateTime.Now;
                foreach (var item in individualbooking)
                {
                    deleteindividualDto dlist = new deleteindividualDto();
                    DateTime oDate = Convert.ToDateTime(item.Date);
                    dlist.date = oDate;
                    datelist.Add(dlist);
                }

                var individualcheck =  datelist.Where(zz => zz.date > datenow).ToList();
                if(individualcheck.Count > 0)
                {
                    return BadRequest("Please cancel current individual bookings before deleteing your account");
                }

                foreach(var item in individualbooking)
                {
                    item.BookingId = null;
                    item.TicketId = null;
                    db.BookingInstance.Remove(item);
                }
                await db.SaveChangesAsync();
                foreach (var item in tickets)
                {
                    db.Ticket.Remove(item);
                }
                await db.SaveChangesAsync();



                var student = await db.Students.Where(zz => zz.Id == StudentId).FirstOrDefaultAsync();
                var user = await db.Users.Where(zz => zz.Id == student.UserId).FirstOrDefaultAsync();
                var reg = await db.RegisteredStudent.Where(zz => zz.StudentId == student.Id).ToListAsync();
                foreach( var item in reg)
                {
                    db.RegisteredStudent.Remove(item);
                }
                db.Students.Remove(student);
                db.Users.Remove(user);

                await db.SaveChangesAsync();
                result.message = "Student Deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong deleting the account";
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
                result.message = "Message deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong deleting the message";
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
                var basket = db.CourseBasketLine.Where(zz => zz.BasketId == dto.BasketId && zz.CourseSubCategoryId == dto.CourseSubCategoryId).FirstOrDefault();
                if (basket != null)
                {
                    return BadRequest("Short course already exists in basket");
                }


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
                var basket = db.SubScriptionBasketLine.Where(zz => zz.BasketId == dto.BasketId && zz.ModuleId == dto.ModuleId && zz.SubscriptionId == dto.SubscriptionId).FirstOrDefault();
                if (basket != null)
                {
                    return BadRequest("Subscription for the specific module already exists in basket");
                }
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

        [HttpGet]
        [Route("GetCouesePrice/{BasketId}")]
        public async Task<IActionResult> GetCouesePrice(int BasketId)
        {

            var course = await db.CourseBasketLine.Where(zz => zz.BasketId == BasketId).Include(zz => zz.CourseSubCategory).SumAsync(zz => zz.CourseSubCategory.price);
            return Ok(course);

        }

        [HttpGet]
        [Route("GetsubPrice/{BasketId}")]
        public async Task<IActionResult> GetsubPrice(int BasketId)
        {

            var course = await db.SubScriptionBasketLine.Where(zz => zz.BasketId == BasketId).Include(zz => zz.Subscription).SumAsync(zz => zz.Subscription.price);
            return Ok(course);

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
                if (dto.Message != "Approved")
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
        [Route("Getcourseontent/{courseSubId}")]
        public async Task<IActionResult> Getcourseontent(int courseSubId)
        {

            var Coursescontent = await db.CourseContent.Include(zz => zz.ContentType).Include(zz =>zz.CourseSubCategory).Where(zz => zz.CourseSubCategoryId == courseSubId).ToListAsync();
            return Ok(Coursescontent);

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
            var entity = await db.CourseContent.Where(zz => zz.Id == id).FirstOrDefaultAsync();
            MemoryStream ms = new MemoryStream(entity.Content);
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

        //for dropdown
        [HttpGet]
        [Route("GetbookingIndividual/{StudentId}")]
        public async Task<IActionResult> GetbookingIndividual(int StudentId)
        {
            dynamic result = new ExpandoObject();
            var today = DateTime.Now;
            var enrol = await db.EnrolLine.Include(zz => zz.Enrollment).Include(zz => zz.Module).Where(zz => zz.Enrollment.StudentId == StudentId
            && zz.TicketQuantity > 0 && zz.EndDate >= today && zz.Subscription.SubscriptionTutorSession
               .Any(zz => zz.TutorSession.SessionType.SessionTypeName == "Individual")).ToListAsync();
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


        //displays list of  sessions to book
        [HttpGet]
        [Route("GetIndividualAvailable/{ModuleId}/{TutorSessionId}")]
        public async Task<IActionResult> GetbookingIndividual(int ModuleId, int TutorSessionId)
        {
            var available = await db.BookingInstance.Include(zz => zz.SessionTime).Include(zz => zz.Tutor).Include(zz => zz.Module).Where(zz => zz.ModuleId == ModuleId && zz.TutorSessionId == TutorSessionId && zz.BookingStatus.bookingStatus == "Open").ToListAsync();
            var sessionmodulelist = new List<Individuallist>();
            foreach (var item in available)
            {
                var modulesesion = new Individuallist();
                modulesesion.title = item.Title;
                modulesesion.endTime = item.SessionTime.EndTime;
                modulesesion.startTime = item.SessionTime.StartTime;
                modulesesion.tutorName = item.Tutor.TutorName;
                modulesesion.tutorSurname = item.Tutor.TutorSurname;
                modulesesion.moduleCode = item.Module.ModuleCode;
                modulesesion.date = DateTime.Parse(item.Date) ;

                modulesesion.id = item.Id;
                sessionmodulelist.Add(modulesesion);

            }

            var list = sessionmodulelist.Where(zz => zz.date.Day >= DateTime.Now.Day).ToList();
            return Ok(list);
        }

        [HttpGet]
        [Route("Ticketsleft/{ModuleId}/{StudentId}")]
        public async Task<IActionResult> Ticketsleft(int ModuleId,  int StudentId)
        {
            var today = DateTime.Now;
            var enrolineId = await db.EnrolLine.Include(zz => zz.Subscription.SubscriptionTutorSession)
               .Where(zz => zz.Enrollment.StudentId == StudentId && zz.EndDate >= today && zz.ModuleId == ModuleId && zz.TicketQuantity > 0 && zz.Subscription.SubscriptionTutorSession
               .Any(zz => zz.TutorSession.SessionType.SessionTypeName == "Individual"))
               .Select(zz => zz.TicketQuantity).FirstOrDefaultAsync();
            return Ok(enrolineId);
        }


        //to make a booking
        [HttpPost]
        [Route("CreateIndividualBooking")]
        public async Task<IActionResult> CreateIndividualBooking([FromBody] BookIndividualDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var today = DateTime.Now;
                var check = await db.BookingInstance.Include(zz => zz.SessionTime).Where(zz => zz.Id == dto.BookingInstanceId).FirstOrDefaultAsync();
                var xx = "";
                var sessiontime = await db.SessionTime.Where(zz => zz.Id == check.SessionTimeId).FirstOrDefaultAsync();
                if (sessiontime.StartTime.Length == 3)
                {
                    xx = sessiontime.StartTime.Substring(0, 1);
                }
                else
                {
                    xx = sessiontime.StartTime.Substring(0, 2);
                }

                var hour = Convert.ToInt32(xx);
                var todayy = DateTime.Parse(check.Date); 
                var time = new DateTime(todayy.Year, todayy.Month, todayy.Day, hour, 0, 0);

                if (time.Day == DateTime.Today.Day)
                {
                    if (time < DateTime.Now)
                    {
                        result.message = "Unable to book as session time has passed";
                        return BadRequest(result.message);
                    }

                }


                //check enroline quantity
                var enrolineId = await db.EnrolLine.Include(zz => zz.Subscription.SubscriptionTutorSession)
               .Where(zz => zz.Enrollment.StudentId == dto.StudentId && zz.EndDate >= today && zz.ModuleId == dto.ModuleId && zz.TicketQuantity > 0 && zz.Subscription.SubscriptionTutorSession
               .Any(zz => zz.TutorSession.SessionType.SessionTypeName == "Individual"))
               .Select(zz => zz.Id).FirstOrDefaultAsync();

                //enrolmentid becomes enrollineid
                var ticketlist = await db.Ticket.Include(zz => zz.TicketStatus)
                    .Where(zz => zz.EnrolLineId == enrolineId && zz.TicketStatus.ticketStatus == true).ToListAsync();

                if (ticketlist.Count > 0)
                {
                    var newBooking = new Booking();
                    newBooking.StudentId = dto.StudentId;
                    await db.Booking.AddAsync(newBooking);
                    await db.SaveChangesAsync();

                    int ticketstatus = await db.TicketStatus.Where(zz => zz.ticketStatus == false).Select(zz => zz.Id).FirstOrDefaultAsync();
                    int bookedstatus = await db.BookingStatus.Where(zz => zz.bookingStatus == "Booked").Select(zz => zz.Id).FirstOrDefaultAsync();
                    var instance = await db.BookingInstance.Where(zz => zz.Id == dto.BookingInstanceId).Include(zz=> zz.Tutor).Include(zz => zz.SessionTime).FirstOrDefaultAsync();
                    var cellremove = instance.Tutor.TutorCell.Substring(1);
                    var cell = "+27" + cellremove;
                    instance.BookingStatusId = bookedstatus;
                    instance.BookingId = newBooking.Id;
                    instance.TicketId = ticketlist[0].Id;
                    instance.Description = dto.Description;

                    ticketlist[0].TicketStatusId = ticketstatus;
                    var enrolinequantity = await db.EnrolLine.Where(zz => zz.Id == enrolineId).FirstOrDefaultAsync();
                    int x = enrolinequantity.TicketQuantity - 1;
                    enrolinequantity.TicketQuantity = x;
                    await db.SaveChangesAsync();
                    var body = "Hi " + instance.Tutor.TutorName + " your individual session at " + instance.SessionTime.StartTime + "-" + instance.SessionTime.EndTime + " on " + instance.Date +  " has been booked";
                   // var message = MessageResource.Create(
                   //to: new PhoneNumber(cell),
                   //from: new PhoneNumber("+17729348745"),
                   //body: body,
                   //client: _client);
                    result.data = instance;
                    result.message = "Booking successfull";
                    return Ok(result);
                }
                else
                {
                    return BadRequest("no tickets");
                }
            }
            catch
            {
                result.message = "Something went wrong while making the booking";
                return BadRequest(result.message);
            }


        }

        //to display bookings made
        [HttpGet]
        [Route("GetMyBookings/{UserId}")]
        public async Task<IActionResult> GetMyBookings(string UserId)
        {
            var studentId = await db.Students.Where(zz => zz.UserId == UserId).FirstOrDefaultAsync();
            var myBookings = await studentRepo.GetMyBookings(studentId.Id);
            var sessionmodulelist = new List<Individuallist>();
            foreach (var item in myBookings)
            {
                var modulesesion = new Individuallist();
                modulesesion.title = item.Title;
                modulesesion.endTime = item.SessionTime.EndTime;
                modulesesion.startTime = item.SessionTime.StartTime;
                modulesesion.tutorName = item.Tutor.TutorName;
                modulesesion.tutorSurname = item.Tutor.TutorSurname;
                modulesesion.moduleCode = item.Module.ModuleCode;
                modulesesion.date = DateTime.Parse(item.Date);
                modulesesion.link = item.Link;
                modulesesion.id = item.Id;
                sessionmodulelist.Add(modulesesion);

            }

            var list = sessionmodulelist.Where(zz => zz.date.Day >= DateTime.Now.Day).ToList();

            return Ok(list);

        }

        [HttpGet]
        [Route("CancelMyBooking/{BookingInstanceId}")]
        public async Task<IActionResult> CancelMyBooking(int BookingInstanceId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var session = db.BookingInstance.Where(zz => zz.Id == BookingInstanceId).FirstOrDefault();
                DateTime oDate = Convert.ToDateTime(session.Date);
                var start = DateTime.Now;
                if ((oDate - start).TotalDays <= 1)
                {
                    result.message = "Can't cancel booking as there is less than 24 hours";
                    return BadRequest(result.message);
                }
                var data = await studentRepo.CancelBooking(BookingInstanceId);
                result.data = data;
                result.message = "Cancellation successfull";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong while canceling the booking";
                return BadRequest(result.message);
            }


        }

        [HttpPost]
        [Route("BookingChangeRequest")]
        public async Task<IActionResult> BookingChangeRequest([FromBody] BookingChangeDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                dto.date = dto.date.AddDays(1);
                string datestring = dto.date.ToString("MM/dd/yyyy");
                var student = await db.Students.Include(zz => zz.Identity).Where(zz => zz.Id == dto.StudentId).FirstOrDefaultAsync();
                SessionTime time = await db.SessionTime.Where(zz => zz.Id == dto.SessionTimeId).FirstOrDefaultAsync();
                BookingInstance email = await db.BookingInstance.Include(zz => zz.SessionTime).Include(zz => zz.Tutor).Where(zz => zz.Id == dto.Id).FirstOrDefaultAsync();
                string subject = "Change request: " + email.SessionTime.StartTime + "-" + email.SessionTime.EndTime + " " + email.Date;
                string content = dto.Message + " " +"Time: " + time.StartTime + "-" + time.EndTime + " " +"Date: " + datestring + " " + student.Identity.Email;

                var message = new Emailservice.Message(new string[] { email.Tutor.TutorEmail }, subject, content);
                await _emailsender.SendEmailAsync(message);

                   
                    result.message = "Email sent";
                    return Ok(result);
            
            }
            catch
            {
                result.message = "Something went wrong while making the booking";
                return BadRequest(result.message);
            }


        }









        #endregion

        #region MakeGroup
        //to get group
        [HttpGet]
        [Route("GetbookingGroup/{StudentId}")]
        public async Task<IActionResult> GetbookingGroup(int StudentId)
        {
            dynamic result = new ExpandoObject();
            var today = DateTime.Now;
            var enrol = await db.EnrolLine.Include(zz => zz.Enrollment).Include(zz => zz.Module).Where(zz => zz.Enrollment.StudentId == StudentId
            && zz.EndDate >= today && zz.TicketQuantity >0 && zz.Subscription.SubscriptionTutorSession
               .Any(zz => zz.TutorSession.SessionType.SessionTypeName == "Group")).ToListAsync();
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
                session.TutorSession.SubscriptionTutorSession = null;
                modulesesion.TutorSession = session.TutorSession;
                sessionmodulelist.Add(modulesesion);

            }


            return Ok(sessionmodulelist);

        }

        [HttpGet]
        [Route("GetMyGroupSessions/{UserId}")]
        public async Task<IActionResult> GetMyGroupSessions(string UserId)
        {
            var studentId = await db.Students.Where(zz => zz.UserId == UserId).FirstOrDefaultAsync();
            var mygroup = await studentRepo.Getmygroupsession(studentId.Id);
            return Ok(mygroup);

        }


        #endregion

        #region feedback
        [HttpPost]
        [Route("CreateFeedback")]
        public async Task<IActionResult> CreateFeedback([FromBody] Feedback feedback)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = db.Feedback.Where(zz => zz.StudentId == feedback.StudentId && zz.BookingInstanceId == feedback.BookingInstanceId).FirstOrDefault();
                if (user != null)
                {
                    return BadRequest("Feedback already exists");
                }
                var data = await studentRepo.CreateFeedback(feedback);
                result.data = data;
                result.message = "Feedback submitted";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong while submitting the feedback.";
                return BadRequest(result.message);
            }

        }

        [HttpGet]
        [Route("GetMyRegiseredSessions/{StudentId}")]
        public async Task<IActionResult> GetMyRegiseredSessions(int StudentId)
        {

            var feedback = await studentRepo.GetmyReg(StudentId);
            return Ok(feedback);

        }

        [HttpGet]
        [Route("GetMyFeedback/{StudentId}")]
        public async Task<IActionResult> GetMyFeedback(int StudentId)
        {

            var feedback = await studentRepo.MyFeedback(StudentId);
            return Ok(feedback);

        }

        [HttpDelete]
        [Route("DeleteMyFeedback/{StudentId}/{BookingInstanceId}")]
        public async Task<IActionResult> DeleteMyFeedback(int StudentId, int BookingInstanceId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var feedback = await studentRepo.DeleteFeedback(StudentId, BookingInstanceId);
                result.data = feedback;
                result.message = "Feedback deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong deleting the feedback";
                return BadRequest(result.message);
            }

        }

        #endregion

        #region mysubscriptions

        // list to display subscriptions based on studeng id
        [HttpGet]
        [Route("GetMySubscriptions/{StudentId}")]
        public async Task<IActionResult> GetMySubscriptions(int StudentId)
        {
            var datenow = DateTime.Now;
            var subscriptions = await db.EnrolLine.Include(zz => zz.Subscription ).Include(zz =>zz.Module)
                .Where(zz => zz.Enrollment.StudentId == StudentId && zz.EndDate > datenow && zz.Subscription.SubscriptionTutorSession.Any(zz => zz.TutorSession.SessionType.SessionTypeName == "Group")).ToListAsync();
            return Ok(subscriptions);

        }

        [HttpGet]
        [Route("GetMyIndividualSubscriptions/{StudentId}")]
        public async Task<IActionResult> GetMyIndividualSubscriptions(int StudentId)
        {
            var datenow = DateTime.Now;
            var subscriptions = await db.EnrolLine.Include(zz => zz.Subscription).Include(zz => zz.Module)
                .Where(zz => zz.Enrollment.StudentId == StudentId && zz.EndDate > datenow && zz.Subscription.SubscriptionTutorSession.Any(zz => zz.TutorSession.SessionType.SessionTypeName == "Individual")).ToListAsync();
            return Ok(subscriptions);

        }
        #endregion

        #region viewgroupSession content

        //gets the booking instances student registered for based on student id
        [HttpGet]
        [Route("GetMyGroupSessionsContent/{StudentId}")]
        public async Task<IActionResult> GetMyGroupSessions(int StudentId)
        {

            var sessions = await db.RegisteredStudent.Include(zz => zz.BookingInstance).Include(zz => zz.BookingInstance.Module)
                .Include(zz => zz.BookingInstance.Tutor).Include(zz => zz.BookingInstance.SessionTime)
                .Where(zz => zz.StudentId == StudentId).ToListAsync();
            return Ok(sessions);

        }

        // when clciking on content button of specific bookinginstance sends id to this function which will display the content in card format
        [HttpGet]
        [Route("GetMyGroupContent/{BookingInstanceId}")]
        public async Task<IActionResult> GetMyGroupContent(int BookingInstanceId)
        {

            var sessionscontent = await db.GroupSessionContent.Include(zz=> zz.BookingInstance)
                .Include(zz => zz.SessionContentCategory).Where(zz=>zz.BookingInstanceId == BookingInstanceId).FirstOrDefaultAsync();
            return Ok(sessionscontent);

        }

        //clicking the watch button sends the id of the groupsessioncontent to this function to watch video
        [HttpGet]
        [Route("VideoGroup/{id}")]
        public async Task<FileStreamResult> VideoGroup(int id)
        {
            var entity = await db.GroupSessionContent.Where(zz => zz.Id == id).FirstOrDefaultAsync();
            MemoryStream ms = new MemoryStream(entity.Recording);
            return new FileStreamResult(ms, "video/mp4");
        }

        //clicking the download button sends the id of the groupsessioncontent to this function to download notes
        [HttpGet]
        [Route("DownloadRContentPdfGroup/{id}")]
        public async Task<FileStreamResult> DownloadRContentPdfGroup(int id)
        {
            var entity = await db.GroupSessionContent.Where(zz => zz.Id == id).FirstOrDefaultAsync();
            MemoryStream ms = new MemoryStream(entity.Notes);
            return new FileStreamResult(ms, "Application/pdf");
        }


        #endregion
    }
}