using System;
using Volo.Abp.Domain.Entities;


namespace OpenPharmaTestApp.TasksList
{
    public class CustomerTaskList : Entity
    {
        public Guid CustomerId { get; set; }
        public Guid TaskListId { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        public Customer Customer { get; set; }
        public TaskList TaskList { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { CustomerId, TaskListId };
        }

        public CustomerTaskList(Guid customerId, Guid taskListId)
        {
            CustomerId = customerId;
            TaskListId = taskListId;
        }
    }
}
