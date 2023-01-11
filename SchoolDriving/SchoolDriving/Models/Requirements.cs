namespace SchoolDriving.Models
{
    public class Requirements
    {
        public Guid Id { get; set; }
        public Guid EnrollmentId { get; set; }

        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
    }
}
