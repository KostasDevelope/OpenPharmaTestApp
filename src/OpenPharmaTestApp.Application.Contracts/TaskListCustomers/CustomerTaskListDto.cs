using System;

namespace OpenPharmaTestApp.TaskListCustomers
{
    public class CustomerTaskListDto
    {
        public Guid CustomerId { get; set; }
        public Guid TaskListId { get; set; }
        public virtual DateTime AssignedAt { get; set; }
    }
}
