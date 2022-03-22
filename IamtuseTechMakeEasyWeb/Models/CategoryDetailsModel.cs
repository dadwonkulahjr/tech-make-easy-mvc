using IamtuseTechMakeEasyWeb.Entities;
using System.Collections.Generic;

namespace IamtuseTechMakeEasyWeb.Models
{
    public class CategoryDetailsModel
    {
        public IEnumerable<GroupedCategoryItemsByCategoryModel> GroupedCategoryItemsByCategoryModels { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
