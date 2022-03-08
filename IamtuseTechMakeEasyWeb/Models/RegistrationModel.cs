using System.ComponentModel.DataAnnotations;

namespace IamtuseTechMakeEasyWeb.Models
{
    public class RegistrationModel
    {
        [EmailAddress, Required, Display(Name ="User Name")]
        public string Email { get; set; }
        [DataType(DataType.Password), Required]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password do not match."), Required,
            Display(Name ="Confirm Password"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Address1 { get; set; }
        [Required]
        public string Address2 { get; set; }
        [Display(Name ="Post Code"), Required]
        public string PostCode { get; set; }
        [Display(Name ="First Name"), Required]
        public string FirstName { get; set; }
        [Display(Name ="Last Name"), Required]
        public string LastName { get; set; }
        [Display(Name ="Acccept User Agreement")]
        public bool AcceptUserAgreement { get; set; }

        public string RegistrationInvalid { get; set; }


        [Display(Name ="Phone Number"), Required]
        public string PhoneNumber { get; set; }

    }
}
