using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using WebApi.ErrorHelper;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessServices;
using WebApi.ActionFilters;

namespace WebApi.Controllers
{
    // [AuthorizationRequired]
    [RoutePrefix("Department")]
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentServices _departmentServices;

        public DepartmentController(BusinessServices.IDepartmentServices department)
        {
            _departmentServices = department;
        }

        [HttpGet]
        [Route("AllDepartments/{CompanyId}")]
        public HttpResponseMessage Get(int CompanyId)
        {
            try
            {
                var Department = _departmentServices.GetAllDepartments(CompanyId);
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("AllCompany_Department")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Department = _departmentServices.GetAllCompanyDepartment();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetDepartmentId/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            if (id != 0)
            {
                var Department = _departmentServices.GetDepartmentById(id);
                if (Department != null)
                    return Request.CreateResponse(HttpStatusCode.OK, Department);
                throw new ApiDataException(1001, "No Department found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }

        [HttpGet]
        [Route("GetDepartmentByCompanyId/{CompanyId}")]
        public HttpResponseMessage GetDepartmentByCompanyId(int CompanyId)
        {
            if (CompanyId != 0)
            {
                var dept = _departmentServices.GetDepartmentByCompanyId(CompanyId);
                if (dept != null)
                    return Request.CreateResponse(HttpStatusCode.OK, dept);
                throw new ApiDataException(1001, "No Department found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }


        [HttpPost]
        [Route("Create")]
        public ResultDTO Post([FromBody]DepartmentEntity departmentEntity)
        {
            try
            {
                return _departmentServices.CreateDepartment(departmentEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        [Route("Modify")]
        public HttpResponseMessage Put([FromBody]DepartmentEntity departmentEntity)
        {
            try
            {
                if (departmentEntity.DepartmentId > 0)
                {
                    var result = _departmentServices.UpdateDepartment(departmentEntity.DepartmentId, departmentEntity);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department not found", HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (id > 0)
                {
                    var isSuccess = _departmentServices.DeleteDepartment(id);
                    if (isSuccess)
                    {
                        msg = Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                    }
                }
                return msg;
            }
            catch (Exception ex)
            {
                return msg;
            }
        }

        [HttpPost]
        [Route("ChangeActive/{id}")]
        public bool DeActivate(int id)
        {
            try
            {
                if (id > 0)
                {
                    var isSuccess = _departmentServices.ToggleActiveDepartment(id);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department not Deactivate", HttpStatusCode.NotFound);
            }
            return true;
        }

        [HttpPost]
        [Route("GetDepOfParentCompanies")]
        public HttpResponseMessage GetDepOfParentCompanies(GetParentListDTO obj)
        {
            try
            {
                var Departments = _departmentServices.GetDepOfParentCompanies(obj);
                return Request.CreateResponse(HttpStatusCode.OK, Departments);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Departments not found", HttpStatusCode.NotFound);
            }
        }

        //[HttpGet]
        //[Route("GetActiveDepartments")]
        //public HttpResponseMessage GetActiveDepartments(int CompanyId)
        //{
        //    try
        //    {
        //        var Departments = _departmentServices.GetActiveDepartmentsById(CompanyId);
        //        return Request.CreateResponse(HttpStatusCode.OK, Departments);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Departments not found", HttpStatusCode.NotFound);
        //    }
        //}
    }
}
