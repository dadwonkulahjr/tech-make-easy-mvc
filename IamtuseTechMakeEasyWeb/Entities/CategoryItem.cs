using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IamtuseTechMakeEasyWeb.Entities
{
    public class CategoryItem
    {
        public int Id { get; set; }
        [StringLength(200, MinimumLength =2)]
        public string Title { get; set; }
        [Column(TypeName ="date"), Required]

        public DateTime? DateTimeItemReleased { get; set; }

        public int MediaTypeId { get; set; }

        public int CategoryId { get; set; }

    }
}
