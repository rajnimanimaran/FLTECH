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
    [RoutePrefix("City")]
    public class CityController : ApiController
    {
        private readonly ICityService _city;

        public CityController(ICityService cityInit)
        {
            _city = cityInit;
        }

        [HttpGet]
        [Route("AllCity")]
        public HttpResponseMessage Get()
        {
            try
            {
                var City = _city.GetAllCity();

                return Request.CreateResponse(HttpStatusCode.OK, City);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "City not found", HttpStatusCode.NotFound);
            }
        }


        [HttpGet]
        [Route("GetCityId/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            if (id != 0)
            {
                var city = _city.GetCityById(id);
                if (city != null)
                    return Request.CreateResponse(HttpStatusCode.OK, city);
                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }

        [HttpGet]
        [Route("GetCityByStateId")]
        public HttpResponseMessage GetCityByStateId(int StateId)
        {
            if (StateId != 0)
            {
                var state = _city.GetCityByStateId(StateId);
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
        [Route("GetActiveCityById")]
        public HttpResponseMessage GetActiveCityById()
        {
            try
            {
                var ObjCity = _city.GetActiveCityById();
                return Request.CreateResponse(HttpStatusCode.OK, ObjCity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "City not found", HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("Create")]
        public ResultDTO Post([FromBody] CityEntity CityEntity)
        {
            try
            {
                return _city.CreateCity(CityEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "City not found", HttpStatusCode.NotFound);
            }

        }
        [HttpPut]
        [Route("Modify")]
        public HttpResponseMessage Put([FromBody] CityEntity CityEntity)
        {
            try
            {
                if (CityEntity.CityId > 0)
                {
                    var result = _city.UpdateCity(CityEntity.CityId, CityEntity);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch
            {
                throw new ApiDataException(1000, "City not found", HttpStatusCode.NotFound);
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
                    var isSuccess = _city.DeleteCity(id);
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
                    var isSuccess = _city.ToggleActiveCity(id);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "City not Deactivate", HttpStatusCode.NotFound);
            }
            return true;
        }
    }
}
