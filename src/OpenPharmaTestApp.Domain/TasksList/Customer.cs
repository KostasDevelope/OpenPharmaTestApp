using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace OpenPharmaTestApp.TasksList
{
    public class Customer : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; } = default!;
        public virtual ICollection<CustomerTaskList> CustomerTaskLists { get; set; }
        public virtual ICollection<TaskList> TaskLists { get; set; }
        public Customer(Guid id, string name) : base(id)
        {
            CustomerTaskLists = new List<CustomerTaskList>();
            TaskLists = new List<TaskList>();
            Name = name;
        }
    }
}
