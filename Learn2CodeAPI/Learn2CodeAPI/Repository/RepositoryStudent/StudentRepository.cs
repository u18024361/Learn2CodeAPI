using AutoMapper;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Dtos.StudentDto;
using Learn2CodeAPI.Dtos.TutorDto;
using Learn2CodeAPI.IRepository.IRepositoryStudent;
using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Repository.RepositoryStudent
{
    public class StudentRepository : IStudent
    {
        private readonly AppDbContext db;
        private readonly UserManager<AppUser> _userManager;
        
        public StudentRepository(AppDbContext _db, UserManager<AppUser> userManager)
        {
            db = _db;
            _userManager = userManager;
            
        }

        #region Student
        public async Task<Student> Register(AppUser userIdentity, RegistrationDto model)
        {
            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
            {
                return null;
            }

            try
            {
                Student student = new Student();
                student.UserId = userIdentity.Id;
                student.StudentName = model.StudentName;
                student.StudentSurname = model.StudentSurname;
                student.StudentCell = model.StudentCell;
                await db.Students.AddAsync(student);
                await db.SaveChangesAsync();

                //await _appDbContext.Students.AddAsync(new Student { UserId = userIdentity.Id, StudentName = model.StudentName,
                //     StudentSurname = model.StudentSurname, StudentCell = model.StudentCell });
                await db.StudentModule.AddAsync(new StudentModule
                {
                    StudentId = student.Id,
                    ModuleId = model.ModuleId
                });
                await db.Basket.AddAsync(new Basket
                {
                    StudentId = student.Id,

                });
                //await _userManager.AddToRoleAsync(userIdentity, "Student");
                await db.SaveChangesAsync();


                return student;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Student> UpdateProfile(UpdateStudent dto)
        {
            //student table
            var student = db.Students.Where(zz => zz.Id == dto.StudentId).FirstOrDefault();
            student.StudentCell = dto.StudentCell;
            student.StudentName = dto.StudentName;
            student.StudentSurname = dto.StudentSurname;
            student.UserId = student.UserId;

            //StudentModule
            var studentModule = db.StudentModule.Where(zz => zz.StudentId == dto.StudentId).FirstOrDefault();
            if (dto.ModuleId != 0)
            {
                studentModule.ModuleId = dto.ModuleId;
                studentModule.StudentId = dto.StudentId;
            }


            //user table
            var user = db.Users.Where(zz => zz.Id == student.UserId).FirstOrDefault();
            user.Email = dto.Email;
            user.NormalizedEmail = dto.Email.ToUpper();
            user.NormalizedUserName = dto.UserName.ToUpper();
            user.UserName = dto.UserName;
            await db.SaveChangesAsync();
            return student;
        }

        #endregion

        #region messages
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

        public async Task<IEnumerable<Message>> GetRecievedMessages(string UserId)
        {
            var message = await db.Message.Where(zz => zz.ReceiverId == UserId).Include(zz => zz.tutor).ThenInclude(zz => zz.Identity).ToListAsync();

            return message;
        }

        public async Task<IEnumerable<Message>> GetSentMessages(string UserId)
        {
            var message = await db.Message.Where(zz => zz.SenderId == UserId).Include(zz => zz.tutor).ThenInclude(zz => zz.Identity).ToListAsync();

            return message;
        }

        public async Task<IEnumerable<Tutor>> GetTutors()
        {
            var tutors = await db.Tutor.Include(zz => zz.Identity).Where(zz => zz.TutorStatus.TutorStatusDesc == "Accepted").ToListAsync();
            return tutors;
        }





        #endregion

        #region viewresources
        public async Task<IEnumerable<Resource>> GetResource(int ModuleId)
        {
            var resource = await db.Resource.Include(zz => zz.ResourceCategory).Where(zz => zz.ModuleId == ModuleId).ToListAsync();
            return resource;
        }


        #endregion

        #region ViewShop
        public async Task<IEnumerable<CourseSubCategory>> GetCourseSubCategory(int CourseFolderId)
        {
            var coursesubcategory = await db.courseSubCategory.Where(zz => zz.CourseFolderId == CourseFolderId).ToListAsync();
            return coursesubcategory;
        }

        public async Task<Basket> GetBasket(int StudentId)
        {
            var basket = await db.Basket.Where(zz => zz.StudentId == StudentId).FirstOrDefaultAsync();
            int Quantity = await db.CourseBasketLine.Where(zz => zz.BasketId == basket.Id).CountAsync();
            double TotalPrice = await db.CourseBasketLine.Where(zz => zz.BasketId == basket.Id).Include(zz => zz.CourseSubCategory).Select(zz => zz.CourseSubCategory.price).SumAsync();
            basket.Quantity = Quantity;
            basket.TotalPrice = TotalPrice;
            await db.SaveChangesAsync();
            return basket;
        }

       
        #endregion

        #region AddtoBasket

        //for courses
        public async Task<CourseBasketLine> BuyCourse(CourseBuyDto dto)
        {
            CourseBasketLine courseBasket = new CourseBasketLine();

            courseBasket.BasketId = dto.BasketId;
            courseBasket.CourseSubCategoryId = dto.CourseSubCategoryId;

            await db.CourseBasketLine.AddAsync(courseBasket);
            await db.SaveChangesAsync();
            return courseBasket;
        }


        #endregion

        #region Checkout
        public async Task<Basket> Checkout(CheckoutDto dto)
        {
            var coursebasketline = await db.CourseBasketLine.Where(zz => zz.BasketId == dto.BasketId).ToListAsync();
            string timestring = DateTime.Now.ToString("MM/dd/yyyy");
            CourseEnrol x = new CourseEnrol();
            x.StudentId = dto.StudentId;
            x.Date = timestring;
            await db.CourseEnrol.AddAsync(x);
            await db.SaveChangesAsync();
            foreach (CourseBasketLine course in coursebasketline)
            {
                
                CourseEnrolLine y = new CourseEnrolLine();
                y.CourseSubCategoryId = course.CourseSubCategoryId;
                y.CourseEnrolId = x.Id;
                await db.CourseEnrolLine.AddAsync(y);
                db.CourseBasketLine.Remove(course);
            }
            var basket = await db.Basket.Where(zz => zz.Id == dto.BasketId).FirstOrDefaultAsync();
            basket.Quantity = 0;
            basket.TotalPrice = 0;
            await db.SaveChangesAsync();
            return basket;

        }
        #endregion
    }
}
