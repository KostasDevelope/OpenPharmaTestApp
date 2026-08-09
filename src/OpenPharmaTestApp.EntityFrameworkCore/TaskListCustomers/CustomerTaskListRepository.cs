using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenPharmaTestApp.EntityFrameworkCore;
using OpenPharmaTestApp.TasksList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
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
            return await (await GetDbSetAsync()).FirstOrDefaultAsync(s=> s.CustomerId == customerId && s.TaskListId == taskListId);
        }

        public async Task<int> GetAssignCountAsync(Guid taskListId)
        {
            return await (await GetDbSetAsync()).CountAsync(s => s.TaskListId == taskListId);
        }

        public async Task<CustomerTaskList?> CreateAsync(CustomerTaskList customerTaskList)
        {
            return await InsertAsync(customerTaskList);
        }
        public async Task<CustomerTaskList?> DeleteAsync(CustomerTaskList customerTaskList)
        {
            return await DeleteAsync(customerTaskList);
        }

    }
}
