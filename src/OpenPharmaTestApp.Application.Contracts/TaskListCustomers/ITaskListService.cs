using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace OpenPharmaTestApp.TaskListCustomers
{
    public interface ITaskListService : ITransientDependency
    {
        Task<PagedResultDto<TaskListDto>> SearchAsync(SearchInput searchInput, CancellationToken cancellationToken);
    }
}
