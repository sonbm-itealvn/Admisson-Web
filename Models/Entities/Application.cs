using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdmissionWeb.Models.Entities
{
    public class Application : ISoftDelete
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        
        [Display(Name = "Mã hồ sơ")]
        public string ApplicationCode { get; set; }
        
        public string UserId { get; set; }
        public int AdmissionPeriodId { get; set; }
        public int ProgramOptionId { get; set; }
        
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }
        
        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        [Display(Name = "Giới tính")]
        public string Gender { get; set; }
        
        [Required(ErrorMessage = "Số CCCD không được để trống")]
        [Display(Name = "Số CCCD")]
        public string IDNumber { get; set; }
        
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        
        [Display(Name = "Trường THCS")]
        public string PreviousSchool { get; set; }
        
        [Display(Name = "Trạng thái")]
        public string Status { get; set; } // Pending, Approved, Rejected
        
        [Display(Name = "Lý do từ chối")]
        public string RejectionReason { get; set; }
        
        [Display(Name = "Ngày nộp")]
        public DateTime SubmittedAt { get; set; }
        
        [Display(Name = "Tài liệu đính kèm")]
        public string DocumentUrls { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual AdmissionPeriod AdmissionPeriod { get; set; }
        public virtual ProgramOption ProgramOption { get; set; }
        public virtual ICollection<ExamResult> ExamResults { get; set; }
    }
}
