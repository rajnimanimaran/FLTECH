using BusinessEntities.Master1.StockRawMaterial;
using BusinessServices.Master1.StockRawMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("RawMaterialStock")]
    public class rawMaterialStockController : ApiController
    {
        private readonly Irmstockservice _rawmaterialStockServices;

        public rawMaterialStockController(BusinessServices.Master1.StockRawMaterial.Irmstockservice rawMaterial)
        {
            _rawmaterialStockServices = rawMaterial;
        }


        [HttpGet]
        [Route("AllRmsk")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Department = _rawmaterialStockServices.GetAllRmSk();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }


        [HttpGet]
        [Route("AllRmName")]
        public HttpResponseMessage GetName()
        {
            try
            {
                var Department = _rawmaterialStockServices.GetAllRmSks();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPost]
        [Route("Create")]
        public bool Post([FromBody]rawMaterialStockEntity rawMaterialStockEntity)
        {
            try
            {
                return _rawmaterialStockServices.CreateRmSk(rawMaterialStockEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]rawMaterialStockEntity rawMaterialStockEntity)
        {
            try
            {
                if (rawMaterialStockEntity.stkRMID > 0)
                {

                    return _rawmaterialStockServices.UpdateRmSk(rawMaterialStockEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category not found", HttpStatusCode.NotFound);
            }
            return false;
        }
        [HttpDelete]
        [Route("Delete/{stkRMID}")]
        public bool Delete(int stkRMID)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (stkRMID > 0)
                {
                    return _rawmaterialStockServices.DeleteRmSk(stkRMID);
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
