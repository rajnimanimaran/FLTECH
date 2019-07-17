using BusinessEntities;
using BusinessServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ActionFilters;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    // [AuthorizationRequired]
    [RoutePrefix("Country")]
    public class CountryController : ApiController
    {
        private readonly ICountryService _Country;

        public CountryController(ICountryService Country)
        {
            _Country = Country;
        }


        [HttpGet]
        [Route("AllCountry")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Country = _Country.GetAllCountry();
                // var CountryEntity = _Country as List<CountryEntity> ?? Country.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, Country);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Country not found", HttpStatusCode.NotFound);
            }
        }


        [HttpPost]
        [Route("Create")]
        public ResultDTO Post([FromBody] CountryEntity Country)
        {

            try
            {
                return _Country.CreateCountry(Country);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Country not create", HttpStatusCode.NotFound);
            }

        }


        [HttpGet]
        [Route("GetActiveCountryById")]
        public HttpResponseMessage GetActiveCountryById()
        {
            try
            {
                var Country = _Country.GetActiveCountryById();
                // var CountryEntity = _Country as List<CountryEntity> ?? Country.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, Country);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Country not found", HttpStatusCode.NotFound);
            }
        }

        // GET api/Coutry/5
        [HttpGet]
        [Route("GetById/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                //if (id != null)
                //{
                var country = _Country.GetCountryById(id);
                // if (country != null)
                return Request.CreateResponse(HttpStatusCode.OK, country);
                //throw new ApiDataException(1001, "No country found for this id.", HttpStatusCode.NotFound);
                ////  }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Country not found", HttpStatusCode.NotFound);
            };
        }

        [HttpPut]
        [Route("Modify")]
        public HttpResponseMessage Put([FromBody] CountryEntity Country)
        {
            try
            {
                if (Country.CountryId > 0)
                {
                    var result = _Country.UpdateCountry(Country.CountryId, Country);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Country not Modified", HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "InternalServerError");
        }

        [HttpPost]
        [Route("ChangeActive/{id}")]
        public bool DeActivate(int id)
        {

            try
            {
                if (id > 0)
                {
                    var isSuccess = _Country.ToggleActiveCountry(id);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Country not Deactivate", HttpStatusCode.NotFound);
            }
            return true;
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
                    var isSuccess = _Country.DeleteCountry(id);
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



    }
}
