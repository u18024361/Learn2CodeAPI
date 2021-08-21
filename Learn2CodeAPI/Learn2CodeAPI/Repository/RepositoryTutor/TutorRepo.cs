using AutoMapper;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.TutorDto;
using Learn2CodeAPI.IRepository.IRepositoryTutor;
using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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

        #region recourcecategory
        public async Task<ResourceCategory> GetByName(string Name)
        {
            var resourcecategory = await db.ResourceCategory.Where(zz => zz.ResourceCategoryName == Name).FirstOrDefaultAsync(); ;
            return resourcecategory;
        }
        #endregion

        #region Resource
        public async Task<IEnumerable<Module>> GetModules()
        {
            var modules = await db.Modules.Include(zz => zz.Degree).Include(zz => zz.Degree.University).ToListAsync();
            return modules;
        }

        public async Task<IEnumerable<Resource>> GetModuleResources(int ModuleId)
        {
            var resources = await db.Resource.Include(zz => zz.ResourceCategory).Include(zz => zz.Module)
                .Where(zz => zz.ModuleId == ModuleId).ToListAsync();
            return resources;
        }
        #endregion

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

        #region bookinginstance
        public async Task<IEnumerable<TutorModule>> GetTutorModule(int TutorId)
        {
            var module = await db.TutorModule.Include(zz => zz.Module).Where(zz => zz.TutorId == TutorId).ToListAsync();
            return module;
        }

        public async Task<IEnumerable<SessionTime>> GetSessionTime()
        {
            var module = await db.SessionTime.ToListAsync();
            return module;
        }

        public async Task<BookingInstance> CreateBooking(BookingInstanceDto model)
        {
            string timestring = model.Date.ToString("MM/dd/yyyy");
            int idtutgroup = await db.TutorSession.Where(zz => zz.SessionType.SessionTypeName == "Group").Select(zz => zz.Id).FirstOrDefaultAsync();
            int idgroup = await db.BookingStatus.Where(zz => zz.bookingStatus == "Ongoing").Select(zz => zz.Id).FirstOrDefaultAsync();
            int idindividual = await db.BookingStatus.Where(zz => zz.bookingStatus == "Open").Select(zz => zz.Id).FirstOrDefaultAsync();
            BookingInstance entity = mapper.Map<BookingInstance>(model);
            entity.AttendanceTaken = false;
            entity.ContentUploaded = false;
            entity.Date = timestring;
            if(model.TutorSessionId == idtutgroup)
            {
                entity.BookingStatusId = idgroup;
            }
            else { entity.BookingStatusId = idindividual; }
            await db.BookingInstance.AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }


        #endregion

        #region sessioncontent
        public async Task<IEnumerable<BookingInstance>> GetTutorSessions(int TutorId)
        {
            var sessions = await db.BookingInstance.Include(zz =>zz.Module).Where(zz => zz.TutorId == TutorId &&
            zz.TutorSession.SessionType.SessionTypeName == "Group").ToListAsync();
            return sessions;
        }

        public async Task<IEnumerable<SessionContentCategory>> GetSessionContentCategory()
        {
            var categories = await db.SessionContentCategory.ToListAsync();
            return categories;
        }

        public async Task<IEnumerable<GroupSessionContent>> GroupSessionContent(int BookingInstanceId)
        {
            var content = await db.GroupSessionContent.Where(zz => zz.BookingInstanceId == BookingInstanceId).ToListAsync();
            return content;
        }


        #endregion

        #region MaintainTutor
        public async Task<Tutor> UpdateTutor(UpdateTutorDto dto)
        {
            //tutor table
            var tutor = await db.Tutor.Where(zz => zz.Id == dto.Id).FirstOrDefaultAsync();
            tutor.TutorName = dto.TutorName;
            tutor.TutorSurname= dto.TutorSurname;
            tutor.TutorEmail = dto.TutorEmail;
            tutor.TutorAbout = dto.TutorAbout;
            tutor.TutorCell = dto.TutorCell;
            using (var target = new MemoryStream())
            {
                dto.TutorPhoto.CopyTo(target);
                tutor.TutorPhoto = target.ToArray();
            }


            //File
            var File = await db.File.Where(zz => zz.Id == dto.FileId).FirstOrDefaultAsync();
            using (var file = new MemoryStream())
            {
                dto.File.CopyTo(file);
                File.FileName = file.ToArray();
            }

            //user table
            var user = await db.Users.Where(zz => zz.Id == dto.UserId).FirstOrDefaultAsync();
            user.Email = dto.TutorEmail;
            user.NormalizedEmail = dto.TutorEmail.ToUpper();
            user.NormalizedUserName = dto.UserName.ToUpper();
            user.UserName = dto.UserName;
            await db.SaveChangesAsync();
            return tutor;
        }
        #endregion

       

    }
}
