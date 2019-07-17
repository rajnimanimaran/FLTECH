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
    [RoutePrefix("RoleMenuMapping")]
    public class RoleMenuMappingController : ApiController
    {
        private readonly IRoleMenuMappingServices _roleMenuMapServices;

        public RoleMenuMappingController(IRoleMenuMappingServices roleMenuMap)
        {
            _roleMenuMapServices = roleMenuMap;
        }

        //[HttpGet]
        //[Route("AllRoleMenuMap")]
        //public HttpResponseMessage Get()
        //{
        //    try
        //    {
        //        var Role = _roleMenuMapServices.GetAllRoleMenuMapping();
        //        return Request.CreateResponse(HttpStatusCode.OK, Role);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Role Not Found", HttpStatusCode.NotFound);
        //    }
        //}

        //[HttpGet]
        //[Route("GetRoleMenuMapId/{id}")]
        //public HttpResponseMessage GetById(int id)
        //{
        //    if (id != null)
        //    {
        //        var roleMenuMap = _roleMenuMapServices.GetRoleMenuMappingById(id);
        //        if (roleMenuMap != null)
        //            return Request.CreateResponse(HttpStatusCode.OK, roleMenuMap);
        //        throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
        //    }
        //    throw new ApiException()
        //    {
        //        ErrorCode = (int)HttpStatusCode.BadRequest,
        //        ErrorDescription = "Bad Request..."
        //    };
        //}


        [HttpPost]
        [Route("Save/{roleId}")]
        public HttpResponseMessage Post(int roleId, [FromBody]IEnumerable<MenuItemsEntity> menuItemsEntity)
        {
            try
            {
                if (_roleMenuMapServices.CreateRoleMenuMapping(roleId, menuItemsEntity))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Failed Saving! Contact Admin");
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1011, "Failed Saving! Contact Admin", HttpStatusCode.InternalServerError);
            }
        }

        //[HttpPut]
        //[Route("Modify/{id}")]
        //public bool Put([FromBody]RoleMenuMappingEntity roleMenuMapEntity)
        //{
        //    try
        //    {
        //        if (roleMenuMapEntity.RoleMenuMappingId > 0)
        //        {
        //            return _roleMenuMapServices.UpdateRoleMenuMapping(roleMenuMapEntity.RoleMenuMappingId, roleMenuMapEntity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Role not found", HttpStatusCode.NotFound);
        //    }
        //    return false;
        //}
        //[HttpDelete]
        //[Route("Delete/{id}")]
        //public bool Delete(int id)
        //{
        //    if (id > 0)
        //    {
        //        var isSuccess = _roleMenuMapServices.DeleteRoleMenuMapping(id);
        //        if (isSuccess)
        //        {
        //            return isSuccess;
        //        }

        //    }
        //    throw new ApiException()
        //    {
        //        ErrorCode = (int)HttpStatusCode.BadRequest,
        //        ErrorDescription = "Bad Request..."
        //    };
        //}

        [HttpGet]
        [Route("GetMenuMappingByRoleId/{roleId}")]
        public HttpResponseMessage GetMenuMappingByRoleId(int roleId)
        {
            try
            {
                var roleMappingList = _roleMenuMapServices.GetRoleMenuMapDetails(roleId);
                return Request.CreateResponse(HttpStatusCode.OK, roleMappingList);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1012, "Failed retriveing Menu Mapping! Contact Admin", HttpStatusCode.InternalServerError);
            }
        }
    }
}
