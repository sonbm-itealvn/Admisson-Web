using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdmissionWeb.Models.Entities
{
    public class AdmissionPeriod : ISoftDelete
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        
        [Required(ErrorMessage = "Tên kỳ tuyển sinh không được để trống")]
        [Display(Name = "Tên kỳ tuyển sinh")]
        public string Name { get; set; }
        
        [Display(Name = "Năm học")]
        public int Year { get; set; }
        
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime StartDate { get; set; }
        
        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        [Display(Name = "Ngày kết thúc")]
        public DateTime EndDate { get; set; }
        
        [Display(Name = "Đang hoạt động")]
        public bool IsActive { get; set; }
        
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
    }
}
