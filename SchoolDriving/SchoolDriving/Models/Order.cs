﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolDriving.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public Student Student { get; set; }
    public Guid StudentId { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }  
    public decimal ProductPrice { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}
