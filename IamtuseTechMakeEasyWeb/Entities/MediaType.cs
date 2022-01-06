﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IamtuseTechMakeEasyWeb.Entities
{
    public class MediaType
    {
        public int Id { get; set; }
        [Required, StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        [StringLength(255), Required]
        public string ThumbnailImagePath { get; set; }

        [ForeignKey("MediaTypeId")]
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
    }
}
