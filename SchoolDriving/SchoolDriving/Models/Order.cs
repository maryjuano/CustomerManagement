using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolDriving.Models;

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
    public Student? Student { get; set; }
    public Guid StudentId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastModified { get; set; }

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
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; }
    public Guid OrderId { get; set; }
}


