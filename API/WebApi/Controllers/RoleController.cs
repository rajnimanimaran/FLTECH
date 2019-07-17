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
   //[AuthorizationRequired]
    [RoutePrefix("Role")]
    public class RoleController : ApiController
    {
        private readonly IRoleServices _roleServices;

        public RoleController(IRoleServices role)
        {
            _roleServices = role;
        }

        [HttpGet]
        [Route("AllRoles")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Role = _roleServices.GetAllRoles();
                return Request.CreateResponse(HttpStatusCode.OK, Role);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Role Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetRoleId/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            if (id != 0)
            {
                var Role = _roleServices.GetRoleById(id);
                if (Role != null)
                    return Request.CreateResponse(HttpStatusCode.OK, Role);
                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }


        [HttpPost]
        [Route("Create")]
        public ResultDTO Post([FromBody]RoleEntity roleEntity)
        {
            try
            {
                return _roleServices.CreateRole(roleEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Role Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        [Route("Modify")]
        public HttpResponseMessage Put([FromBody]RoleEntity roleEntity)
        {
            try
            {
                if (roleEntity.RoleId > 0)
                {
                    var result = _roleServices.UpdateRole(roleEntity.RoleId, roleEntity);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Role not found", HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "InternalServerError");
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
                    var isSuccess = _roleServices.DeleteRole(id);
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
                    var isSuccess = _roleServices.ToggleActiveRole(id);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "State not Deactivate", HttpStatusCode.NotFound);
            }
            return true;
        }

        [HttpGet]
        [Route("GetActiveRole")]
        public HttpResponseMessage GetActiveRole()
        {
            try
            {
                var objRole = _roleServices.GetActiveRolesById();
                return Request.CreateResponse(HttpStatusCode.OK, objRole);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Role not found", HttpStatusCode.NotFound);
            }
        }
    }
}
