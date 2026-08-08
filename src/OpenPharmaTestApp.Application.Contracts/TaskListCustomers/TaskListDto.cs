using System;

namespace OpenPharmaTestApp.TaskListCustomers
{
    public class TaskListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CretedById { get; set; }
        public virtual DateTime CreationTime { get; set; }
    }
}
