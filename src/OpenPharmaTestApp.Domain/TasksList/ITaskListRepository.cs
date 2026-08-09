using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OpenPharmaTestApp.TasksList
{
    public interface ITaskListRepository : IBasicRepository<TaskList, Guid>
    {
        Task<List<TaskList>> SearchAsync(string filter, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, bool isDeleted = false, CancellationToken cancellationToken = default);
        Task<int> GetCountSearchAsync(string filter, bool isDeleted = false, CancellationToken cancellationToken = default);
        Task<List<TaskList>> GetByCustomIdAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task<TaskList?> GetAsync(Guid id);
        Task<TaskList?> CreateAsync(TaskList taskList);
        Task<TaskList?> UpdateAsync(TaskList taskList);
        Task DeleteAsync(TaskList taskList);
        Task<TaskList?> GetByNameAsync(string name);
    }
}
