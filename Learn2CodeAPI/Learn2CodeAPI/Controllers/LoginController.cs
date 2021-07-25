using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.LoginDto;
using Learn2CodeAPI.IRepository.IRepositoryLogin;
using Learn2CodeAPI.IRepository.IRepositoryStudent;
using Learn2CodeAPI.Models.Login.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Learn2CodeAPI.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase

    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private IMapper _mapper;
        private readonly AppDbContext db;
        private IStudent studentRepo;
        private ILogin loginrepo;

        public LoginController(IStudent _studentRepo, UserManager<AppUser> userManager, IMapper mapper, AppDbContext appDbContext, AppDbContext _db, ILogin _loginRepo )
        {
            studentRepo = _studentRepo;
            loginrepo = _loginRepo;
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
            db = _db;
        }

        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {

            var result = await loginrepo.ChangePassword(dto);
            if (result == null)
            {
                return BadRequest("Unable to update password");
            }
            else
            {
                return Ok("Password updated Succesffully");
            }


        }


    }
}