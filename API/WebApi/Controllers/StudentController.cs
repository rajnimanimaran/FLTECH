using BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("Student")]
    public class StudentController : ApiController
    {
        private readonly IStudentService _actionServices;

        public StudentController(IStudentService action)
        {
            _actionServices = action;
        }

        [HttpGet]
        [Route("GetActionId/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            if (id != 0)
            {
                var Action = _actionServices.GetStudentById(id);
                if (Action != null)
                    return Request.CreateResponse(HttpStatusCode.OK, Action);
                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }

    }
}
