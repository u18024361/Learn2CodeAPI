using Learn2CodeAPI.Dtos.StudentDto;
using Learn2CodeAPI.Dtos.TutorDto;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.IRepository.IRepositoryStudent
{
    public interface IStudent
    {
        Task<Student> Register(AppUser userIdentity, RegistrationDto model);

        Task<Student> UpdateProfile(UpdateStudent dto);
        Task<IEnumerable<Message>> GetSentMessages(string UserId);
        Task<IEnumerable<Message>> GetRecievedMessages(string UserId);
        Task<Message> CreateMessage(MessageDto model);
    }
}
