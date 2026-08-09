using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace OpenPharmaTestApp.TaskListCustomers
{
    public interface ITaskListService : ITransientDependency
    {
        Task<PagedResultDto<TaskListDto>> SearchAsync(SearchInput searchInput, CancellationToken cancellationToken);
        Task<PagedResultDto<TaskListDto>> GetByCustomIdAsync(Guid customerId, CancellationToken cancellationToken);
        Task<TaskListDto> GetAsync(Guid id);
        Task<TaskListDto> CreateAsync(CreateTaskListDto model);
        Task<TaskListDto> UpdateAsync(UpdateTaskListDto model);
        Task<TaskListDto> DeleteAsync(Guid id);
    }
}
