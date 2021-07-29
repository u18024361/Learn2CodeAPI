using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.IRepository.IRepositoryAdmin
{
    public interface IAdmin
    {
        Task<IEnumerable<Degree>> GetAllDegrees(int UniversityId);
        Task<IEnumerable<Module>> GetAllModules(int DegreeId);
        Task <University> GetByName(string Name);
        Task<Degree> GetByDegreeName(string Name);

        Task<Module> GetByModuleName(string Name);

        Task<CourseFolder> GetByCourseFolderName(string Name);

        Task<IEnumerable<Student>> GetAllStudents();

        Task<IEnumerable<Tutor>> GetAllApplications();





    }
}
