
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
    [RoutePrefix("State")]
    public class StateController : ApiController
    {
        private readonly IStateServices _State;

        public StateController(IStateServices State)
        {
            this._State = State;
        }

        [HttpGet]
        [Route("AllState")]
        public HttpResponseMessage Get()
        {
            try
            {
                var State = _State.GetAllStates();
                return Request.CreateResponse(HttpStatusCode.OK, State);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "State not found", HttpStatusCode.NotFound);
            }
        }
        [HttpGet]
        [Route("GetStateId/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            if (id != 0)
            {
                var State = _State.GetStateById(id);
                if (State != null)
                    return Request.CreateResponse(HttpStatusCode.OK, State);
                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }

        [HttpGet]
        [Route("GetStateByCountryId")]
        public HttpResponseMessage GetStateByCountryId(int id)
        {
            if (id != 0)
            {
                var state = _State.GetStateByCountryId(id);
                if (state != null)
                    return Request.CreateResponse(HttpStatusCode.OK, state);
                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }

        [HttpGet]
        [Route("GetActiveStateById")]
        public HttpResponseMessage GetActiveStateById()
        {
            try
            {
                var state = _State.GetActiveStateById();

                return Request.CreateResponse(HttpStatusCode.OK, state);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category not found", HttpStatusCode.NotFound);
            }
        }



        [HttpPost]
        [Route("Create")]
        public ResultDTO Post([FromBody] StateEntity StateEntity)
        {
            try
            {
                return _State.CreateState(StateEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "State not found", HttpStatusCode.NotFound);
            }

        }

        [HttpPut]
        [Route("Modify")]
        public HttpResponseMessage Put([FromBody] StateEntity StateEntity)
        {
            try
            {
                if (StateEntity.StateId > 0)
                {
                    var result = _State.UpdateState(StateEntity.StateId, StateEntity);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch
            {
                throw new ApiDataException(1000, "State not found", HttpStatusCode.NotFound);
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
                    var isSuccess = _State.DeleteState(id);
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
                    var isSuccess = _State.ToggleActiveState(id);

                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "State not Deactivate", HttpStatusCode.NotFound);
            }
            return true;
        }
    }
}