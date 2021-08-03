using AutoMapper;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.TutorDto;
using Learn2CodeAPI.IRepository.IRepositoryTutor;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Repository.RepositoryTutor
{
    public class TutorRepo : ITutor
    {
        private readonly AppDbContext db;
        private readonly UserManager<AppUser> _userManager;
        private IMapper mapper;

        public TutorRepo(AppDbContext _db, UserManager<AppUser> userManager, IMapper _mapper)
        {
            mapper = _mapper;
            db = _db;
            _userManager = userManager;

        }

        public async Task<ResourceCategory> GetByName(string Name)
        {
            var resourcecategory = await db.ResourceCategory.Where(zz => zz.ResourceCategoryName == Name).FirstOrDefaultAsync(); ;
            return resourcecategory;
        }



        #region Message
        public async Task<Message> CreateMessage(MessageDto model)
        {
            Message newmessage = new Message();
            newmessage.MessageSent = model.MessageSent;
            var date = DateTime.Now;
            string timestring = date.ToString("g");
            newmessage.TimeStamp = timestring;
            newmessage.StudentId = model.StudentId;
            newmessage.TutorId = model.TutorId;
            newmessage.SenderId = model.SenderId;
            newmessage.ReceiverId = model.ReceiverId;
            await db.Message.AddAsync(newmessage);
            await db.SaveChangesAsync();
            return newmessage;

        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var student = await db.Students.Include(zz => zz.Identity).ToListAsync();
            return student;
        }

        public async Task<IEnumerable<Message>> GetRecievedMessages(string UserId)
        {
            var message = await db.Message.Where(zz => zz.ReceiverId == UserId).Include(zz => zz.student).ThenInclude(zz => zz.Identity).ToListAsync();

            return message;
        }

        public async Task<IEnumerable<Message>> GetSentMessages(string UserId)
        {
            var message = await db.Message.Where(zz => zz.SenderId == UserId).Include(zz => zz.student).ThenInclude(zz => zz.Identity).ToListAsync();

            return message;
        }

        #endregion
    }
}
