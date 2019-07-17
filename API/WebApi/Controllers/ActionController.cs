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

namespace WebApi.Controllers
{
    [RoutePrefix("Action")]
    public class ActionController : ApiController
    {
        private readonly IActionServices _actionServices;

        public ActionController(IActionServices action)
        {
            _actionServices = action;
        }

        [HttpGet]
        [Route("AllActions")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Action = _actionServices.GetAllActions();
                return Request.CreateResponse(HttpStatusCode.OK, Action);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Role Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetActionId/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            if (id != 0)
            {
                var Action = _actionServices.GetActionById(id);
                if (Action != null)
                    return Request.CreateResponse(HttpStatusCode.OK, Action);
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
        public int Post([FromBody]ActionEntity actionEntity)
        {
            try
            {
                return _actionServices.CreateAction(actionEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Action Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]ActionEntity actionEntity)
        {
            try
            {
                if (actionEntity.ActionId > 0)
                {
                    return _actionServices.UpdateAction(actionEntity.ActionId, actionEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Action not found", HttpStatusCode.NotFound);
            }
            return false;
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
                    var isSuccess = _actionServices.DeleteAction(id);
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
            };
        }
    }
}
