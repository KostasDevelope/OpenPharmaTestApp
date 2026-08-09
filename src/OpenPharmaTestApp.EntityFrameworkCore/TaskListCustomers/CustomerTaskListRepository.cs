using Microsoft.EntityFrameworkCore;
using OpenPharmaTestApp.EntityFrameworkCore;
using OpenPharmaTestApp.TasksList;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OpenPharmaTestApp.TaskListCustomers
{
    public class CustomerTaskListRepository : EfCoreRepository<OpenPharmaTestAppDbContext, CustomerTaskList>, ICustomerTaskListRepository
    {
        public CustomerTaskListRepository(IDbContextProvider<OpenPharmaTestAppDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<CustomerTaskList?> GetAsync(Guid customerId, Guid taskListId)
        {
            return await (await GetDbSetAsync()).FirstOrDefaultAsync(s => s.CustomerId == customerId && s.TaskListId == taskListId);
        }

        public async Task<int> GetAssignTaskListCountAsync(Guid taskListId)
        {
            return await (await GetDbSetAsync()).CountAsync(s => s.TaskListId == taskListId);
        }

        public async Task<int> GetAssignCustomerCountAsync(Guid customerId)
        {
            return await (await GetDbSetAsync()).CountAsync(s => s.CustomerId == customerId);
        }

        public async Task<CustomerTaskList?> CreateAsync(CustomerTaskList customerTaskList)
        {
            return await InsertAsync(customerTaskList);
        }
        public async Task DeleteAsync(CustomerTaskList customerTaskList)
        {
            await base.DeleteAsync(customerTaskList);
        }

    }
}
