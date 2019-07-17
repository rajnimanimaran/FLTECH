using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using BusinessServices;
using WebApi.ErrorHelper;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ActionFilters;


namespace WebApi.Controllers
{
    // [AuthorizationRequired]
    [RoutePrefix("Menu")]
    public class MenuController : ApiController
    {
        private readonly IMenuServices _menuServices;

        public MenuController(IMenuServices menu)
        {
            _menuServices = menu;
        }

        [HttpGet]
        [Route("AllMenus")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Menu = _menuServices.GetAllMenus();
                return Request.CreateResponse(HttpStatusCode.OK, Menu);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Role Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetMenuId")]
        public HttpResponseMessage GetById(int id)
        {
            if (id != 0)
            {
                var Menu = _menuServices.GetMenuById(id);
                if (Menu != null)
                    return Request.CreateResponse(HttpStatusCode.OK, Menu);
                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }
    }
}
