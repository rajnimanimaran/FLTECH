using BusinessEntities;
using BusinessEntities.Master1.SalesOrder;
using BusinessServices;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("PurchaseOrder1")]
    public class PurchaseOrderController : ApiController
    {
        private readonly IPurchaseOrderService _PurchaseOrderService;

        public PurchaseOrderController(IPurchaseOrderService sodm)
        {
            _PurchaseOrderService = sodm;
        }


        [HttpGet]
        [Route("AllPurchaseOrder")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Department = _PurchaseOrderService.GetAllsodm();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }


        [HttpGet]
        [Route("GetPoDetails")]
        public HttpResponseMessage Getpodetails(int PurchaseID)
        {
            try
            {
                var Department = _PurchaseOrderService.GetPurchaseOrderDetails(PurchaseID);
                    
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
        //        var Department = _PurchaseOrderService.GetAllSupplierName();
        //        return Request.CreateResponse(HttpStatusCode.OK, Department);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
        //    }
        //}

        [HttpGet]
        [Route("AllSuppliers")]
        public HttpResponseMessage GetSupplierName()
        {
            int x = 10;
            return Request.CreateResponse(HttpStatusCode.OK, x);
            //try
            //{
            //    var Department = _PurchaseOrderService.GetAllSupplierName();
            //    return Request.CreateResponse(HttpStatusCode.OK, Department);
            //}
            //catch (Exception ex)
            //{
            //    throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            //}
        }

        [HttpPost]
        [Route("Create")]
        public bool Post([FromBody]PurchaseOrderCommonEntity PurchaseOrderCommonEntity)
        {
            try
            {
                return _PurchaseOrderService.Create(PurchaseOrderCommonEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]PurchaseOrderCommonEntity purchaseMDEntity)
        {
            try
            {
                if (purchaseMDEntity.PurchaseID > 0)
                {
                    return _PurchaseOrderService.Update(purchaseMDEntity.PurchaseID, purchaseMDEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
            }
            return false;
        }
        [HttpDelete]
        [Route("Delete/{ID}/{PurchaseID}")]
        public bool Delete(int ID ,int PurchaseID)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (ID > 0 )
                {
                    return _PurchaseOrderService.Delete(ID, PurchaseID);
                }
               

            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
            }
            return false;

        }


        [HttpGet]
        [Route("AllSO")]
        public HttpResponseMessage GetSo()
        {
            try
            {
                var Department = _PurchaseOrderService.GetAllpodm();
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
                var Department = _PurchaseOrderService.GetSalesOrderDetails(OrderID);

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
                return _PurchaseOrderService.Create(SalesOrderCommonEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPut]
        [Route("Modifys")]
        public bool Put([FromBody]SalesOrderCommonEntity SalesOrderCommonEntity)
        {
            try
            {
                if (SalesOrderCommonEntity.OrderID > 0)
                {
                    return _PurchaseOrderService.Update(SalesOrderCommonEntity.OrderID, SalesOrderCommonEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
            }
            return false;
        }



      


        //[HttpDelete]
        //[Route("Delete/{ID}/{OrderID}")]
        //public bool Delete(int ID, int OrderID)
        //{
        //    HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
        //    try
        //    {
        //        if (ID > 0)
        //        {
        //            return _SalesOrderService.Delete(ID, OrderID);
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





  