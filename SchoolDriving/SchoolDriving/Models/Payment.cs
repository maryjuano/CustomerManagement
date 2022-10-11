using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDriving.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }      
        public Schedule? Schedule { get; set; }
        public Guid ScheduleId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
