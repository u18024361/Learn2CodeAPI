using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Emailservice;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.StudentDto;
using Learn2CodeAPI.Dtos.TutorDto;
using Learn2CodeAPI.IRepository.Generic;
using Learn2CodeAPI.IRepository.IRepositoryTutor;
using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learn2CodeAPI.Controllers
{
    [Route("api/Tutor")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        private IMapper mapper;
        private ITutor TutorRepo;
        private IGenRepository<Student> StudentGenRepo;
        private IGenRepository<Module> ModuleRepo;
        private IGenRepository<Models.Tutor.Message> MessageGenRepo;
        private IGenRepository<Resource> ResourceGenRepo;
        private IGenRepository<ResourceCategory> ResourceCategoryGenRepo;
        private IGenRepository<BookingInstance> BookingInstanceGenRepo;
        private IGenRepository<GroupSessionContent> GroupSessionContentGenRepo;
        private readonly IWebHostEnvironment webHostEnvironment;
        private IGenRepository<Tutor> TutorGenRepo;
        private readonly AppDbContext db;
        private readonly IEmailSender _emailsender;

        public TutorController(
            IMapper _mapper,
            ITutor _TutorRepo,
            IWebHostEnvironment hostEnvironment,
            IGenRepository<Student> _StudentGenRepo,
            IGenRepository<Tutor> _Tutor,
            IGenRepository<Module> _ModuleRepo,
            IGenRepository<Models.Tutor.Message> _Message,
            IGenRepository<GroupSessionContent> _GroupSessionContentGenRepo,
            IGenRepository<ResourceCategory> _ResourceCategoryGenRepo,
            AppDbContext _db,
            IGenRepository<BookingInstance> _BookingInstanceGenRepo,
            IGenRepository<Resource> _Resource,
             IEmailSender emailsender
             )

        {
            db = _db;
            mapper = _mapper;
            TutorRepo = _TutorRepo;
            StudentGenRepo = _StudentGenRepo;
            MessageGenRepo = _Message;
            ResourceCategoryGenRepo = _ResourceCategoryGenRepo;
            webHostEnvironment = hostEnvironment;
            ModuleRepo = _ModuleRepo;
            BookingInstanceGenRepo = _BookingInstanceGenRepo;
            ResourceGenRepo = _Resource;
            GroupSessionContentGenRepo = _GroupSessionContentGenRepo;
            TutorGenRepo = _Tutor;
            _emailsender = emailsender;
        }

        [HttpGet]
        [Route("GetTutor/{UserId}")]
        public async Task<IActionResult> GetResourceCategorybyId(string UserId)
        {
            var entity = await db.Tutor.Include(zz => zz.Identity).Where(zz => zz.UserId == UserId).FirstOrDefaultAsync();

            return Ok(entity);
        }

        #region ResourceCategory
        [HttpGet]
        [Route("GetResourceCategorybyId/{ResourceCategoryId}")]
        public async Task<IActionResult> GetResourceCategorybyId(int ResourceCategoryId)
        {
            var entity = await ResourceCategoryGenRepo.Get(ResourceCategoryId);

            return Ok(entity);
        }

        [HttpGet]
        [Route("SearchResourceCategory/{ResourceCategoryName}")]
        public async Task<IActionResult> SearchResourceCategory(string ResourceCategoryName)
        {
            var entity = await TutorRepo.GetByName(ResourceCategoryName);

            return Ok(entity);
        }

        [HttpGet]
        [Route("GetAllResourceCategories")]
        public async Task<IActionResult> GetAllResourceCategories()
        {
            var ResourceCat = await ResourceCategoryGenRepo.GetAll();
            return Ok(ResourceCat);

        }

        [HttpPost]
        [Route("CreateResourceCategory")]
        public async Task<IActionResult> CreateResourceCategory([FromBody] ResourceCategoryDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var check = db.ResourceCategory.Where(zz => zz.ResourceCategoryName == dto.ResourceCategoryName).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Resource Category already exists";
                    return BadRequest(result.message);
                }
                ResourceCategory entity = mapper.Map<ResourceCategory>(dto);
                var data = await ResourceCategoryGenRepo.Add(entity);
                result.data = data;
                result.message = "Resource Category created";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong creating the Resource Category";
                return BadRequest(result.message);
            }

        }

        [HttpPut]
        [Route("EditResourceCategory")]
        public async Task<IActionResult> EditResourceCategory([FromBody] ResourceCategoryDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = db.ResourceCategory.Where(zz => zz.ResourceCategoryName == dto.ResourceCategoryName).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Resource Category already exists";
                    return BadRequest(result.message);
                }
                ResourceCategory entity = mapper.Map<ResourceCategory>(dto);
                var data = await ResourceCategoryGenRepo.Update(entity);
                result.data = data;
                result.message = "Resource Category updated";
                return Ok(result);

            }
            catch
            {
                result.message = "Something went wrong updating the Resource Category";
                return BadRequest(result.message);

            }




        }



        [HttpDelete]
        [Route("DeleteResourceCategory/{ResourceCategoryId}")]
        public async Task<IActionResult> DeleteResourceCategory(int ResourceCategoryId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var data = await ResourceCategoryGenRepo.Delete(ResourceCategoryId);
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

        #region Resource
        [HttpGet]
        [Route("GetAllModulesForResources")]
        public async Task<IActionResult> GetAllModulesForResources()
        {
            var entity = await TutorRepo.GetModules();

            return Ok(entity);
        }

        [HttpGet]
        [Route("GetAllUniversitiesForResources")]
        public async Task<IActionResult> GetAllUniversitiesForResources()
        {
            var entity = await db.University.ToListAsync();

            return Ok(entity);
        }

        [HttpGet]
        [Route("GetUniversityForResources/{UniversityId}")]
        public async Task<IActionResult> GetUniversityForResources(int UniversityId)
        {
            var entity = await db.Modules.Include(zz => zz.Degree).Include(zz => zz.Degree.University).Where(zz => zz.Degree.University.Id == UniversityId).ToListAsync();

            return Ok(entity);
        }

        //testing purposes
        [HttpGet]
        [Route("GetResourcesall")]
        public async Task<IActionResult> GetResources()
        {
            var entity = await db.Resource.Include(zz => zz.Module).Include(zz => zz.ResourceCategory).ToListAsync();

            return Ok(entity);
        }


        

        [HttpGet]
        [Route("GetModuleResources/{ModuleId}")]
        public async Task<IActionResult> GetModuleResources(int ModuleId)
        {
            var entity = await TutorRepo.GetModuleResources(ModuleId);

            return Ok(entity);
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

        

        [HttpPost]
        [Route("CreateResource")]
        public async Task<IActionResult> CreateResource([FromForm] ResourceDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               
                Resource resource = new Resource();
               
                resource.ModuleId = dto.ModuleId;
                resource.ResourceCategoryId = dto.ResourceCategoryId;
                resource.ResourceDescription = dto.ResourceDescription;
                using (var target = new MemoryStream())
                {
                    dto.ResoucesName.CopyTo(target);
                    resource.ResoucesName = target.ToArray();
                }
                await db.Resource.AddAsync(resource);
                await db.SaveChangesAsync();
                result.data = resource;
                result.message = "Resource Category created";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went while adding the resource";
                return BadRequest(result.message);
            }

        }

        [HttpPut]
        [Route("EditResource")]
        public async Task<IActionResult> EditResource([FromForm] ResourceDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var resource = await db.Resource.Where(zz => zz.Id == dto.Id).FirstOrDefaultAsync();
                resource.ModuleId = dto.ModuleId;
                resource.ResourceCategoryId = dto.ResourceCategoryId;
                resource.ResourceDescription = dto.ResourceDescription;
                using (var target = new MemoryStream())
                {
                    dto.ResoucesName.CopyTo(target);
                    resource.ResoucesName = target.ToArray();
                }
                var check = db.Resource.Where(zz => zz.ResoucesName == resource.ResoucesName && zz.Id != dto.Id).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Resource already exists";
                    return BadRequest(result.message);
                }
                await db.SaveChangesAsync();
                result.data = resource;
                result.message = "Resource updated";
                return Ok(result);

            }
            catch
            {
                result.message = "Something went wrong updating the resource";
                return BadRequest(result.message);

            }
        }

        [HttpDelete]
        [Route("DeleteResource/{ResourceId}")]
        public async Task<IActionResult> DeleteResource(int ResourceId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                var data = await ResourceGenRepo.Delete(ResourceId);

                result.data = data;
                result.message = "Resource deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong deleting the resource";
                return BadRequest(result.message);
            }


        }


        #endregion

        #region Messages
        //for creating a message
        [HttpGet]
        [Route("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {

            var students = await TutorRepo.GetAllStudents();
            return Ok(students);

        }

        [HttpGet]
        [Route("GetSelectedStudent/{StudentId}")]
        public async Task<IActionResult> GetSelectedStudent(int Id)
        {

            var student = await StudentGenRepo.Get(Id);
            return Ok(student);

        }

        [HttpGet]
        [Route("GetSentMessages/{UserId}")]
        public async Task<IActionResult> GetSentMessages(string UserId)
        {

            var messages = await TutorRepo.GetSentMessages(UserId);
            return Ok(messages);

        }

        [HttpGet]
        [Route("GetRecievedMessages/{UserId}")]
        public async Task<IActionResult> GetRecievedMessages(string UserId)
        {

            var messages = await TutorRepo.GetRecievedMessages(UserId);
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
                var data = await TutorRepo.CreateMessage(dto);
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

        #region Application
        [HttpGet]
        [Route("GetAllModules")]
        public async Task<IActionResult> GetAllModules()
        {

            var students = await ModuleRepo.GetAll();
            return Ok(students);

        }

        [HttpPost]
        [Route("TutorApplication")]
        public async Task<IActionResult> TutorApplication([FromForm] TutorDtoo model)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var check = db.Tutor.Where(zz => zz.TutorEmail == model.TutorEmail).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Application already exists";
                    return BadRequest(result.message);
                }

               
                Models.Tutor.File file = new Models.Tutor.File();
                using (var Filetarget = new MemoryStream())
                {
                    model.File.CopyTo(Filetarget);
                    file.FileName = Filetarget.ToArray();
                }
                await db.File.AddAsync(file);
                await db.SaveChangesAsync();
                int idpending = await db.TutorStatus.Where(zz => zz.TutorStatusDesc == "Applied").Select(zz => zz.Id).FirstOrDefaultAsync();
                Tutor tutor = new Tutor();

                tutor.TutorName = model.TutorName;
                tutor.TutorSurname = model.TutorSurname;
                tutor.TutorAbout = model.TutorAbout;
                tutor.TutorCell = model.TutorCell;
                tutor.TutorEmail = model.TutorEmail;
                tutor.FileId = file.Id;
                tutor.TutorStatusId = idpending;
                using (var Imagetarget = new MemoryStream())
                {
                    model.TutorPhoto.CopyTo(Imagetarget);
                    tutor.TutorPhoto = Imagetarget.ToArray();
                }

                await db.Tutor.AddAsync(tutor);
                await db.SaveChangesAsync();

                TutorModule tutorModule = new TutorModule();
                tutorModule.TutorId = tutor.Id;
                tutorModule.ModuleId = model.ModuleId;
                await db.TutorModule.AddAsync(tutorModule);
                await db.SaveChangesAsync();

                result.data = tutor;
                result.message = "Application sent";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong creating the application";
                return BadRequest(result.message);
            }

        }





        private string UploadedFile(TutorDtoo model)
        {
            string uniqueFileName = null;

            if (model.TutorPhoto != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.TutorPhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.TutorPhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion

        #region Bookinginstance

        //get modules for dropdown
        [HttpGet]
        [Route("GetTutorModule/{TutorId}")]
        public async Task<IActionResult> GetTutorModule(int TutorId)
        {
            var entity = await TutorRepo.GetTutorModule(TutorId);

            return Ok(entity);
        }

        [HttpGet]
        [Route("GetSessionTime")]
        public async Task<IActionResult> GetSessionTime()
        {
            var entity = await TutorRepo.GetSessionTime();

            return Ok(entity);
        }

        [HttpGet]
        [Route("GetsessionType")]
        public async Task<IActionResult> GetsessionType()
        {
            var type = await db.TutorSession.Include(zz => zz.SessionType).ToListAsync();

            return Ok(type);
        }


        [HttpGet]
        [Route("GetGroupSessions/{TutorId}")]
        public async Task<IActionResult> GetGroupSessions(int TutorId)
        {
            var entity = await db.BookingInstance.Include(zz => zz.SessionTime).Include(zz => zz.Module).Include(zz => zz.BookingStatus)
                .Where(zz => zz.TutorId == TutorId && zz.TutorSession.SessionType.SessionTypeName == "Group").ToListAsync();

            return Ok(entity);
        }

        [HttpGet]
        [Route("GetIndividualSessions/{TutorId}")]
        public async Task<IActionResult> GetIndividualSessions(int TutorId)
        {
            var entity = await db.BookingInstance.Include(zz => zz.SessionTime).Include(zz => zz.Module).Include(zz => zz.BookingStatus)
                 .Where(zz => zz.TutorId == TutorId && zz.TutorSession.SessionType.SessionTypeName == "Individual").ToListAsync();

            return Ok(entity);
        }

        [HttpGet]
        [Route("GetAllSessions/{TutorId}")]
        public async Task<IActionResult> GetAllSessions(int TutorId)
        {
            var entity = await db.BookingInstance.Include(zz => zz.SessionTime).Include(zz => zz.Module).Include(zz => zz.BookingStatus)
                 .Where(zz => zz.TutorId == TutorId).ToListAsync();

            return Ok(entity);
        }

       
        [HttpPost]
        [Route("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingInstanceDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                string timestring = dto.Date.ToString("MM/dd/yyyy");
                var check = db.BookingInstance.Where(zz => zz.SessionTimeId == dto.SessionTimeId &&
                zz.Date == timestring && zz.TutorId == dto.TutorId).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Session already exists at that time on that day";
                    return BadRequest(result.message);
                }


                var BookingInstance = await TutorRepo.CreateBooking(dto);
                var type = db.TutorSession.Include(zz => zz.SessionType).Where(zz => zz.SessionType.SessionTypeName == "Group").FirstOrDefault();
                string subject = "Group Session:"+ BookingInstance.Module.ModuleCode;
                string content = "Dear Student" + Environment.NewLine +
                    "Please note that a group session for " + BookingInstance.Module.ModuleCode + " has been scheduled for " + BookingInstance.Date + " from " + BookingInstance.SessionTime.StartTime
                    + " to " + BookingInstance.SessionTime.EndTime
                    + Environment.NewLine +
                     Environment.NewLine +
                    "Please use the following link to join: " + " " + BookingInstance.Link
                     + Environment.NewLine +
                      Environment.NewLine +
                      "Regards TutorDevOps";

                if (BookingInstance.TutorSessionId == type.Id)
                {
                    //dynamic result = new ExpandoObject();
                    var today = DateTime.Now;
                    var enrol = await db.EnrolLine.Include(zz => zz.Enrollment).Include(zz => zz.Module).Where(zz => zz.ModuleId == BookingInstance.ModuleId && zz.TicketQuantity >0
                    && zz.EndDate >= today && zz.TicketQuantity > 0 && zz.Subscription.SubscriptionTutorSession.Any(zz => zz.TutorSession.SessionType.SessionTypeName == "Group")).ToListAsync();
                    var sessionmodulelist = new List<dynamic>();
                    var groupsession = await db.TutorSession.Include(zz => zz.SessionType).Where(zz => zz.SessionType.IsGroup == true).FirstOrDefaultAsync();
                    var ticketstatus = await db.TicketStatus.Where(zz => zz.ticketStatus == false).FirstOrDefaultAsync();
                    foreach (var item in enrol)
                    {
                        var student = db.Students.Include(zz => zz.Identity).Where(zz => zz.Id == item.Enrollment.StudentId).FirstOrDefault();
                        RegisteredStudent regstudent = new RegisteredStudent();
                        regstudent.BookingInstanceId = BookingInstance.Id;
                        regstudent.StudentId = student.Id;
                        db.RegisteredStudent.Add(regstudent);
                        

                        var tickets = await db.Ticket.Include(zz => zz.TicketStatus)
                            .Where(zz => zz.EnrolLineId == item.Id && zz.TicketStatus.ticketStatus == true).FirstOrDefaultAsync();

                        tickets.TicketStatusId = ticketstatus.Id;
                        item.TicketQuantity = item.TicketQuantity - 1;
                        await db.SaveChangesAsync();
                        string email = student.Identity.Email;
                        var message = new Emailservice.Message(new string[] {email}, subject, content);
                        await _emailsender.SendEmailAsync(message);
                    }
                }
                else
                {
                    result.data = BookingInstance;
                    result.message = "Individual Session created";
                    return Ok(result);
                }
                result.data = BookingInstance;
                result.message = "Group Session created";
                return Ok(result);
            }
            catch
            {
                //send emails

                result.message = "Something went wrong creating the session";
                return BadRequest(result.message);
            }

        }

        //edit
        [HttpPut]
        [Route("EditSession")]
        public async Task<IActionResult> EditSession([FromBody] BookingInstanceDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var session = db.BookingInstance.Where(zz => zz.Id == dto.Id).FirstOrDefault();
                string datestring = dto.Date.ToString("MM/dd/yyyy");
                DateTime oDate = DateTime.ParseExact(session.Date, "MM/dd/yyyy", CultureInfo.CurrentCulture);
                var start = DateTime.Now;
                if ((oDate - start).TotalDays <= 1.5 && (oDate - start).TotalDays > 0)
                {
                    result.message = "Can't update as there is less than 24 hours";
                    return BadRequest(result.message);
                }

                var check = db.BookingInstance.Where(zz => zz.SessionTimeId == dto.SessionTimeId &&
                zz.Date == datestring && zz.Id != dto.Id && zz.TutorId == dto.TutorId).FirstOrDefault();
                if (check != null)
                {
                    result.message = "Session already exists on the provided date and time";
                    return BadRequest(result.message);
                }

               
                var data = await TutorRepo.UpdateBooking(dto);
                result.data = data;
                result.message = "Session updated";
                return Ok(result);

            }
            catch
            {
                result.message = "Something went wrong updating the Session";
                return BadRequest(result.message);

            }
        }

        // need to look
        [HttpDelete]
        [Route("DeleteSession/{SessionId}")]
        public async Task<IActionResult> DeleteSession(int SessionId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var session = db.BookingInstance.Include(zz => zz.BookingStatus).Include(zz=> zz.Ticket).Where(zz => zz.Id == SessionId).FirstOrDefault();
                DateTime oDate = DateTime.ParseExact(session.Date,"MM/dd/yyyy",CultureInfo.CurrentCulture);
                var start = DateTime.Now;
                if ((oDate - start).TotalDays <= 1.5 && (oDate - start).TotalDays >=  0)
                {
                    result.message = "Can't delete as there is less than 24 hours";
                    return BadRequest(result.message); 
                }

                if (session.BookingStatus.bookingStatus == "Booked" && oDate > start)
                {

                    var ticket = await db.Ticket.Where(zz => zz.Id == session.TicketId).FirstOrDefaultAsync();
                    var status = await db.TicketStatus.Where(zz => zz.ticketStatus == true).FirstOrDefaultAsync();
                    ticket.TicketStatusId = status.Id;
                    var booking = await db.Booking.Where(zz => zz.Id == session.BookingId).FirstOrDefaultAsync();
                    session.BookingId = null;
                    db.Booking.Remove(booking);
                    await db.SaveChangesAsync();
                }
                else if (session.BookingStatus.bookingStatus == "Ongoing")
                {
                    var reglist = await db.RegisteredStudent.Where(zz => zz.BookingInstanceId == session.Id).ToListAsync();
                    var ticketstatus = await db.TicketStatus.Where(zz => zz.ticketStatus == true).FirstOrDefaultAsync();
                    foreach(var item in reglist)
                    {
                        var enroline = await db.EnrolLine.Include(zz => zz.Enrollment).
                            Where(zz => zz.Enrollment.StudentId == item.StudentId && zz.ModuleId == session.ModuleId &&
                            zz.EndDate >= start && zz.StartDate <= start && zz.Subscription.SubscriptionTutorSession
                            .Any(zz => zz.TutorSession.SessionType.SessionTypeName == "Group")).FirstOrDefaultAsync();
                        var ticket = await db.Ticket.Include(zz => zz.TicketStatus).Where(zz => zz.TicketStatus.ticketStatus == false
                        && zz.EnrolLineId == enroline.Id).FirstOrDefaultAsync();
                        ticket.TicketStatusId = ticketstatus.Id;
                        db.RegisteredStudent.Remove(item);
                        await db.SaveChangesAsync();

                    }
                }


                var data = await BookingInstanceGenRepo.Delete(SessionId);

                result.data = data;
                result.message = "Session deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong deleting the Session";
                return BadRequest(result.message);
            }


        }
        #endregion

        #region groupsessioncontent
        [HttpGet]
        [Route("GetTutorSessions/{TutorId}")]
        public async Task<IActionResult> GetTutorSessions(int TutorId)
        {
            var entity = await TutorRepo.GetTutorSessions(TutorId);
            return Ok(entity);
        }

        [HttpGet]
        [Route("GetSessionContentCategory")]
        public async Task<IActionResult> GetSessionContentCategory()
        {
            var entity = await TutorRepo.GetSessionContentCategory();
            return Ok(entity);
        }

        [HttpPost]
        [Route("CreateSessionContent")]
        public async Task<IActionResult> CreateSessionContent([FromForm] GroupSessionContentDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = await db.GroupSessionContent.Where(zz => zz.BookingInstanceId == dto.BookingInstanceId).FirstOrDefaultAsync();
                if (check != null)
                {
                    result.message = "Content already exists";
                    return BadRequest(result.message);
                }
                GroupSessionContent content = new GroupSessionContent();
                content.BookingInstanceId = dto.BookingInstanceId;
                content.NotesName = dto.Notes.FileName;
                content.RecordingName = dto.Recording.FileName;
                content.SessionContentCategoryId = dto.SessionContentCategoryId;
                using (var target = new MemoryStream())
                {
                    dto.Notes.CopyTo(target);
                    content.Notes = target.ToArray();
                }
                using (var video = new MemoryStream())
                {
                    dto.Recording.CopyTo(video);
                    content.Recording = video.ToArray();
                }
                await db.GroupSessionContent.AddAsync(content);
                await db.SaveChangesAsync();
                var instance = await db.BookingInstance.Where(zz => zz.Id == dto.BookingInstanceId).FirstOrDefaultAsync();
                instance.ContentUploaded = true;
                await db.SaveChangesAsync();
                result.data = content;
                result.message = "Content added successfully";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went while adding the content";
                return BadRequest(result.message);
            }

        }

        [HttpGet]
        [Route("GetSessionContent/{BookingInstanceId}")]
        public async Task<IActionResult> GetSessionContent(int BookingInstanceId)
        {
            var entity = await TutorRepo.GroupSessionContent(BookingInstanceId);
            return Ok(entity);
        }

        [HttpPut]
        [Route("EditSessionContent")]
        public async Task<IActionResult> EditSessionContent([FromForm] GroupSessionContentDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               
                var content = await db.GroupSessionContent.Where(zz =>zz.Id == dto.Id).FirstOrDefaultAsync();
                content.SessionContentCategoryId = dto.SessionContentCategoryId;
                content.NotesName = dto.Notes.FileName;
                content.RecordingName = dto.Recording.FileName;
                using (var target = new MemoryStream())
                {
                    dto.Notes.CopyTo(target);
                    content.Notes = target.ToArray();
                }
                using (var video = new MemoryStream())
                {
                    dto.Recording.CopyTo(video);
                    content.Recording = video.ToArray();
                }
                await db.GroupSessionContent.AddAsync(content);
                await db.SaveChangesAsync();
                var instance = await db.BookingInstance.Where(zz => zz.Id == dto.BookingInstanceId).FirstOrDefaultAsync();
                instance.ContentUploaded = true;
                await db.SaveChangesAsync();
                result.data = content;
                result.message = "Content updated successfully";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went while updating the content";
                return BadRequest(result.message);
            }

        }

        [HttpDelete]
        [Route("DeleteSessionContent/{GroupSessionContentId}")]
        public async Task<IActionResult> DeleteSessionContent(int GroupSessionContentId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var data = await GroupSessionContentGenRepo.Delete(GroupSessionContentId);
                int sessionid = await db.GroupSessionContent.Where(zz => zz.Id == GroupSessionContentId).Select(zz => zz.BookingInstanceId).FirstOrDefaultAsync();
                var session = await db.BookingInstance.Where(zz => zz.Id == sessionid).FirstOrDefaultAsync();
                session.ContentUploaded = false;
                await db.SaveChangesAsync();
                result.data = data;
                result.message = "Content deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong deleting the Content";
                return BadRequest(result.message);
            }


        }

        [HttpGet]
        [Route("DownloadNotes/{GroupSessionContentId}")]
        public async Task<FileStreamResult> DownloadNotes(int GroupSessionContentId)
        {
            GroupSessionContent entity = await db.GroupSessionContent.Where(zz => zz.Id == GroupSessionContentId).FirstOrDefaultAsync();
            MemoryStream ms = new MemoryStream(entity.Notes);
            return new FileStreamResult(ms, "Application/pdf");
        }

        [HttpGet]
        [Route("WatchVideo/{GroupSessionContentId}")]
        public async Task<FileStreamResult> Video(int GroupSessionContentId)
        {
            var entity = await db.GroupSessionContent.Where(zz => zz.Id == GroupSessionContentId).FirstOrDefaultAsync();
            MemoryStream ms = new MemoryStream(entity.Recording);
            return new FileStreamResult(ms, "video/mp4");
        }
        #endregion

        #region MaintainTutor

        [HttpPut]
        [Route("updateTutor")]
        public async Task<IActionResult> UpdateStudentAsync([FromForm] UpdateTutorDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = await db.Users.Where(zz => zz.Email == dto.TutorEmail
                && zz.Id != dto.UserId).FirstOrDefaultAsync();
                if (check != null)
                {
                    result.message = "Sorry Email is taken";
                    return BadRequest(result.message);
                }
                var checkusername = await db.Users.Where(zz => zz.UserName == dto.UserName
               && zz.Id != dto.UserId).FirstOrDefaultAsync();
                if (checkusername != null)
                {
                    result.message = "Sorry username is taken";
                    return BadRequest(result.message);
                }
                
                var data = await TutorRepo.UpdateTutor(dto);
                result.data = data;
                result.message = "Tutor updated";
                return Ok(result);

            }
            catch
            {
                result.message = "Something went wrong updating the Tutor";
                return BadRequest(result.message);

            }

        }

        [HttpDelete]
        [Route("DeleteTutor/{TutorId}")]
        public async Task<IActionResult> DeleteTutor(int TutorId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var sessions = await db.BookingInstance.Where(zz => zz.TutorId == TutorId).ToListAsync();
                var messages = await db.Message.Where(zz => zz.TutorId == TutorId).ToListAsync();
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
                    return BadRequest("Unable to delete profile as you have upcoming sessions");
                }

                foreach (var item in messages)
                {

                    db.Message.Remove(item);
                }
                await db.SaveChangesAsync();
                var tutor = await db.Tutor.Where(zz => zz.Id == TutorId).FirstOrDefaultAsync();
                var file = await db.File.Where(zz => zz.Id == tutor.FileId).FirstOrDefaultAsync();

                var data = await TutorGenRepo.Delete(TutorId);
                db.File.Remove(file);
                await db.SaveChangesAsync();
                result.data = data;
                result.message = "Account deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong deleting Your Account";
                return BadRequest(result.message);
            }


        }
        #endregion

        #region Finalize session
        [HttpGet]
        [Route("FinalizeSession/{BookingInstanceId}")]
        public async Task<IActionResult> FinalizeSession(int BookingInstanceId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int status = await db.BookingStatus.Where(zz => zz.bookingStatus == "Finalized").Select(zz => zz.Id).FirstAsync();
                var instance = await db.BookingInstance.Where(zz => zz.Id == BookingInstanceId).FirstOrDefaultAsync();
                instance.BookingStatusId = status;
                await db.SaveChangesAsync();
                var reglist = await db.RegisteredStudent.Include(zz => zz.Student.Identity).Where(zz => zz.BookingInstanceId == instance.Id).ToListAsync();
                string subject = "Group Session Finalized:";
                string content = "Dear Student" + Environment.NewLine +
                  "Please note that the "+instance.Title+ " group session gas been finalized (attendance has been taken & session content has been uploaded.)"
                     + Environment.NewLine +
                     "As a valued student we always appreciate your feedback when it comes to our amazing tutors and the session, so please dont hesitate to provide feedback by clicking on the feedback tab on your home screen"
                     +Environment.NewLine +
                      Environment.NewLine +
                      "Regards TutorDevOps";
                foreach (var item in reglist)
                {
                    var message = new Emailservice.Message(new string[] { item.Student.Identity.Email }, subject, content);
                    await _emailsender.SendEmailAsync(message);
                }

                result.data = instance;
                result.message = "Session finalized";
                return Ok(result);

            }
            catch
            {
                result.message = "Something went wrong finalizing the Session";
                return BadRequest(result.message);

            }
        }
        #endregion

        #region Attendance
        [HttpGet]
        [Route("GetSessions/{TutorId}")]
        public async Task<IActionResult> GetSessions(int TutorId)
        {
            var entity = await db.BookingInstance.Where(zz => zz.TutorId == TutorId && zz.TutorSession.SessionType.SessionTypeName == "Group")
                .Include(zz =>zz.Module).ToListAsync();
            return Ok(entity);
        }

        [HttpGet]
        [Route("GetSessionAttendance/{BookingInstanceId}")]
        public async Task<IActionResult> GetSessionAttendance(int BookingInstanceId)
        {
            var entity = await db.RegisteredStudent.Where(zz => zz.BookingInstanceId == BookingInstanceId)
                .Include(zz => zz.Student).ThenInclude(zz => zz.Identity).ToListAsync();

            return Ok(entity);
        }

        [HttpPost]
        [Route("SubmitAttendance")]
        public async Task<IActionResult> SubmitAttendance([FromBody] List<RegisteredStudent> register)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            { 
                foreach(RegisteredStudent student in register)
                {
                    int id = student.Id;
                    RegisteredStudent existreg = db.RegisteredStudent.Find(id);
                    existreg.Attended = student.Attended;
                    var instance = await db.BookingInstance.Where(zz => zz.Id == student.BookingInstanceId).FirstOrDefaultAsync();
                    instance.AttendanceTaken = true;
                }
                await db.SaveChangesAsync();
               
                result.data = register;
                result.message = "Attendance Taken";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong taking attendance";
                return BadRequest(result.message);
            }

        }

        [HttpDelete]
        [Route("DeleteAttendance/{BookingInstanceId}")]
        public async Task<IActionResult> DeleteAttendance(int BookingInstanceId)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<RegisteredStudent> list = await db.RegisteredStudent.Where(zz => zz.BookingInstanceId == BookingInstanceId).ToListAsync();
                foreach(RegisteredStudent student in list)
                {
                  db.RegisteredStudent.Remove(student);
                }
                await db.SaveChangesAsync();
                result.data = list;
                result.message = "Attendance deleted";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong deleting the attendance";
                return BadRequest(result.message);
            }


        }

        #endregion


        //test
        [HttpPost]
        [Route("File")]
        public async Task<IActionResult> File([FromForm(Name = "file")] IFormFile file)
        {
            Models.Tutor.File x = new Models.Tutor.File();
            using (var Filetarget = new MemoryStream())
            {
                file.CopyTo(Filetarget);
                x.FileName = Filetarget.ToArray();
                
                
            }
            await db.File.AddAsync(x);
            await db.SaveChangesAsync();
            return Ok();
          

        }

        [HttpPost]
        [Route("pic")]
        public async Task<IActionResult> pic([FromForm(Name = "file")] IFormFile file)
        {
            var t = await db.Tutor.Where(zz => zz.Id == 4).FirstOrDefaultAsync();
            using (var Filetarget = new MemoryStream())
            {
                file.CopyTo(Filetarget);
                t.TutorPhoto = Filetarget.ToArray();


            }
           
            await db.SaveChangesAsync();
            return Ok();


        }

    }
}



