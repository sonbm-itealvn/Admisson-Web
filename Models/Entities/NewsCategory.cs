using System.ComponentModel.DataAnnotations;

namespace AdmissionWeb.Models.Entities
{
    public class NewsCategory : ISoftDelete
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }
    }
}
