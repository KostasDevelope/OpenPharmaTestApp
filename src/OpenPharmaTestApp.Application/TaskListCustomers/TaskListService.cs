using OpenPharmaTestApp.TasksList;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OpenPharmaTestApp.TaskListCustomers
{
    public class TaskListService : ApplicationService, ITaskListService
    {
        private readonly ITaskListRepository _taskListRepository;
        public TaskListService(ITaskListRepository taskListRepository)
        {
            _taskListRepository = taskListRepository;
        }
        public async Task<PagedResultDto<TaskListDto>> SearchAsync(SearchInput searchInput, CancellationToken cancellationToken)
        {
            var taskLists = await _taskListRepository.SearchAsync(
                searchInput.Filter,
                searchInput.Sorting,
                searchInput.MaxResultCount,
                searchInput.SkipCount,
                false,
                cancellationToken
            );
            var totalCount = await _taskListRepository.GetCountSearchAsync(searchInput.Filter,false,cancellationToken);
            var taskListDtos = ObjectMapper.Map<List<TaskList>, List<TaskListDto>>(taskLists);
            return new PagedResultDto<TaskListDto>(totalCount, taskListDtos);
        }

    }
}
