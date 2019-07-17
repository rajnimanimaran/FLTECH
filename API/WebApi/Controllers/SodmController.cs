using BusinessEntities.sodm;
using BusinessServices.sodm;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("sodm")]
    public class SodmController : ApiController
    {
        private readonly IsodmService _sodmService;

        public SodmController(IsodmService sodm)
        {
            _sodmService = sodm;
        }


        [HttpGet]
        [Route("AllSalesOrderMaster")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Department = _sodmService.GetAllsodm();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        //[HttpGet]
        //[Route("GetStateByCountryId")]
        //public HttpResponseMessage GetStateByCountryId(int id)
        //{
        //    if (id != null)
        //    {
        //        var state = _State.GetStateByCountryId(id);
        //        if (state != null)
        //            return Request.CreateResponse(HttpStatusCode.OK, state);
        //        throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
        //    }
        //    throw new ApiException()
        //    {
        //        ErrorCode = (int)HttpStatusCode.BadRequest,
        //        ErrorDescription = "Bad Request..."
        //    };
        //}
        [HttpPost]
        [Route("Create")]
        public bool Post([FromBody]sodmsomdEntity SodmsomdEntity)
        {
            try
            {
                return _sodmService.Create(SodmsomdEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
    }

}


