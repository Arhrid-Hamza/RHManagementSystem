using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHManagementSystem.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [ForeignKey("DepartmentResponsibleNavigation")]
        public int DepartmentResponsible { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [ForeignKey("EmployeeResponsibleNavigation")]
        public long EmployeeResponsible { get; set; }

        // Navigation properties
        public Department? DepartmentResponsibleNavigation { get; set; }
        public Employee? EmployeeResponsibleNavigation { get; set; }
    }
}
