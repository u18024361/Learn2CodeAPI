using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.TutorDto;
using Learn2CodeAPI.IRepository.Generic;
using Learn2CodeAPI.IRepository.IRepositoryTutor;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
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
        private IGenRepository<Message> MessageGenRepo;
        private IGenRepository<ResourceCategory> ResourceCategoryGenRepo;

        private readonly AppDbContext db;

        public TutorController(
            IMapper _mapper,
            ITutor _TutorRepo, 
            IGenRepository<Student> _StudentGenRepo, 
            IGenRepository<Message> _Message,
            IGenRepository<ResourceCategory> _ResourceCategoryGenRepo,
            AppDbContext _db)

        {
            db = _db;
            mapper = _mapper;
            TutorRepo = _TutorRepo;
            StudentGenRepo = _StudentGenRepo;
            MessageGenRepo = _Message;
            ResourceCategoryGenRepo = _ResourceCategoryGenRepo;
        }

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
            var ResourceCat= await ResourceCategoryGenRepo.GetAll();
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

        #region Application
        #endregion
    }
}