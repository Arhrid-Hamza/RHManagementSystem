using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHManagementSystem.Models
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string GeneratedBy { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        public string? Content { get; set; }
    }
}
