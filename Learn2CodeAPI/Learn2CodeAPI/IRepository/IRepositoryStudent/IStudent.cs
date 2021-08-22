using Learn2CodeAPI.Dtos.StudentDto;
using Learn2CodeAPI.Dtos.TutorDto;
using Learn2CodeAPI.Models.Admin;
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

        Task<IEnumerable<Tutor>> GetTutors();
        Task<IEnumerable<BookingInstance>> GetMyBookings(int StudentId);
        Task<IEnumerable<Resource>> GetResource(int ModuleId);
        Task<IEnumerable<CourseSubCategory>> GetCourseSubCategory(int CourseFolderId);
        Task<IEnumerable<Subscription>> GetSubscriptions();
        Task<Basket> GetBasket(int StudentId);
        Task<CourseBasketLine> BuyCourse(CourseBuyDto dto);
        Task<SubScriptionBasketLine> BuySubscription(SubscriptionBuyDto dto);
       Task<Basket>Checkout(CheckoutDto dto);
        Task<BookingInstance> CancelBooking(int BookingInstanceId);

        Task<Feedback> CreateFeedback(Feedback feedback);
        Task<Feedback> DeleteFeedback(int StudentId, int BookingInstanceId);
        Task<IEnumerable<Feedback>> MyFeedback(int StudentId);
        Task<IEnumerable<RegisteredStudent>> GetmyReg(int StudentId);

        Task<IEnumerable<Degree>> GetDegree(int UniId);
        Task<IEnumerable<Module>> GetModule(int DegreeId);
        Task<IEnumerable<RegisteredStudent>> Getmygroupsession(int StudentId);
    }
}
