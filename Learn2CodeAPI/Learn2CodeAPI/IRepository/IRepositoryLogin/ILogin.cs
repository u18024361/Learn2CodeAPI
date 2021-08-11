using Learn2CodeAPI.Dtos.LoginDto;
using Learn2CodeAPI.Models.Login.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.IRepository.IRepositoryLogin
{
   public interface ILogin
    {
        Task<AppUser> ChangePassword(ChangePasswordDto dto);
        Task<AppUser> getpassword(string userid);
    }
}

