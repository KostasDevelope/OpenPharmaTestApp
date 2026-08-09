using System;
using System.ComponentModel.DataAnnotations;

namespace OpenPharmaTestApp.TaskListCustomers
{
    [Serializable]
    public class UpdateCustomerDto
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string? Name { get; set; }
        [Required]
        public Guid Id { get; set; }
    }
}
