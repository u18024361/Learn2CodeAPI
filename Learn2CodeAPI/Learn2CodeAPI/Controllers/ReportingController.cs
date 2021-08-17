using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.ReportDto;
using Learn2CodeAPI.Models;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Learn2CodeAPI.Controllers
{
    [Route("api/Reporting")]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        

            private readonly UserManager<AppUser> _userManager;
            private IMapper _mapper;
            private readonly AppDbContext db;
        private readonly ITwilioRestClient _client;

        public ReportingController(UserManager<AppUser> userManager, IMapper mapper,
                AppDbContext _db, ITwilioRestClient client)
            {

                _userManager = userManager;
                _mapper = mapper;
                db = _db;
            _client = client;
            }

        #region AdminHome
            [HttpGet]
            [Route("TotalStudents")]
            public async Task<IActionResult> TotalStudents()
            {
                int students = await db.Students.CountAsync();
                return Ok(students);
            }

            [HttpGet]
            [Route("TotalTutors")]
            public async Task<IActionResult> TotalTutors()
            {
                int students = await db.Tutor.CountAsync();
                return Ok(students);
            }

            [HttpGet]
            [Route("TotalUniversities")]
            public async Task<IActionResult> TotalUniversities()
            {
                int students = await db.University.CountAsync();
                return Ok(students);
            }
            [HttpGet]
            [Route("TotalDegrees")]
            public async Task<IActionResult> TotalDegrees()
            {
                int students = await db.Degrees.CountAsync();
                return Ok(students);
            }
            [HttpGet]
            [Route("TotalModules")]
            public async Task<IActionResult> TotalModules()
            {
                int students = await db.Degrees.CountAsync();
                return Ok(students);
            }

        #endregion

        #region Tutordetails
        [HttpGet]
        [Route("TutorDetails")]
        public async Task<IActionResult> TutorDetails()
        {
            var Tutors = await db.Tutor.ToListAsync();
            return Ok(Tutors);
        }
        #endregion

        #region studentdetails
        [HttpGet]
        [Route("StudentDetails")]
        public async Task<IActionResult> GetAllStudents()
        {

            var Students = await db.Students.Include(zz => zz.Identity).Include(zz => zz.StudentModule).ThenInclude(StudentModule => StudentModule.Module.Degree.University).ToListAsync();
            return Ok(Students);
        }


        #endregion

        #region Attendance
        [HttpGet]
        [Route("AttendacSession")]
        public async Task<IActionResult> AttendacSession()
        {

            var Sessions = await db.BookingInstance.Where(zz => zz.AttendanceTaken == true 
            && zz.TutorSession.SessionType.SessionTypeName == "Group" ).ToListAsync();
            return Ok(Sessions);
        }

        // for table 
        [HttpGet]
        [Route("SessionAttendanceReport/{BookingInstanceId}")]
        public async Task<IActionResult> SessionAttendanceReport(int BookingInstanceId)
        {

            var Attendance = await db.RegisteredStudent.Where(zz => zz.BookingInstanceId == BookingInstanceId).Include(zz => zz.Student).ThenInclude(zz => zz.Identity).ToListAsync();
            return Ok(Attendance);
        }

        //use ng2
        [HttpGet]
        [Route("SessionAttendanceGraph/{BookingInstanceId}")]
        public async Task <IActionResult> SessionAttendanceGraph(int BookingInstanceId)
        {

            var Attendance = await db.RegisteredStudent.Where(zz => zz.BookingInstanceId == BookingInstanceId).ToListAsync();
            int attended = await db.RegisteredStudent.Where(zz => zz.BookingInstanceId == BookingInstanceId && zz.Attended == true).CountAsync();
            int Missed = await db.RegisteredStudent.Where(zz => zz.BookingInstanceId == BookingInstanceId && zz.Attended == false).CountAsync();

            AttendanceGraphDto dto = new AttendanceGraphDto();
            dto.Attended = attended;
            dto.Missed = Missed;
            return Ok(dto);

            

        }

        #endregion

        #region Feedback
        [HttpGet]
        [Route("GetSessions")]
        public async Task<IActionResult> GetSessions()
        {
            var sessions = await db.BookingInstance.ToListAsync();
            return Ok(sessions);
        }

        //for description table
        [HttpGet]
        [Route("GetSessionsFeedback/{BookingInstanceId}")]
        public async Task<IActionResult> GetSessionsFeedback(int BookingInstanceId)
        {
            var sessions = await db.Feedback.Include(zz => zz.Student).Where(zz => zz.BookingInstanceId == BookingInstanceId).ToListAsync();
            return Ok(sessions);
        }

        [HttpGet]
        [Route("GetSessionsFeedbackScore/{BookingInstanceId}")]
        public async Task<IActionResult> GetSessionsFeedbackScore(int BookingInstanceId)
        {
            dynamic feedbackobject = new ExpandoObject();
            List<Feedback> sessions = await db.Feedback.Include(zz => zz.Student).Where(zz => zz.BookingInstanceId == BookingInstanceId).ToListAsync();
            feedbackobject.Timliness = sessions.Average(zz => zz.Timliness);
            feedbackobject.Ability = sessions.Average(zz => zz.Ability);
            feedbackobject.Friendliness = sessions.Average(zz => zz.Friendliness);
            return Ok(feedbackobject);
        }

        #endregion

        #region totlatutorsession
        [HttpGet]
        [Route("GetTotalTutorsessions")]
        public async Task<IActionResult> GetTotalTutorsessions([FromBody] TotalTutorSessionDto dto)
        {
            var enddate = dto.EndDate.AddHours(23.99);
            var Tutorsessions = new List<TutorSessionDto>();
            string StartDate = dto.StartDate.ToString("MM/dd/yyyy");
            string EndDate = dto.EndDate.ToString("MM/dd/yyyy");
            var sessions = await db.BookingInstance.Include(zz => zz.Module).Include(zz =>zz.Tutor).Where(zz => zz.TutorId == dto.TutorId).ToListAsync();

            foreach(var item in sessions)
            {
                TutorSessionDto x = new TutorSessionDto();
                string[] formats = { "MM/dd/yyyy" };
                x.Date= DateTime.ParseExact(item.Date, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                x.TutorName = item.Tutor.TutorName;
                x.TutorSurname = item.Tutor.TutorSurname;
                x.TutorEmail = item.Tutor.TutorEmail;
                x.ModuleCode = item.Module.ModuleCode;
                x.Title = item.Title;
                Tutorsessions.Add(x);
            }

            var list = Tutorsessions.Where(zz => zz.Date >= dto.StartDate && zz.Date <= enddate).ToList();
            return Ok(list);
        }
        #endregion

        #region Salesreport
        //for table
        [HttpGet]
        [Route("GetSalesReport")]
        public async Task<IActionResult> GetSalesReport([FromBody] SalesParameterDto dto)
        {
          
            var enddate = dto.EndDate.AddHours(23.99);
            var sales = new List<SalesDto>();
            DateTime convertedDate;
            var payments = await db.Payment.ToListAsync();
            foreach (var item in payments)
            {
                SalesDto x = new SalesDto();
                x.Amount = item.PaymentAmount;
                x.FullName = item.FullName;
                if (item.PaymentDate.Length == 25) {
                    string date = item.PaymentDate.Remove(5, 2);
                    convertedDate = Convert.ToDateTime(date);
                    x.Date = convertedDate;
                }
                else
                {
                    string date = item.PaymentDate.Remove(5, 3);
                    convertedDate = Convert.ToDateTime(date);
                    x.Date = convertedDate;
                }

                sales.Add(x);
            }
            var list = sales.Where(zz => zz.Date >= dto.StartDate && zz.Date <= enddate).ToList();
            return Ok(list);
        }

        [HttpGet]
        [Route("SubscriptionSales")]
        public IActionResult SubscriptionSales()
        {
            var sub = db.EnrolLine.Include(zz => zz.Subscription).AsEnumerable().GroupBy(zz => zz.Subscription.SubscriptionName);
            var subsales = new List<SubscriptionSalesDto>();
            foreach (var group in sub)
            {
                SubscriptionSalesDto vm = new SubscriptionSalesDto();
                vm.Subscription = group.Key;
                vm.Amount = group.Sum(zz => zz.Subscription.price).ToString();
                subsales.Add(vm);

            }
            return Ok(subsales);
        }
        #endregion



        [HttpGet]
        [Route("sms")]
        public IActionResult SendSms(SmsMessage model)
        {
            string x = model.To.Substring(1);
            string number = "+27"+x;
            var message = MessageResource.Create(
                to: new PhoneNumber(number),
                from: new PhoneNumber("+17729348745"),
                body: model.Message,
                client: _client); // pass in the custom client
            return Ok("Success");
        }
    }
}

