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
    [RoutePrefix("OrganizationLevel")]
    public class OrganizationLevelController : ApiController
    {
        private readonly IOrganizationLevelServices _OrganizationLevel;

        public OrganizationLevelController(IOrganizationLevelServices OrganizationLevel)
        {
            _OrganizationLevel = OrganizationLevel;
        }

        [HttpGet]
        [Route("AllOrganizationLevel")]
        public HttpResponseMessage Get()
        {
            try
            {
                var OrganizationLevel = _OrganizationLevel.GetAllOrganizationLevel();
                return Request.CreateResponse(HttpStatusCode.OK, OrganizationLevel);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "OrganizationLevel not found", HttpStatusCode.NotFound);
            }
        }



        [HttpPost]
        [Route("Create")]
        public ResultDTO Post([FromBody] OrganizationLevelEntity OrganizationLevel)
        {
            try
            {
                return _OrganizationLevel.CreateOrganizationLevel(OrganizationLevel);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "OrganizationLevel not found", HttpStatusCode.NotFound);
            }

        }


        [HttpGet]
        [Route("GetById/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                //if (id != null)
                //{
                var OrganizationLvl = _OrganizationLevel.GetOrganizationLevelById(id);
                // if (OrganizationLevel != null)
                return Request.CreateResponse(HttpStatusCode.OK, OrganizationLvl);
                //throw new ApiDataException(1001, "No OrganizationLevel found for this id.", HttpStatusCode.NotFound);
                ////  }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "OrganizationLevel  not found", HttpStatusCode.NotFound);
            };
        }



        [HttpPut]
        [Route("Modify")]
        public HttpResponseMessage Put([FromBody] OrganizationLevelEntity OrganizationLevel)
        {
            try
            {
                if (OrganizationLevel.OrganizationLevelId > 0)
                {
                    var result = _OrganizationLevel.UpdateOrganizationLevel(OrganizationLevel.OrganizationLevelId, OrganizationLevel);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "OrganizationLevel not found", HttpStatusCode.NotFound);
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
                    var isSuccess = _OrganizationLevel.DeleteOrganizationLevel(id);
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
                    var isSuccess = _OrganizationLevel.ToggleActiveOrganizationLevel(id);
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
