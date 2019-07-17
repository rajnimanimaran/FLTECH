using BusinessEntities.purchase_Order;
using BusinessServices.Master1.purchase_Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
   

        [RoutePrefix("PurchaseOrder")]
        public class Purchase_OrderController : ApiController
        {
            private readonly IPurchaseOrderMasterService _PurchaseOrderMasterservice;

            public Purchase_OrderController(IPurchaseOrderMasterService sodm)
            {
                _PurchaseOrderMasterservice = sodm;
            }


        [HttpGet]
        [Route("AllPurchaseOrder/{PurchaseID}/{MaterialType}")]
        public HttpResponseMessage Get(int PurchaseID, int MaterialType)
        {
            try
            {
                var Department = _PurchaseOrderMasterservice.GetAllPurchaseDetails(PurchaseID, MaterialType);
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }

        }


        [HttpGet]
        [Route("GetActivePurchaseList")]
        public HttpResponseMessage GetActivePurchaseList()
        {
            try
            {
                var Department = _PurchaseOrderMasterservice.GetActivePurchaseList();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpGet]
        [Route("GetPurchaseListByID/{PurchaseID}/{ActionBy}")]
        public HttpResponseMessage GetPurchaseListByID(int PurchaseID, int ActionBy)
        {
            try
            {
                var Department = _PurchaseOrderMasterservice.GetPurchaseListByID( PurchaseID, ActionBy);
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        



        [HttpPost]
            [Route("Create")]
            public bool Post(PurchaseOrderMasterEntity PurchaseOrderMasterEntity)
            {
                try
                {
                    return _PurchaseOrderMasterservice.Create(PurchaseOrderMasterEntity);
                }
                catch (Exception ex)
                {
                    throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
                }
            }
            [HttpPut]
            [Route("Modify")]
            public bool Put(PurchaseOrderMasterEntity PurchaseOrderMasterEntity)
            {
                try
                {
                    if (PurchaseOrderMasterEntity.PurchaseID > 0)
                    {
                        return _PurchaseOrderMasterservice.Update(PurchaseOrderMasterEntity.PurchaseID, PurchaseOrderMasterEntity);
                    }
                }
                catch (Exception ex)
                {
                    throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
                }
                return false;
            }


            [HttpDelete]
            [Route("PurchaseID")]
            public bool Delete(int PurchaseID, int ActionBy)
            {
                HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
                try
                {
                    if (PurchaseID > 0)
                    {
                        return _PurchaseOrderMasterservice.Delete(PurchaseID, ActionBy);
                    }


                }
                catch (Exception ex)
                {
                    throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
                }
                return false;

            }
            [HttpPost]
            [Route("AllPurchaseOrder")]
            public HttpResponseMessage GetPurchaseOrderByID(GetPurchaseOrderByID obj)
            {
                try
                {
                    var Department = _PurchaseOrderMasterservice.get(obj);
                    return Request.CreateResponse(HttpStatusCode.OK, Department);
                }
                catch (Exception ex)
                {
                    throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
                }
            }


        }

    }

