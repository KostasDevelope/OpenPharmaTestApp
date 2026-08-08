using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace OpenPharmaTestApp.TasksList
{
    public class TaskList : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public virtual ICollection<CustomerTaskList> CustomerTaskLists { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public TaskList(Guid id, string name, Guid customerId) : base(id)
        {
            Name = name;
            CustomerId = customerId;
            CustomerTaskLists = new List<CustomerTaskList>();
        }
    }
}
