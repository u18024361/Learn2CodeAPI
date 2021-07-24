using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.StudentDto;
using Learn2CodeAPI.IRepository.IRepositoryStudent;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Repository.RepositoryStudent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public StudentController(IStudent _studentRepo, UserManager<AppUser> userManager, IMapper mapper, AppDbContext appDbContext,  AppDbContext _db)
        {
            studentRepo = _studentRepo;
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
            db = _db;
        }


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



    }
}