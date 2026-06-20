using System;
using System.ComponentModel.DataAnnotations;

namespace AdmissionWeb.Models.Entities
{
    public class NewsArticle : ISoftDelete
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Nội dung không được để trống")]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        
        [Display(Name = "Tác giả")]
        public string? Author { get; set; }
        
        [Display(Name = "Ngày đăng")]
        public DateTime PublishedAt { get; set; }
        
        [Display(Name = "Danh mục")]
        public string? Category { get; set; }
        
        [Display(Name = "Hình ảnh")]
        public string? ImageUrl { get; set; }
        
        [Display(Name = "Công khai")]
        public bool IsPublished { get; set; }
    }
}
