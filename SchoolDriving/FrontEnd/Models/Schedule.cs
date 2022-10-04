using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrontEnd.Models
{
    public class Schedule
    {
        public Schedule()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }      
        public Instructor Instructor { get; set; }
        public Guid InstructorId { get; set; }
        public Course Course { get; set; }
        public Guid CourseId { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
