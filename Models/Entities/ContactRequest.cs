using System;
using System.ComponentModel.DataAnnotations;

namespace AdmissionWeb.Models.Entities
{
    public class ContactRequest : ISoftDelete
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [Display(Name = "Họ tên")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "Tiêu đề")]
        public string Subject { get; set; }
        
        [Required(ErrorMessage = "Nội dung không được để trống")]
        [Display(Name = "Nội dung")]
        public string Message { get; set; }
        
        [Display(Name = "Ngày gửi")]
        public DateTime CreatedAt { get; set; }
        
        [Display(Name = "Đã đọc")]
        public bool IsRead { get; set; }
    }
}
