using BusinessEntities;
using BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ActionFilters;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    //[AuthorizationRequired]
    [RoutePrefix("User")]
    public class UserController : ApiController
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices user)
        {
            _userServices = user;
        }

        [HttpGet]
        [Route("AllUsers")]
        public HttpResponseMessage Get()
        {
            try
            {
                var User = _userServices.GetAllUsers();
                return Request.CreateResponse(HttpStatusCode.OK, User);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "User Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public HttpResponseMessage GetById(long id)
        {
            if (id != 0)
            {
                var User = _userServices.GetUserById(id);
                if (User != null)
                    return Request.CreateResponse(HttpStatusCode.OK, User);
                throw new ApiDataException(1001, "No User found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }

        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage Post([FromBody]CreateEmpUserRequestDTO userEntity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userEntity.UserType = "EMP";
                    var res = _userServices.Register(userEntity);
                    if (res.IsSuccess)
                        return Request.CreateResponse(HttpStatusCode.OK, res);
                    else
                        return Request.CreateResponse(HttpStatusCode.BadRequest, res);
                }
                else
                {
                    // the code below should probably be refactored into a GetModelErrors
                    // method on your BaseApiController or something like that

                    var errors = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.BadRequest, new CreateUserResponseDTO
                    {
                        IsSuccess = false,
                        Message = errors
                    });
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Something wrong, Try Again!", HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetUserMenuByUserId/{id}")]
        public HttpResponseMessage GetUserMenuByUserId(long id)
        {
            if (id != 0)
            {
                var menuList = _userServices.GetUserMenuList(id);
                if (menuList != null)
                    return Request.CreateResponse(HttpStatusCode.OK, menuList);
                throw new ApiDataException(1001, "No User found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }

        [HttpGet]
        [Route("GetUserCompanies/{userId}")]
        public HttpResponseMessage GetUserCompanies(long userId)
        {
            if (userId != 0)
            {
                var companyList = _userServices.GetCompanyByUserId(userId);
                if (companyList != null)
                    return Request.CreateResponse(HttpStatusCode.OK, companyList);
                throw new ApiDataException(1001, "No User found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }

        [HttpPost]
        [Route("UpdateSelectedCompany")]
        public HttpResponseMessage UpdateSelectedCompany(UpdateSelectCompanyDTO obj)
        {
            if (obj != null)
            {
                var res = _userServices.UpdateSelectedCompany(obj.UserId, obj.CompanyId);
                if (res != false)
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                throw new ApiDataException(1001, "No User found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }

        [HttpPut]
        [Route("Modify")]
        public HttpResponseMessage Put([FromBody]UpdateUserDTO userEntity)
        {
            try
            {
                if (userEntity.UserId > 0)
                {
                    var result = _userServices.UpdateUser(userEntity.UserId, userEntity);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Role not found", HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "InternalServerError");
        }


        [HttpPost]
        [Route("CheckUsername")]
        public HttpResponseMessage CheckUsername(CheckUsernameDTO obj)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(obj.Username))
                {
                    var res = _userServices.CheckUsername(obj.Username);

                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, false);
            }
            catch (Exception ex)
            {

                throw new ApiException()
                {
                    ErrorCode = (int)HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request..."
                };
            }

        }

        [HttpPost]
        [Route("CheckUserEmailId")]
        public HttpResponseMessage CheckUserEmailId(CheckUserEmailDTO obj)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(obj.EmailId))
                {
                    var res = _userServices.CheckUserEmailId(obj.EmailId);

                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, false);
            }
            catch (Exception ex)
            {
                throw new ApiException()
                {
                    ErrorCode = (int)HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request..."
                };
            }
        }

        [HttpPost]
        [Route("CheckUserAccount")]
        public HttpResponseMessage CheckUserAccount(CheckUserAccount obj)
        {
            try
            {
                var res = _userServices.CheckUserAccount(obj.EmployeeId);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                throw new ApiException()
                {
                    ErrorCode = (int)HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request..."
                };
            }
        }

        [HttpGet]
        [Route("GetUsersByCompId/{compId}")]
        public HttpResponseMessage GetUserByCompId(int compId)
        {
            try
            {
                var User = _userServices.GetUserByCompId(compId);
                return Request.CreateResponse(HttpStatusCode.OK, User);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "User Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        [Route("Delete/{UserId}")]
        public HttpResponseMessage Delete(long UserId)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {

                if (UserId > 0)
                {
                    var isSuccess = _userServices.DeleteUser(UserId);
                    if (isSuccess)
                    {
                        msg = Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                    }

                }
                return msg;
            }
            catch (Exception ex)
            {
                return msg;
            }
        }
    }
}
