using BusinessEntities;
using BusinessServices;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("EmpExperience")]
    public class EmpExperienceController : ApiController
    {
        private readonly IEmployeeExperienceService _empExpServices;

        public EmpExperienceController(IEmployeeExperienceService empExpServices)
        {
            _empExpServices = empExpServices;
        }

        [HttpGet]
        [Route("getAllEmpExpInfo/{empId}")]
        public HttpResponseMessage Get(int empId)
        {
            try
            {
                var empExpInfo = _empExpServices.GetEmpExpByEmpId(empId);
                return Request.CreateResponse(HttpStatusCode.OK, empExpInfo);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("createEmpExpInfo")]
        public HttpResponseMessage Post(EmployeeExperienceEntity empExperience)
        {
            try
            {
                var result = _empExpServices.CreateEmployeeExperience(empExperience);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Employee Not Found", HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("removeEmpExpInfo/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = _empExpServices.DeleteEmployeeExperience(id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }
    }
}
