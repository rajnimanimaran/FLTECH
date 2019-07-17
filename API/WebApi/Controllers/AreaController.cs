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
    [RoutePrefix("Area")]
    public class AreaController : ApiController
    {
        private readonly AreaServices _Area;

        public AreaController(AreaServices Area)
        {
            this._Area = Area;
        }

        [HttpGet]
        [Route("AllArea")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Area = _Area.GetAllAreas();
                return Request.CreateResponse(HttpStatusCode.OK, Area);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Area not found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetAreaId/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            if (id != 0)
            {
                var Area = _Area.GetAreaById(id);
                if (Area != null)
                    return Request.CreateResponse(HttpStatusCode.OK, Area);
                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }


        [HttpGet]
        [Route("GetAreaByCityId/{CityId}")]
        public HttpResponseMessage GetAreaByCityId(int CityId)
        {
            if (CityId != 0)
            {
                var area = _Area.GetAreaByCityId(CityId);
                if (area != null)
                    return Request.CreateResponse(HttpStatusCode.OK, area);
                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }
        [HttpGet]
        [Route("GetActiveAreaById")]
        public HttpResponseMessage GetActiveAreaById()
        {
            try
            {
                var Area = _Area.GetActiveAreaById();

                return Request.CreateResponse(HttpStatusCode.OK, Area);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Area not found", HttpStatusCode.NotFound);
            }
        }


        [HttpPost]
        [Route("Create")]
        public ResultDTO Post([FromBody] AreaEntity AreaEntity)
        {
            try
            {
                return _Area.CreateArea(AreaEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Area not found", HttpStatusCode.NotFound);
            }

        }
        [HttpPut]

        [Route("Modify")]
        public HttpResponseMessage Put([FromBody] AreaEntity AreaEntity)
        {
            try
            {
                if (AreaEntity.AreaId > 0)
                {
                    var result = _Area.UpdateArea(AreaEntity.AreaId, AreaEntity);
                    return Request.CreateResponse(HttpStatusCode.OK, result);

                }
            }
            catch
            {
                throw new ApiDataException(1000, "Area not found", HttpStatusCode.NotFound);
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
                    var isSuccess = _Area.DeleteArea(id);
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
                    var isSuccess = _Area.ToggleActiveAreas(id);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Area not Deactivate", HttpStatusCode.NotFound);
            }
            return true;
        }

    }
}