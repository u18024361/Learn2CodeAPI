using Learn2CodeAPI.Dtos.TutorDto;
using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.IRepository.IRepositoryTutor
{
    public interface ITutor
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<IEnumerable<Message>> GetSentMessages(string UserId);
        Task<Message> CreateMessage(MessageDto model);
        Task<IEnumerable<Message>> GetRecievedMessages(string UserId);
        Task<ResourceCategory> GetByName(string name);

        Task<IEnumerable<TutorModule>> GetTutorModule(int TutorId);
        Task<IEnumerable<SessionTime>> GetSessionTime();
        Task<BookingInstance> CreateBooking(BookingInstanceDto model);

        Task<IEnumerable<Module>> GetModules();
        Task<IEnumerable<Resource>> GetModuleResources(int ModuleId);

        Task<IEnumerable<BookingInstance>> GetTutorSessions(int TutorId);
        Task<IEnumerable<SessionContentCategory>> GetSessionContentCategory();
        Task<IEnumerable<GroupSessionContent>> GroupSessionContent(int BookingInstanceId);

        Task<Tutor> UpdateTutor(UpdateTutorDto dto);


    }
}
