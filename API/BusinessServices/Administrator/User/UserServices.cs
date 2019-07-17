using AutoMapper;
using BusinessEntities;
using BusinessServices.Utility;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyServices _companyService;
        private readonly IUserMenuMappingServices _userMenuMappingServices;

        public UserServices(IUnitOfWork unitOfWork, ICompanyServices companyService, IUserMenuMappingServices userMenuMappingServices)
        {
            _unitOfWork = unitOfWork;
            _companyService = companyService;
            _userMenuMappingServices = userMenuMappingServices;
        }

        public EmpUserDTO GetUserById(long UserId)
        {
            var user = _unitOfWork.UserRepository.GetByID(UserId);
            if (user != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<OrganizationLevel, OrganizationLevelEntity>();
                    cfg.CreateMap<Role, UserRole>();
                    cfg.CreateMap<Company, UserCompanyDTO>();
                    cfg.CreateMap<User, EmpUserDTO>().ForMember("SelectedCompany", p => p.Ignore());
                });
                IMapper mapper = config.CreateMapper();

                var resUser = mapper.Map<User, EmpUserDTO>(user);
                if (user.SelectedCompany != null)
                {
                    var selectedCompany = _companyService.GetCompanyById(user.SelectedCompany ?? 0);
                    resUser.SelectedCompany = new UserCompanyDTO
                    {
                        CompanyId = selectedCompany.CompanyId,
                        CompanyCode = selectedCompany.CompanyCode,
                        CompanyName = selectedCompany.CompanyName
                    };
                }
                else
                    resUser.SelectedCompany = null;

                return resUser;
            }
            return null;
        }

        public IEnumerable<EmpUserDTO> GetAllUsers()
        {
            var users = _unitOfWork.UserRepository.GetAll().ToList();
            if (users.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<OrganizationLevel, OrganizationLevelEntity>();
                    cfg.CreateMap<Role, UserRole>();
                    cfg.CreateMap<Company, UserCompanyDTO>();
                    cfg.CreateMap<User, EmpUserDTO>().ForMember("SelectedCompany", p => p.Ignore());
                });
                IMapper mapper = config.CreateMapper();

                var userList = mapper.Map<List<User>, List<EmpUserDTO>>(users);

                foreach (var user in userList)
                {
                    user.LastName = (user.LastName == null) ? "" : user.LastName;
                    if (user.UserType == "EMP")
                    {
                        var emp = _unitOfWork.EmployeeRepository.GetByID(user.RefId);
                        if (emp != null)
                            user.EmployeeCode = emp.EmployeeCode;
                        else
                            user.EmployeeCode = "";
                    }
                }

                return userList;
            }
            return null;
        }

        public bool DeleteUser(long UserId)
        {
            var success = false;
            if (UserId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var user = _unitOfWork.UserRepository.GetByID(UserId);
                    if (user != null)
                    {
                        _unitOfWork.UserRepository.Delete(user);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }

            }
            return success;
        }

        public CreateUserResponseDTO Register(CreateEmpUserRequestDTO regDetail)
        {
            CreateUserResponseDTO response = new CreateUserResponseDTO();
            try
            {
                User user = new User();

                user.Username = regDetail.Username;
                user.FirstName = regDetail.FirstName;
                user.LastName = regDetail.LastName;
                user.EmailId = regDetail.EmailId;
                user.CreatedBy =  regDetail.CreatedBy;
                user.ModifiedBy = regDetail.CreatedBy;
                user.ModifiedOn = DateTime.Now;
                user.CreatedOn =  DateTime.Now;
                user.RoleId = regDetail.RoleId;
                user.CompanyId = regDetail.CompanyId;
                user.IsActive = true;
                user.UserType = regDetail.UserType;
                user.RefId = regDetail.RefId;

                Secures SecTemp = new Secures();

                user.PasswordKey = SecTemp.AuthKey(32);
                user.PasswordHash = SecTemp.Encrytp_String(regDetail.Password, user.PasswordKey);

                User usernameCheck = new User();
                User emailCheck = new User();

                usernameCheck = _unitOfWork.UserRepository.Get(u => u.Username == regDetail.Username);
                emailCheck = _unitOfWork.UserRepository.Get(u => u.EmailId == regDetail.EmailId);
                if (usernameCheck == null && emailCheck == null)
                {
                    var res = _unitOfWork.UserRepository.Insert(user, true);
                    _unitOfWork.Save();
                    if (res != null)
                    {
                        User testUser = new User();
                        testUser = _unitOfWork.UserRepository.Get(u => u.Username == regDetail.Username && u.EmailId == regDetail.EmailId);
                        if (testUser != null)
                        {
                            response.IsSuccess = true;
                            response.Message.Add("User created Successfully!");
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message.Add("Failed Try again after sometime");
                        }
                        //SecTemp.Send_Register_Mail(testUser.email, regDetail.Password, testUser.user_tblname, testUser.UserRole.user_role);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message.Add("Failed Try again after sometime");
                    }
                }
                else
                {
                    if (usernameCheck != null)
                    {
                        response.IsSuccess = false;
                        response.Message.Add("Username already Exist");
                    }
                    if (emailCheck != null)
                    {
                        response.IsSuccess = false;
                        response.Message.Add("Email already Exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public long Authenticate(string username, string password)
        {
            var user = _unitOfWork.UserRepository.Get(u => (u.Username == username || u.EmailId == username)
                && (new Secures().Decrypt_String(u.PasswordHash, u.PasswordKey)) == password);

            if (user != null && user.UserId > 0)
            {
                return user.UserId;
            }
            return 0;
        }

        public IEnumerable<BusinessEntities.MenuItemsEntity> GetUserMenuList(long userId)
        {
            var menuList = _userMenuMappingServices.GetUserMenuMapDetails(userId);

            removeUnauthMenusFromList(menuList);

            return menuList;
        }

        private void removeUnauthMenusFromList(IEnumerable<MenuItemsEntity> menuList)
        {
            foreach (var menu in menuList.ToList())
            {
                if (!menu.IsSelected)
                {
                    ((List<MenuItemsEntity>)menuList).Remove(menu);
                }
                else
                {
                    foreach (var action in menu.MenuAction.ToList())
                    {
                        if (!action.IsSelected)
                        {
                            menu.MenuAction.Remove(action);
                        }
                    }
                    removeUnauthMenusFromList(menu.SubMenuItems);
                }
            }
        }

        public IEnumerable<UserCompanyDTO> GetCompanyByUserId(long userId)
        {
            var company = GetUserById(userId).Company;
            var allCompanies = _unitOfWork.CompanyRepository.GetAll().Where(c => c.IsActive == true);

            var userCompanies = new List<Company>();
            userCompanies.Add(allCompanies.First(c => c.CompanyId == company.CompanyId));

            userCompanies.AddRange(CommonService.getSubCompanyList(allCompanies, userCompanies.First().CompanyId));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Company, UserCompanyDTO>();
            });
            IMapper mapper = config.CreateMapper();

            var result = mapper.Map<List<Company>, List<UserCompanyDTO>>(userCompanies);

            return result;
        }

        public bool UpdateSelectedCompany(long userId, int companyId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var user = _unitOfWork.UserRepository.GetByID(userId);

                    user.SelectedCompany = companyId;

                    _unitOfWork.UserRepository.Update(user);
                    _unitOfWork.Save();
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckUsername(string userName)
        {
            var user = _unitOfWork.UserRepository.GetMany(u => u.Username.Trim() == userName.Trim()).ToList();

            if (user.Count > 0)
                return true;

            return false;
        }

        public bool CheckUserEmailId(string emailId)
        {
            var user = _unitOfWork.UserRepository.GetMany(u => u.EmailId.Trim() == emailId.Trim()).ToList();

            if (user.Count > 0)
                return true;

            return false;
        }

        public IEnumerable<EmpUserDTO> GetUserByCompId(int compId)
        {
            var users = _unitOfWork.UserRepository.GetMany(u => u.CompanyId == compId).ToList();
            if (users.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<OrganizationLevel, OrganizationLevelEntity>();
                    cfg.CreateMap<Role, UserRole>();
                    cfg.CreateMap<Company, UserCompanyDTO>();
                    cfg.CreateMap<User, EmpUserDTO>().ForMember("SelectedCompany", p => p.Ignore());
                });
                IMapper mapper = config.CreateMapper();

                var userList = mapper.Map<List<User>, List<EmpUserDTO>>(users);

                foreach (var user in userList)
                {
                    user.LastName = (user.LastName == null) ? "" : user.LastName;
                    if (user.UserType == "EMP")
                    {
                        var emp = _unitOfWork.EmployeeRepository.GetByID(user.RefId);
                        if (emp != null)
                            user.EmployeeCode = emp.EmployeeCode;
                        else
                            user.EmployeeCode = "";
                    }
                }

                return userList;
            }
            return null;
        }

        public ResultDTO UpdateUser(long UserId,UpdateUserDTO UserEntity)
        {
            var result = new ResultDTO { IsSuccess = false };
            if(UserEntity != null)
            {    
                    using (var scope = new TransactionScope())
                    {
                        var user = _unitOfWork.UserRepository.GetByID(UserId);
                        if (UserEntity != null)
                        {
                            user.UserId = UserEntity.UserId;
                            user.Username = UserEntity.Username;
                            user.FirstName = UserEntity.FirstName;
                            user.LastName = UserEntity.LastName;
                            user.EmailId = UserEntity.EmailId;
                            user.RoleId = UserEntity.RoleId; 
                            user.ModifiedBy = UserEntity.ModifiedBy;
                            user.ModifiedOn = DateTime.Now; 
                            _unitOfWork.UserRepository.Update(user);
                            _unitOfWork.Save();
                            scope.Complete();

                            result.IsSuccess = true;
                            result.Message = "Updated Successfully";
                        }
                    }
                           
               
            }
            return result;

        }

        public bool CheckUserAccount(int EmployeeId)
        {
            var user = _unitOfWork.UserRepository.GetMany(u => u.RefId == EmployeeId).ToList();

            if (user.Count > 0)
                return true;

            return false;
        }
    }
}
