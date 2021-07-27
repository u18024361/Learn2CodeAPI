using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learn2CodeAPI.Data.Mapper;
using Learn2CodeAPI.Dtos.AdminDto;
using Learn2CodeAPI.IRepository.Generic;
using Learn2CodeAPI.IRepository.IRepositoryAdmin;
using Learn2CodeAPI.IRepository.IRepositoryStudent;
using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learn2CodeAPI.Controllers
{
    [Route("api/Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        
        private  IMapper mapper;
        private IGenRepository<University> universityGenRepo;
        private IGenRepository<Degree> DegreeGenRepo;
        private IGenRepository<Module> ModuleGenRepo;
        private IGenRepository<Student> StudentGenRepo;
        private IGenRepository<CourseFolder> CourseFolderGenRepo;
        private IGenRepository<SessionContentCategory> SessionContentCategoryRepo;
        private IAdmin AdminRepo;
        public AdminController(
            IMapper _mapper,
            IGenRepository<University> _universityGenRepo,
            IGenRepository<Degree> _DegreeGenRepo,
            IGenRepository<Module> _ModuleGenRepo,
            IGenRepository<CourseFolder> _CourseFolderGenRepo,
             IGenRepository<Student> _StudentGenRepo,
              IGenRepository<SessionContentCategory> _SessionContentCategoryRepo,
            IAdmin _AdminRepo

            )
        

        
        {
            universityGenRepo = _universityGenRepo;
            mapper = _mapper;
            DegreeGenRepo = _DegreeGenRepo;
            ModuleGenRepo = _ModuleGenRepo;
            AdminRepo = _AdminRepo;
            CourseFolderGenRepo = _CourseFolderGenRepo;
            StudentGenRepo = _StudentGenRepo;
            SessionContentCategoryRepo = _SessionContentCategoryRepo;
        }

        #region University

        [HttpGet]
        [Route("GetUniversitybyId/{UniversityId}")]
        public async Task<IActionResult>GetUniversitybyId(int UniversityId)
        {
            var entity  = await universityGenRepo.Get(UniversityId);

            return Ok(entity);
        }

        [HttpGet]
        [Route("SearchUniversity/{UniversityName}")]
        public async Task<IActionResult> SearchUniversity(string UniversityName)
        {
            var entity = await AdminRepo.GetByName(UniversityName);

            return Ok(entity);
        }

        [HttpGet]
        [Route("GetAllUniversities")]
        public async Task<IActionResult> GetAllUniversities()
        {
            var Universities = await universityGenRepo.GetAll();
            return Ok(Universities);

        }

        [HttpPost]
        [Route("CreateUniversity")]
        public async Task<IActionResult> CreateUniversity([FromBody] UniversityDto dto)
        {
            University entity = mapper.Map<University>(dto);
           

            dynamic result = await universityGenRepo.Add(entity);
          
            
                return Ok(result);
          
        }

        [HttpPut]
        [Route("EditUniversity")]
        public async Task<IActionResult> EditUniversity([FromBody] UniversityDto dto)
        {
            University entity = mapper.Map<University>(dto);

            dynamic result = await universityGenRepo.Update(entity);


            return Ok(result);
        }

        //[HttpPost]
        //[Route("DeleteApplication")]
        //public async Task<IActionResult> DeleteApplication([FromBody] ApplicationDto dto)
        //{
        //    if (dto.Id == 0)
        //    {
        //        return BadRequest();
        //    }

        //    dynamic result = await ppmsService.DeleteApplicationAsync(dto);
        //    if (result.ok)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest(result.message);
        //    }
        //}

        [HttpDelete]
        [Route("DeleteUniversity/{UniversityId}")]
        public async Task<IActionResult> DeleteUniversity(int UniversityId)
        {
            var result = await universityGenRepo.Delete(UniversityId);
            return Ok(result);
        }

        #endregion

        #region Degree

        [HttpGet]
        [Route("GetDegreebyId/{DegreeId}")]
        public async Task<IActionResult> GetDegreebyId(int DegreeId)
        {
            var entity = await DegreeGenRepo.Get(DegreeId);
            //var entity = db.Degrees.Where(zz => zz.Id == DegreeId).Include(zz => zz.University).FirstOrDefault();

            return Ok(entity);
        }

        [HttpGet]
        [Route("SearchDegree/{DegreeName}")]
        public async Task<IActionResult> SearchDegree(string DegreeName)
        {
            var entity = await AdminRepo.GetByDegreeName(DegreeName);

            return Ok(entity);
        }


        [HttpGet]
        [Route("GetAllDegrees/{UniversityId}")]
        public async Task<IActionResult> GetAllDegrees(int UniversityId)
        {
            var degrees = await AdminRepo.GetAllDegrees(UniversityId);
            return Ok(degrees);

        }

        [HttpPost]
        [Route("CreateDegree")]
        public async Task<IActionResult> CreateDegree([FromBody] DegreeDto dto)
        {
            Degree entity = mapper.Map<Degree>(dto);


            dynamic result = await DegreeGenRepo.Add(entity);


            return Ok(result);

        }

        [HttpPut]
        [Route("EditDegree")]
        public async Task<IActionResult> EditDegree([FromBody] DegreeDto dto)
        {
            Degree entity = mapper.Map<Degree>(dto);

            dynamic result = await DegreeGenRepo.Update(entity);


            return Ok(result);
        }

        //[HttpPost]
        //[Route("DeleteApplication")]
        //public async Task<IActionResult> DeleteApplication([FromBody] ApplicationDto dto)
        //{
        //    if (dto.Id == 0)
        //    {
        //        return BadRequest();
        //    }

        //    dynamic result = await ppmsService.DeleteApplicationAsync(dto);
        //    if (result.ok)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest(result.message);
        //    }
        //}

        [HttpDelete]
        [Route("DeleteDegree/{DegreeId}")]
        public async Task<IActionResult> DeleteDegree(int DegreeId)
        {
            var result = await DegreeGenRepo.Delete(DegreeId);
            return Ok(result);
        }

        #endregion

        #region Module

        [HttpGet]
        [Route("SearchModule/{ModuleName}")]
        public async Task<IActionResult> SearchModule(string ModuleName)
        {
            var entity = await AdminRepo.GetByModuleName(ModuleName);

            return Ok(entity);
        }

        [HttpGet]
        [Route("GetModulebyId/{ModuleId}")]
        public async Task<IActionResult> GetModulebyId(int ModuleId)
        {
            var entity = await ModuleGenRepo.Get(ModuleId);
            //var entity = db.Degrees.Where(zz => zz.Id == DegreeId).Include(zz => zz.University).FirstOrDefault();

            return Ok(entity);
        }



        [HttpGet]
        [Route("GetAllModules/{DegreeId}")]
        public async Task<IActionResult> GetAllModules(int DegreeId)
        {
            var modules = await AdminRepo.GetAllModules(DegreeId);
            return Ok(modules);

        }

        [HttpPost]
        [Route("CreateModule")]
        public async Task<IActionResult> CreateModule([FromBody] Module dto)
        {
            Module entity = mapper.Map<Module>(dto);


            dynamic result = await ModuleGenRepo.Add(entity);


            return Ok(result);

        }

        [HttpPut]
        [Route("EditModule")]
        public async Task<IActionResult> EditModule([FromBody] Module dto)
        {
            Module entity = mapper.Map<Module>(dto);

            dynamic result = await ModuleGenRepo.Update(entity);


            return Ok(result);
        }

        //[HttpPost]
        //[Route("DeleteApplication")]
        //public async Task<IActionResult> DeleteApplication([FromBody] ApplicationDto dto)
        //{
        //    if (dto.Id == 0)
        //    {
        //        return BadRequest();
        //    }

        //    dynamic result = await ppmsService.DeleteApplicationAsync(dto);
        //    if (result.ok)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest(result.message);
        //    }
        //}

        [HttpDelete]
        [Route("DeleteModule/{ModuleId}")]
        public async Task<IActionResult> DeleteModule(int ModuleId)
        {
            var result = await ModuleGenRepo.Delete(ModuleId);
            return Ok(result);
        }

        #endregion

        #region CourseFolder

        [HttpPost]
        [Route("CreateCourseFolder")]
        public async Task<IActionResult> CreateCourseFolder([FromBody] CourseFolderDto dto)
        {
            CourseFolder entity = mapper.Map<CourseFolder>(dto);
             

            dynamic result = await CourseFolderGenRepo.Add(entity);

            return Ok(result);

        }

        [HttpGet]
        [Route("GetAllCourseFolder")]
        public async Task<IActionResult> GetAllCourseFolder()
        {
            var entity = await CourseFolderGenRepo.GetAll();
            return Ok(entity);

        }

        [HttpGet]
        [Route("SearchCourseFolder/{CourseFolderName}")]
        public async Task<IActionResult> SearchCourseFolder(string CourseFolderName)
        {
            var entity = await AdminRepo.GetByCourseFolderName(CourseFolderName);

            return Ok(entity);
        }

        [HttpPut]
        [Route("EditCourseFolder")]
        public async Task<IActionResult> EditCourseFolder([FromBody] CourseFolderDto dto)
        {
            CourseFolder entity = mapper.Map<CourseFolder>(dto);

            dynamic result = await CourseFolderGenRepo.Update(entity);


            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteCourseFolder/{CourseFolderId}")]
        public async Task<IActionResult> DeleteCourseFolder(int CourseFolderId)
        {
            var result = await CourseFolderGenRepo.Delete(CourseFolderId);
            return Ok(result);
        }


        #endregion

        #region Student
        [HttpGet]
        [Route("GeAllStudents")]
        public async Task<IActionResult> GeAllStudents()
        {
            var entity = await AdminRepo.GetAllStudents();

            return Ok(entity);
        }


        [HttpDelete]
        [Route("DeleteStudent/{StudentId}")]
        public async Task<IActionResult> DeleteStudent(int StudentId)
        {
            var result = await StudentGenRepo.Delete(StudentId);
            return Ok(result);
        }



        #endregion

        #region SessionContentCategory
        [HttpGet]
        [Route("GetAllSessionContentCategory")]
        public async Task<IActionResult> GetAllSessionContentCategory()
        {
            var Universities = await SessionContentCategoryRepo.GetAll();
            return Ok(Universities);

        }

        [HttpPost]
        [Route("CreateSessionContentCategory")]
        public async Task<IActionResult> CreateSessionContentCategory([FromBody] SessionContentCategoryDto dto)
        {
            SessionContentCategory entity = mapper.Map<SessionContentCategory>(dto);


            dynamic result = await SessionContentCategoryRepo.Add(entity);


            return Ok(result);

        }

        [HttpPut]
        [Route("EditSessionContentCategory")]
        public async Task<IActionResult> EditSessionContentCategory([FromBody] SessionContentCategoryDto dto)
        {
            SessionContentCategory entity = mapper.Map<SessionContentCategory>(dto);

            dynamic result = await SessionContentCategoryRepo.Update(entity);


            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteSessionContentCategory/{SessionContentCategoryId}")]
        public async Task<IActionResult> DeleteSessionContentCategory(int SessionContentCategoryId)
        {
            var result = await SessionContentCategoryRepo.Delete(SessionContentCategoryId);
            return Ok(result);
        }
        #endregion

    }
}