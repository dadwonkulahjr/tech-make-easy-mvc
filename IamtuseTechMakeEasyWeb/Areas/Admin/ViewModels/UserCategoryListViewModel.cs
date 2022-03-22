using System.Collections.Generic;

namespace IamtuseTechMakeEasyWeb.Areas.Admin.ViewModels
{
    public class UserCategoryListViewModel
    {
        public int CategoryId { get; set; }
        public ICollection<UserViewModel> Users { get; set; }

        public ICollection<UserViewModel> UsersSelected { get; set; }
    }
}
