using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learn2CodeAPI.Data;
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
        private IGenRepository<Message> MessageGenRepo;
        private IGenRepository<Resource> ResourceGenRepo;
        private IGenRepository<ResourceCategory> ResourceCategoryGenRepo;
        private IGenRepository<BookingInstance> BookingInstanceGenRepo;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly AppDbContext db;

        public TutorController(
            IMapper _mapper,
            ITutor _TutorRepo,
            IWebHostEnvironment hostEnvironment,
            IGenRepository<Student> _StudentGenRepo,
            IGenRepository<Module> _ModuleRepo,
            IGenRepository<Message> _Message,
            IGenRepository<ResourceCategory> _ResourceCategoryGenRepo,
            AppDbContext _db,
            IGenRepository<BookingInstance> _BookingInstanceGenRepo,
            IGenRepository<Resource> _Resource
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
        [Route("GetAllModules")]
        public async Task<IActionResult> GetAllResourceModules()
        {
            var entity = await TutorRepo.GetModules();

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
        [Route("DownloadResource/{resourceid}")]
        public async Task<FileStreamResult> DownloadResource(int id)
        {
            var entity = await db.Resource.Where(zz => zz.Id == id).Select(zz => zz.ResoucesName).FirstOrDefaultAsync();
            MemoryStream ms = new MemoryStream(entity);
            return new FileStreamResult(ms, "application/pdf");
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

                //var check = db.BookingInstance.Where(zz => zz.SessionTimeId == dto.SessionTimeId &&
                //zz.Date == dto.Date && zz.TutorId == dto.TutorId).FirstOrDefault();
                //if (check != null)
                //{
                //    result.message = "USession Already exists";
                //    return BadRequest(result.message);
                //}

                var resource = await db.Resource.Where(zz => zz.Id == dto.Id).FirstOrDefaultAsync();
                resource.ModuleId = dto.ModuleId;
                resource.ResourceCategoryId = dto.ResourceCategoryId;
                resource.ResourceDescription = dto.ResourceDescription;
                using (var target = new MemoryStream())
                {
                    dto.ResoucesName.CopyTo(target);
                    resource.ResoucesName = target.ToArray();
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
        public async Task<IActionResult> TutorApplication(TutorDtoo model)
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

                string uniqueFileName = UploadedFile(model);
                Models.Tutor.File file = new Models.Tutor.File();
                file.FileName = "test";
                await db.File.AddAsync(file);
                await db.SaveChangesAsync();
                int idpending = await db.TutorStatus.Where(zz => zz.TutorStatusDesc == "Applied").Select(zz => zz.Id).FirstOrDefaultAsync();
                Tutor tutor = new Tutor
                {
                    TutorName = model.TutorName,
                    TutorSurname = model.TutorSurname,
                    TutorAbout = model.TutorAbout,
                    TutorCell = model.TutorCell,
                    TutorEmail = model.TutorEmail,
                    FileId = file.Id,
                    TutorStatusId = idpending,
                    TutorPhoto = uniqueFileName,
                };
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
        [Route("GetGroupSessions/{TutorId}")]
        public async Task<IActionResult> GetGroupSessions(int TutorId)
        {
            var entity = await db.BookingInstance.Include(zz => zz.SessionTime).Include(zz => zz.Module).Include(zz => zz.BookingStatus)
                .Where(zz => zz.TutorId == TutorId && zz.TutorSession.SessionType.SessionTypeName == "Group").FirstAsync();

            return Ok(entity);
        }

        [HttpGet]
        [Route("GetIndividualSessions/{TutorId}")]
        public async Task<IActionResult> GetIndividualSessions(int TutorId)
        {
            var entity = await db.BookingInstance.Include(zz => zz.SessionTime).Include(zz => zz.Module).Include(zz => zz.BookingStatus)
                 .Where(zz => zz.TutorId == TutorId && zz.TutorSession.SessionType.SessionTypeName == "Individual").FirstAsync();

            return Ok(entity);
        }

        [HttpGet]
        [Route("GetAllSessions/{TutorId}")]
        public async Task<IActionResult> GetAllSessions(int TutorId)
        {
            var entity = await db.BookingInstance.Include(zz => zz.SessionTime).Include(zz => zz.Module).Include(zz => zz.BookingStatus)
                 .Where(zz => zz.TutorId == TutorId).FirstAsync();

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

                var data = await TutorRepo.CreateBooking(dto);
                result.data = data;
                result.message = "Session created";
                return Ok(result);
            }
            catch
            {

                result.message = "Something went wrong creating the session";
                return BadRequest(result.message);
            }

        }

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
                DateTime oDate = Convert.ToDateTime(session.Date);
                var start = DateTime.Now;
                if ((oDate - start).TotalDays <= 1)
                {
                    result.message = "Can't update as there is less than 24 hours";
                    return BadRequest(result.message);
                }

                //var check = db.BookingInstance.Where(zz => zz.SessionTimeId == dto.SessionTimeId &&
                //zz.Date == dto.Date && zz.TutorId == dto.TutorId).FirstOrDefault();
                //if (check != null)
                //{
                //    result.message = "USession Already exists";
                //    return BadRequest(result.message);
                //}
                string timestring = dto.Date.ToString("MM/dd/yyyy");
                BookingInstance entity = mapper.Map<BookingInstance>(dto);
                entity.Date = timestring;
                var data = await BookingInstanceGenRepo.Update(entity);
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
                var session = db.BookingInstance.Where(zz => zz.Id == SessionId).FirstOrDefault();
                DateTime oDate = Convert.ToDateTime(session.Date);
                var start = DateTime.Now;
                if ((oDate - start).TotalDays <= 1)
                {
                    result.message = "Can't delete as there is less than 24 hours";
                    return BadRequest(result.message);
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

        

    }
}



