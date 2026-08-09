using System;
using System.ComponentModel.DataAnnotations;

namespace OpenPharmaTestApp.TaskListCustomers
{
    [Serializable]
    public class CreateTaskListDto
    {
        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string? Name { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
    }
}
