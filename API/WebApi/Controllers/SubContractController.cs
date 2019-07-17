using BusinessEntities.Master1.SubContract;
using BusinessServices.Master1.SubContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("SubContract")]
    public class SubContractController : ApiController
    {
        private readonly ISubCont _SubContService;

        public SubContractController(BusinessServices.Master1.SubContract.ISubCont contract)
        {
            _SubContService = contract;
        }


        [HttpGet]
        [Route("Allcontract")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Department = _SubContService.GetAllsub();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPost]
        [Route("Create")]
        public bool Post([FromBody]SubContEntity SubContEntity)
        {
            try
            {
                return _SubContService.Createsub(SubContEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]SubContEntity SubContEntity)
        {
            try
            {
                if (SubContEntity.subContID > 0)
                {

                    return _SubContService.Updatesub(SubContEntity.subContID, SubContEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category not found", HttpStatusCode.NotFound);
            }
            return false;
        }
        [HttpDelete]
        [Route("Delete/{subContID}")]
        public bool Deletesub(int subContID)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (subContID > 0)
                {
                    return _SubContService.Deletesub(subContID);
                }

            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
            return false;
        }

    }
}
