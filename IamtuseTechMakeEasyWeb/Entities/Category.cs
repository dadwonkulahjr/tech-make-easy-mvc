using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IamtuseTechMakeEasyWeb.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        public string ThumbnailImagePath { get; set; }
        [StringLength(255)]
        public string Description { get; set; }


        //Nagivation Properties
        [ForeignKey("CategoryId")]
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ICollection<UserCategory> UserCategories { get; set; }
    }
}
