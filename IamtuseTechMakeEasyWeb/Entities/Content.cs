﻿using System.ComponentModel.DataAnnotations;

namespace IamtuseTechMakeEasyWeb.Entities
{
    public class Content
    {
        public int Id { get; set; }
        [Required, StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        [StringLength(255, MinimumLength = 5)]
        public string HtmlContent { get; set; }
        [StringLength(255, MinimumLength = 5)]
        public string VideoLink { get; set; }

        //Navigation Properties
        //Foreign Key Constraint

        public virtual CategoryItem CategoryItem { get; set; }
    }
}
