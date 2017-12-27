using System;
using System.ComponentModel.DataAnnotations;

namespace MultiTenants.Data.Models
{
    public class Value
    {
        [Key]
        public Guid Id { get; set; }

        public string Content { get; set; }
    }
}
