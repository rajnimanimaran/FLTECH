using BusinessEntities.BOM_Master;
using BusinessServices.BOM_Master;
//using BusinessServices.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers

{
    [RoutePrefix("BOM_master")]
    public class BOM_masterController : ApiController
    {
        private readonly IBOMmasterServices _BOMmasterServices;

        public BOM_masterController(BusinessServices.BOM_Master.IBOMmasterServices BOM_master)
        {
            _BOMmasterServices = BOM_master;
        }


        [HttpGet]
        [Route("AllBOM_master/{BOMID}")]
        public HttpResponseMessage Get(int BOMID)
        {
            try
            {
                var Department = _BOMmasterServices.GetAllBOM_Master(BOMID);
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPost]
        [Route("Create")]
        public bool Post([FromBody]BOM_masterEntity BOM_masterEntity)
        {
            try
            {
                return _BOMmasterServices.CreateBOM_master (BOM_masterEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]BOM_masterEntity BOM_masterEntity)
        {
            try
            {
                if (BOM_masterEntity.BOMID > 0)
                {

                    return _BOMmasterServices.UpdateBOM_master(BOM_masterEntity.BOMID, BOM_masterEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category not found", HttpStatusCode.NotFound);
            }
            return false;
        }
        [HttpDelete]
        [Route("Delete/{BOMID}")]
        public bool Delete(int BOMID)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (BOMID > 0)
                {
                    return _BOMmasterServices.DeleteBOM_master(BOMID);
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
