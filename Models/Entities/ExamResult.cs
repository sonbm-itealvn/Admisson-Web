using System.ComponentModel.DataAnnotations;

namespace AdmissionWeb.Models.Entities
{
    public class ExamResult : ISoftDelete
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int ApplicationId { get; set; }
        
        [Required]
        [Display(Name = "Môn thi")]
        public string SubjectName { get; set; }
        
        [Range(0, 10, ErrorMessage = "Điểm phải từ 0 đến 10")]
        [Display(Name = "Điểm số")]
        public double Score { get; set; }
        
        [Display(Name = "Ghi chú")]
        public string Remarks { get; set; }

        public virtual Application Application { get; set; }
    }
}
