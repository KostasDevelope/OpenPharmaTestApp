using System;
using System.ComponentModel.DataAnnotations;

namespace OpenPharmaTestApp.TaskListCustomers
{
    [Serializable]
    public class CreateCustomerTaskListDto
    {
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public Guid TaskListId { get; set; }
    }
}
