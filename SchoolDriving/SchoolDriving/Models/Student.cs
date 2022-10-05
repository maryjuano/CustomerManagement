using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolDriving.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
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
    }

    public enum EmploymentStatus
    {
        Casual = 1,
        JobOrder = 2,
        Probationary = 3,
        Permanent = 4,
        SelfEmployed = 5,
        OFW
    }
}
