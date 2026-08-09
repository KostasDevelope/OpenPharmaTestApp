using System;
using System.ComponentModel.DataAnnotations;

namespace OpenPharmaTestApp.TaskListCustomers
{
    [Serializable]
    public class CreateCustomerDto
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string? Name { get; set; }
    }
}
