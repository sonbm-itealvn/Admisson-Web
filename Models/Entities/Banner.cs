using System;
using System.ComponentModel.DataAnnotations;

namespace AdmissionWeb.Models.Entities
{
    public class Banner : ISoftDelete
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
        [StringLength(200)]
        public string Title { get; set; }

        public string? ImageUrl { get; set; }

        [StringLength(500)]
        public string? LinkUrl { get; set; }

        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }
    }
}
