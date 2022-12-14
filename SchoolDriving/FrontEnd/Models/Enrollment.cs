using FrontEnd.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrontEnd.Models
{
    public class Enrollment
    {
        public Enrollment()
        {
            Requirements = new HashSet<Requirements>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Reference { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string StreetNumber { get; set; }
        public string Barangay { get; set; }
        public string Municipality { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string CivilStatus { get; set; }
        public string? LicenseNumber { get; set; }
        public string EducationalAttainment { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public Course? Course { get; set; }
        public Guid CourseId { get; set; }
        public Schedule? Schedule { get; set; }
        public Guid? ScheduleId { get; set; }
        public Payment? Payment { get; set; }
        public Guid? PaymentId { get; set; }
        public bool Approved { get; set; }
        public ICollection<Requirements> Requirements { get; set; }
    }
}
