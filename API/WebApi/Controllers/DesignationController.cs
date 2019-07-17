using BusinessEntities;
using BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ActionFilters;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    // [AuthorizationRequired]
    [RoutePrefix("Designation")]
    public class DesignationController : ApiController
    {
        private readonly IDesignationService _designation;

        public DesignationController(IDesignationService designationInit)
        {
            _designation = designationInit;
        }

        [HttpGet]
        [Route("AllDesignations/{companyId}")]
        public HttpResponseMessage Get(int companyId)
        {
            try
            {
                var Designation = _designation.GetAllDesignation(companyId);

                return Request.CreateResponse(HttpStatusCode.OK, Designation);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Designation not found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetDesignationbyId/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var Designation = _designation.GetDesignationById(id);
                return Request.CreateResponse(HttpStatusCode.OK, Designation);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Designation not found", HttpStatusCode.NotFound);
            }
        }

        //[HttpGet]
        //[Route("GetDesignationbyDepartmentId/{DepartmentId}")]
        //public HttpResponseMessage GetDesignationByDepartmentId(int DepartmentId)
        //{
        //    if (DepartmentId != null)
        //    {
        //        var design = _designation.GetDesignationByDepartmentId(DepartmentId);
        //        if (design != null)
        //            return Request.CreateResponse(HttpStatusCode.OK, design);
        //        throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
        //    }
        //    throw new ApiException()
        //    {
        //        ErrorCode = (int)HttpStatusCode.BadRequest,
        //        ErrorDescription = "Bad Request..."
        //    };
        //}


        [HttpPost]
        [Route("Create")]
        public ResultDTO Post([FromBody] DesignationEntity DesignationEntity)
        {
            try
            {
                return _designation.CreateDesignation(DesignationEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Designation not found", HttpStatusCode.NotFound);
            }

        }
        [HttpPut]
        [Route("Modify")]
        public HttpResponseMessage Put([FromBody] DesignationEntity DesignationEntity)
        {
            try
            {
                if (DesignationEntity.DesignationId > 0)
                {
                    var result = _designation.UpdateDesignation(DesignationEntity.DesignationId, DesignationEntity);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch
            {
                throw new ApiDataException(1000, "Designation not found", HttpStatusCode.NotFound);
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
                    var isSuccess = _designation.DeleteDesignation(id);
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
                    var isSuccess = _designation.ToggleActiveDesignation(id);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Designation not Deactivate", HttpStatusCode.NotFound);
            }
            return true;
        }

        [HttpGet]
        [Route("GetActiveDesignationById")]
        public HttpResponseMessage GetActiveDesignationById()
        {
            try
            {
                var Designation = _designation.GetActiveDesignationById();
                return Request.CreateResponse(HttpStatusCode.OK, Designation);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Designation not found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetDesigByDepId/{depId}")]
        public HttpResponseMessage GetDesigByDepId(int depId)
        {
            try
            {
                var Designation = _designation.GetDesigByDepId(depId);
                return Request.CreateResponse(HttpStatusCode.OK, Designation);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Designation not found", HttpStatusCode.NotFound);
            }
        }
    }
}
