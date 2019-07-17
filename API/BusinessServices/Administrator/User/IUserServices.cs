using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IUserServices
    {
        EmpUserDTO GetUserById(long UserId);
        IEnumerable<EmpUserDTO> GetAllUsers();
        IEnumerable<EmpUserDTO> GetUserByCompId(int compId);
        CreateUserResponseDTO Register(CreateEmpUserRequestDTO UserEntity);
        ResultDTO UpdateUser(long UserId,UpdateUserDTO UserEntity);
        long Authenticate(string username, string password);
        IEnumerable<BusinessEntities.MenuItemsEntity> GetUserMenuList(long userId);
        IEnumerable<UserCompanyDTO> GetCompanyByUserId(long userId);
        bool UpdateSelectedCompany(long userId, int companyId);
        bool CheckUsername(string userName);
        bool CheckUserEmailId(string emailId);
        bool DeleteUser(long UserId);
        bool CheckUserAccount(int EmployeeId);
       
    }
}
