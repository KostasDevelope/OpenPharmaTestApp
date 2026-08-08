using Microsoft.EntityFrameworkCore;
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
        public TaskListRepository(IDbContextProvider<OpenPharmaTestAppDbContext> dbContextProvider) : base(dbContextProvider)
        {
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

    }
}
