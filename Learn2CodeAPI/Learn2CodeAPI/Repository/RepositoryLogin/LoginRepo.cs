using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.LoginDto;
using Learn2CodeAPI.IRepository.IRepositoryLogin;
using Learn2CodeAPI.Models.Login.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Repository.RepositoryLogin
{
    public class LoginRepo : ILogin
    {
        private readonly AppDbContext db;
        private readonly UserManager<AppUser> _userManager;

        public LoginRepo(AppDbContext _db, UserManager<AppUser> userManager)
        {
            db = _db;
            _userManager = userManager;

        }
        #region changepassword
        public async Task<AppUser> ChangePassword(ChangePasswordDto dto)
        {
            var user = await db.Users.Where(zz => zz.Id == dto.Id).FirstOrDefaultAsync();
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password);

            await db.SaveChangesAsync();

            return user;
        }

        public async Task<AppUser> getpassword(string userid)
        {
            var user = await db.Users.Where(zz => zz.Id == userid).FirstOrDefaultAsync();
            return user;
        }
        #endregion

    }
}
