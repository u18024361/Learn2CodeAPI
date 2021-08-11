using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.LoginDto;
using Learn2CodeAPI.Dtos.LoginDto.IdentityDto;
using Learn2CodeAPI.IRepository.IRepositoryLogin;
using Learn2CodeAPI.IRepository.IRepositoryStudent;
using Learn2CodeAPI.JwtFeatures;
using Learn2CodeAPI.Models.Login.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private readonly JwtHandler _jwtHandler;

        public LoginController(IStudent _studentRepo, UserManager<AppUser> userManager, IMapper mapper,
            AppDbContext appDbContext, AppDbContext _db, ILogin _loginRepo, JwtHandler jwtHandler)
        {
            studentRepo = _studentRepo;
            loginrepo = _loginRepo;
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
            db = _db;
            _jwtHandler = jwtHandler; 
        }


        #region changepassword
        //to check if password is correct not sure will work cause of hashing
        [HttpGet]
        [Route("GetPassword/{Userid}")]
        public async Task<IActionResult> GetPassword(string userid)
        {
            var password = await loginrepo.getpassword(userid);
            return Ok(password.PasswordHash);

        }

        //must send userid
        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var data = await loginrepo.ChangePassword(dto);
                result.data = data;
                result.message = "Password changed";
                return Ok(result);

            }
            catch
            {
                result.message = "Something went wrong while changing the password";
                return BadRequest(result.message);

            }

        }

        #endregion



        #region Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto userForAuthentication)
        {
            dynamic result = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(userForAuthentication.Email);
                var typeid = await db.UserRoles.Where(zz => zz.UserId == user.Id).FirstOrDefaultAsync();
                var type = await db.Roles.Where(zz => zz.Id == typeid.RoleId).FirstOrDefaultAsync();
                if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                {
                    result.message = "Invalid login details";
                    return Ok(result);
                }
                var signingCredentials = _jwtHandler.GetSigningCredentials();
                var claims = await _jwtHandler.GetClaims(user);
                var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new AuthResponseDto{ IsAuthSuccessful = true, Token = token, Id = user.Id, ErrorMessage ="Successful login",Type = type.Name});
                
          
            }
            catch
            {

                result.message = "Something went wrong while loging in";
                return BadRequest(result.message);
            }







           
            
            
        }
        #endregion


    }
}