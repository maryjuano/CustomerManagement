﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDriving.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? PaymentReference { get; set; }
        public bool IsPaid { get; set; }
        public decimal TotalPrice { get; set; }
        public Student? Student { get; set; }
        public Guid StudentId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public Guid OrderId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
