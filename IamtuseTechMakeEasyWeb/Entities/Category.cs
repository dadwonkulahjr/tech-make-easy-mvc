using System.ComponentModel.DataAnnotations;

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
    }
}
