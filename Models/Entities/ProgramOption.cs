using System.ComponentModel.DataAnnotations;

namespace AdmissionWeb.Models.Entities
{
    public class ProgramOption : ISoftDelete
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        
        [Required(ErrorMessage = "Tên ngành/lớp không được để trống")]
        [Display(Name = "Tên ngành/lớp")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Mã ngành không được để trống")]
        [Display(Name = "Mã ngành")]
        public string Code { get; set; }
        
        [Display(Name = "Chỉ tiêu")]
        public int Quota { get; set; }
        
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        
        [Display(Name = "Lớp chuyên")]
        public bool IsSpecialized { get; set; }
    }
}
