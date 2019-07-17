using BusinessEntities.Master1.SalesOrder;
using BusinessServices.Master1.SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    public class SalesOrderController : ApiController
    {

        [RoutePrefix("SalesOrdersssssssss")]
        public class PurchaseOrderController : ApiController
        {
            private readonly ISalesOrderService _SalesOrderService;

            public PurchaseOrderController(ISalesOrderService sodm)
            {
                _SalesOrderService = sodm;
            }


            [HttpGet]
            [Route("AllSalesOrder")]
            public HttpResponseMessage Get()
            {
                try
                {
                    var Department = _SalesOrderService.GetAllpodm();
                    return Request.CreateResponse(HttpStatusCode.OK, Department);
                }
                catch (Exception ex)
                {
                    throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
                }
            }


            [HttpGet]
            [Route("GetSoDetails")]
            public HttpResponseMessage Getsodetails(int OrderID)
            {
                try
                {
                    var Department = _SalesOrderService.GetSalesOrderDetails(OrderID);

                    return Request.CreateResponse(HttpStatusCode.OK, Department);
                }
                catch (Exception ex)
                {
                    throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
                }
            }

            //[HttpGet]
            //[Route("AllSupplierName")]
            //public HttpResponseMessage GetSupplierName()
            //{
            //    try
            //    {
            //        var Department = _SalesOrderService.GetAllSupplierName();
            //        return Request.CreateResponse(HttpStatusCode.OK, Department);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            //    }
            //}
            [HttpPost]
            [Route("Create")]
            public bool Post([FromBody]SalesOrderCommonEntity SalesOrderCommonEntity)
            {
                try
                {
                    return _SalesOrderService.Create(SalesOrderCommonEntity);
                }
                catch (Exception ex)
                {
                    throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
                }
            }
            //[HttpPut]
            //[Route("Modify")]
            //public bool Put([FromBody]SalesOrderCommonEntity salesMDEntity)
            //{
            //    try
            //    {
            //        if (salesMDEntity.SalesID > 0)
            //        {
            //            return _SalesOrderService.Update(salesMDEntity.SalesID, salesMDEntity);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
            //    }
            //    return false;
            //}
            //[HttpDelete]
            //[Route("Delete/{ID}/{SalesID}")]
            //public bool Delete(int ID, int SalesID)
            //{
            //    HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            //    try
            //    {
            //        if (ID > 0)
            //        {
            //            return _SalesOrderService.Delete(ID, SalesID);
            //        }


            //    }
            //    catch (Exception ex)
            //    {
            //        throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
            //    }
            //    return false;

            //}
        }
    }
}
