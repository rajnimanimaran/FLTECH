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
    [AuthorizationRequired]
    [RoutePrefix("UserMenuMapping")]
    public class UserMenuMappingController : ApiController
    {
        private readonly IUserMenuMappingServices _userMenuMapServices;

        public UserMenuMappingController(IUserMenuMappingServices roleMenuMap)
        {
            _userMenuMapServices = roleMenuMap;
        }

        [HttpPost]
        [Route("Save/{userId}")]
        public HttpResponseMessage Post(long userId, [FromBody]IEnumerable<MenuItemsEntity> menuItemsEntity)
        {
            try
            {
                if (_userMenuMapServices.CreateUserMenuMapping(userId, menuItemsEntity))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Failed Saving! Contact Admin");
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1021, "Failed Saving! Contact Admin", HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetMenuMappingByUserId/{userId}")]
        public HttpResponseMessage GetMenuMappingByUserId(long userId)
        {
            try
            {
                var userMappingList = _userMenuMapServices.GetUserMenuMapDetails(userId);
                return Request.CreateResponse(HttpStatusCode.OK, userMappingList);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1022, "Failed retriveing Menu Mapping! Contact Admin", HttpStatusCode.InternalServerError);
            }
        }
    }
}
