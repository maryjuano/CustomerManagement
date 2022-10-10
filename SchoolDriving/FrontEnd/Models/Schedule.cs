using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrontEnd.Models
{
    public class Schedule
    {
        public Schedule()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Instructor? Instructor { get; set; }
        public Guid InstructorId { get; set; }
        public Course? Course { get; set; }
        public Guid CourseId { get; set; }
        public Student? Student { get; set; }
        public Guid? StudentId { get; set; }        
    }
}
