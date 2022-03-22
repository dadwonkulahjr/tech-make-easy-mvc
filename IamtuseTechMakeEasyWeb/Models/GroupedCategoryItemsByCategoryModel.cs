﻿using System.Linq;

namespace IamtuseTechMakeEasyWeb.Models
{
    public class GroupedCategoryItemsByCategoryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public IGrouping<int,CategoryItemDetailsModel> Items { get; set; }
    }
}
