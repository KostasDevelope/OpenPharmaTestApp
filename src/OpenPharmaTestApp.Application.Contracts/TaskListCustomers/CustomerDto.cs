using System;

namespace OpenPharmaTestApp.TaskListCustomers
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public virtual DateTime CreationTime { get; set; }
    }
}
