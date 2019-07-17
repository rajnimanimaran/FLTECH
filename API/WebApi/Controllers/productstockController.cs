using BusinessEntities.Master1.ProductStock;
using BusinessServices.Master1.ProductStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class productstockController : ApiController
    {
        private readonly IProductStockService _productstockService;

        public productstockController(BusinessServices.Master1.ProductStock.IProductStockService product)
        {
            _productstockService = product;
        }


        [HttpGet]
        [Route("Allproduct")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Department = _productstockService.GetAllPrdSk();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPost]
        [Route("Create")]
        public bool Post([FromBody]ProductStockEntity productstockEntity)
        {
            try
            {
                return _productstockService.CreatePrdSk(productstockEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]ProductStockEntity productstockEntity)
        {
            try
            {
                if (productstockEntity.prdID > 0)
                {

                    return _productstockService.UpdatePrdSk(productstockEntity.prdID, productstockEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category not found", HttpStatusCode.NotFound);
            }
            return false;
        }
        [HttpDelete]
        [Route("Delete/{StockID}")]
        public bool Delete(int StockID)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (StockID > 0)
                {
                    return _productstockService.DeletePrdSk(StockID);
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
