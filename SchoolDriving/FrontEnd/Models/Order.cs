using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models;

public class Order
{
    public Order()
    {
        OrderItems = new HashSet<OrderItem>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string OrderReference { get; set; }
    public decimal TotalPrice { get; set; }
    public Enrollment? Enrollment { get; set; }
    public Guid EnrollmentId { get; set; }
    public Student? Student { get; set; }
    public Guid StudentId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastModified { get; set; } 
    public Payment? Payment { get; set; }
    public Guid PaymentId { get; set; }
    public Guid? InvoiceId { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string CourseName { get; set; }
    [Range(1, 100)]
    public int Quantity { get; set; }
    public bool Professional { get; set; }
    public decimal ProductPrice { get; set; }
    public Guid OrderId { get; set; }
}


