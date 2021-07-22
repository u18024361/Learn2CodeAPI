using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learn2CodeAPI.Data.Mapper;
using Learn2CodeAPI.Dtos.AdminDto;
using Learn2CodeAPI.IRepository.Generic;
using Learn2CodeAPI.Models.Admin;
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
        public AdminController(
             IMapper _mapper,
            IGenRepository<University> _universityGenRepo)
        {
            universityGenRepo = _universityGenRepo;
            mapper = _mapper; 
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
        [Route("GetAllUniversities")]
        public async Task<IActionResult> GetAllUniversities()
        {
            var Universities = await universityGenRepo.GetAll();
            return Ok(Universities);

        }

        [HttpPost]
        [Route("CreateUniversity")]
        public async Task<IActionResult> CreateApplication([FromBody] UniversityDto dto)
        {
            University entity = mapper.Map<University>(dto);
           

            dynamic result = await universityGenRepo.Add(entity);
          
            
                return Ok(result);
          
        }

        [HttpPut]
        [Route("EditUniversity")]
        public async Task<IActionResult> EditApplication([FromBody] UniversityDto dto)
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
            var result = await universityGenRepo.Delete(id);
            return Ok(result);
        }




        #endregion
    }
}