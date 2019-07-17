using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class EmpUserDTO
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }        
        public string EmailId { get; set; }        
        public UserRole Role { get; set; }
        public UserCompanyDTO Company { get; set; }
        public UserCompanyDTO SelectedCompany { get; set; }
        public long? RefId { get; set; }
        public string UserType { get; set; }
        public string EmployeeCode { get; set; }

        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class UserCompanyDTO
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
    }

    public class UpdateUserDTO
    {
        [Required]
        public long UserId { get; set; } 
         
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password Must be between 5 and 255 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        [Required]
        public int RefId { get; set; }

        public string UserType { get; set; }
    }



    public class CreateEmpUserRequestDTO
    {       
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password Must be between 5 and 255 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public int RefId { get; set; }

        public string UserType { get; set; }
    }

    public class CreateUserResponseDTO
    {
        public CreateUserResponseDTO()
        {
            Message = new List<string>();
        }
        public bool IsSuccess { get; set; }

        public ICollection<string> Message { get; set; }
    }

    public class CheckUsernameDTO
    {
        public string Username { get; set; }
    }
    public class CheckUserEmailDTO
    {
        public string EmailId { get; set; }
    }

    public class CheckUserAccount
    {
        public int EmployeeId { get; set; }
    }
}
