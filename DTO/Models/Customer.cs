using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
