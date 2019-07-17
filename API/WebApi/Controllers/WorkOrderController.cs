using BusinessEntities;
using BusinessServices.WorkOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("WorkOrder")]
    public class WorkOrderController : ApiController
    {

        private readonly IWorkOrderService _WorkOrderService;

        public WorkOrderController(IWorkOrderService workOrderService)
        {
            _WorkOrderService = workOrderService;
        }
        [HttpPost]
        [Route("CreatePO")]
        public HttpResponseMessage Create(WorkOrderEntity WOEntity)
        {
            try
            {
                //return _WorkOrderService.Create(WOEntity);
                var Department = _WorkOrderService.Create(WOEntity);
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }

        //[HttpGet]
        //[Route("AllWorkOrder")]
        //public HttpResponseMessage Get()
        //{
        //    try
        //    {
        //        var Department = _WorkOrderService.GetAllsodm();
        //        return Request.CreateResponse(HttpStatusCode.OK, Department);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
        //    }
        //}

        //[HttpGet]
        //[Route("getSalesOrderDropDown")]
        //public HttpResponseMessage Getsalesorder()
        //{
        //    try
        //    {
        //        var Department = _WorkOrderService.Getsalesorderdrop();
        //        return Request.CreateResponse(HttpStatusCode.OK, Department);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
        //    }
        //}


        //[HttpGet]
        //[Route("GetWoDetails")]
        //public HttpResponseMessage Getpodetails(int WorkOrderID)
        //{
        //    try
        //    {
        //        var Department = _WorkOrderService.GetWorkOrderDetails(WorkOrderID);

        //        return Request.CreateResponse(HttpStatusCode.OK, Department);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
        //    }
        //}


        [HttpGet]
        [Route("GetWosales/{WorkOrderID}")]
        public HttpResponseMessage GetWorkOrdersoDetails(int WorkOrderID)
        {
            try
            {
                var Department = _WorkOrderService.GetWorkOrdersoDetails(WorkOrderID);

                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpGet]
        [Route("WOSdetails/{WODetlID}")]
        public HttpResponseMessage GetWOSdetails(int WODetlID)
        {
            try
            {
                var Department = _WorkOrderService.WOSdetails(WODetlID);
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpGet]
        [Route("QCdetails/{PrdID}")]
        public HttpResponseMessage GetQCdetails(int PrdID)
        {
            try
            {
                var Department = _WorkOrderService.QCdetails(PrdID);
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetOSList/{OrderStatusID}")]
        public HttpResponseMessage GetOSList(int OrderStatusID)
        {
            try
            {
                var Department = _WorkOrderService.GetOSList(OrderStatusID);
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }

        //[HttpGet]
        //[Route("AllSuppliers")]
        //public HttpResponseMessage GetSupplierName()
        //{
        //    int x = 10;
        //    return Request.CreateResponse(HttpStatusCode.OK, x);
        //    //try
        //    //{
        //    //    var Department = _PurchaseOrderService.GetAllSupplierName();
        //    //    return Request.CreateResponse(HttpStatusCode.OK, Department);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
        //    //}
        //}

        //[HttpPost]
        //[Route("Create")]
        //public bool Post([FromBody]WorkOrderEntity WorkOrderEntity)
        //{
        //    try
        //    {
        //        return _WorkOrderService.Create(WorkOrderEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
        //    }
        //}
        //[HttpPut]
        //[Route("Modify")]
        //public bool Put([FromBody]WorkOrderEntity purchaseMDEntity)
        //{
        //    try
        //    {
        //        if (purchaseMDEntity.WorkOrderID > 0)
        //        {
        //            return _WorkOrderService.Update(purchaseMDEntity.WorkOrderID, purchaseMDEntity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
        //    }
        //    return false;
        //}
        ////[HttpDelete]
        ////[Route("Delete/{ID}/{WorkOrderID}")]
        ////public bool Delete(int ID, int PurchaseID)
        ////{
        ////    HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
        ////    try
        ////    {
        ////        if (ID > 0)
        ////        {
        ////            _WorkOrderService.Delete(ID, PurchaseID);
        ////        }


        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
        ////    }
        ////    return false;

        ////}

    }
}
