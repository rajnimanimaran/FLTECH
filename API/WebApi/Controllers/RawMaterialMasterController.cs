using BusinessEntities.Master1.RawMaterial;
using BusinessServices.Master1.RawMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("RawMaterialmaster")]
    public class RawMaterialMasterController : ApiController
    {
        private readonly IRawMaterialService _RawMaterialService;

        public RawMaterialMasterController(BusinessServices.Master1.RawMaterial.IRawMaterialService rawmaterial)
        {
            _RawMaterialService = rawmaterial;
        }


        [HttpGet]
        [Route("AllRawmaterial")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Department = _RawMaterialService.GetAllRawmaterial();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPost]
        [Route("Create")]
        public bool Post([FromBody]RawMaterialMasterEntity RawMaterialMasterEntity)
        {
            try
            {
                return _RawMaterialService.CreateRawmaterial(RawMaterialMasterEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]RawMaterialMasterEntity RawMaterialMasterEntity)
        {
            try
            {
                if (RawMaterialMasterEntity.RMID > 0)
                {

                    return _RawMaterialService.UpdateRawmaterial(RawMaterialMasterEntity.RMID, RawMaterialMasterEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category not found", HttpStatusCode.NotFound);
            }
            return false;
        }
        [HttpDelete]
        [Route("Delete/{RMID}")]
        public bool Delete(int RMID)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (RMID > 0)
                {
                    return _RawMaterialService.DeleteRawmaterial(RMID);
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
