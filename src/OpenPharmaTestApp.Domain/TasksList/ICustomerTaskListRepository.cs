using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OpenPharmaTestApp.TasksList
{
    public interface ICustomerTaskListRepository : IBasicRepository<CustomerTaskList>
    {
        Task<CustomerTaskList?> GetAsync(Guid customerId, Guid taskListId);
        Task<CustomerTaskList?> CreateAsync(CustomerTaskList taskList);
        Task<CustomerTaskList?> DeleteAsync(CustomerTaskList taskList);
        Task<int> GetAssignCountAsync(Guid taskListId);
    }
}
