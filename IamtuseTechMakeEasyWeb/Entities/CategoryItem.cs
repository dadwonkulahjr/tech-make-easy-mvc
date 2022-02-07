using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IamtuseTechMakeEasyWeb.Entities
{
    public class CategoryItem
    {
        private DateTime _releasedDate = DateTime.MinValue;
        public int Id { get; set; }
        [StringLength(200, MinimumLength =2)]
        public string Title { get; set; }
        [Column(TypeName ="date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]

        public DateTime DateTimeItemReleased
        {
            get
            {
               return (_releasedDate == DateTime.MinValue) ? DateTime.Now : _releasedDate;
            }
            set
            {
                _releasedDate = value;
            }
        }
        [StringLength(255)]

        public string Description { get; set; }

        public int MediaTypeId { get; set; }

        public int CategoryId { get; set; }

        [NotMapped]
        public virtual ICollection<SelectListItem> MediaTypes { get; set; }
        [NotMapped]
        public int ContentId { get; set; }

    }
}
