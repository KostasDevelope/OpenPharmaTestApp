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
    public class TaskListRepository : EfCoreRepository<OpenPharmaTestAppDbContext, TaskList, Guid>, ITaskListRepository
    {
        private readonly IConfiguration _configuration;
        public TaskListRepository(IDbContextProvider<OpenPharmaTestAppDbContext> dbContextProvider,
            IConfiguration configuration) : base(dbContextProvider)
        {
            _configuration = configuration;
        }

        public async Task<List<TaskList>> SearchAsync(string filter,
          string? sorting = "desc",
          int maxResultCount = 10,
          int skipCount = 0,
          bool isDeleted = false,
          CancellationToken cancellationToken = default)
        {
            var taskLists = await (await GetDbSetAsync())
                .WhereIf(!string.IsNullOrEmpty(filter), p => p.Name.Contains(filter))
                .Where(p => p.IsDeleted == isDeleted)
                .OrderBy($"creationTime {(sorting.IsNullOrEmpty() ? "desc" : sorting)}" )
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            return taskLists;
        }

        public async Task<int> GetCountSearchAsync(string filter, bool isDeleted = false, CancellationToken cancellationToken = default)
        {
            var count = await (await GetDbSetAsync())
                 .WhereIf(!string.IsNullOrEmpty(filter), p => p.Name.Contains(filter))
                 .Where(p => p.IsDeleted == isDeleted)          
                 .CountAsync(GetCancellationToken(cancellationToken));

            return count;
        }

        public async Task<List<TaskList>> GetByCustomIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Include(s => s.CustomerTaskLists)
                .Where(s => s.CustomerId == customerId 
                || s.CustomerTaskLists.Select(o=>o.CustomerId).Contains(customerId))
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<TaskList?> GetAsync(Guid id)
        {
            return await (await GetDbSetAsync()).FirstOrDefaultAsync(s=> s.Id == id);
        }

        public async Task<TaskList?> CreateAsync(TaskList taskList)
        {
            return await InsertAsync(taskList);
        }

        public async Task<TaskList?> UpdateAsync(TaskList taskList)
        {
            return await UpdateAsync(taskList);
        }

        public async Task<TaskList?> DeleteAsync(TaskList taskList)
        {
            return await DeleteAsync(taskList);
        }
    }
}
