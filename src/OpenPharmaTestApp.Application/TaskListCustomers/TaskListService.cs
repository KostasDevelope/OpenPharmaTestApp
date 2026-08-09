using OpenPharmaTestApp.TasksList;
using System;
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
            var totalCount = await _taskListRepository.GetCountSearchAsync(searchInput.Filter, false, cancellationToken);
            var taskListDtos = ObjectMapper.Map<List<TaskList>, List<TaskListDto>>(taskLists);
            return new PagedResultDto<TaskListDto>(totalCount, taskListDtos);
        }
        public async Task<PagedResultDto<TaskListDto>> GetByCustomIdAsync(Guid customerId, CancellationToken cancellationToken)
        {
            var taskLists = await _taskListRepository.GetByCustomIdAsync(customerId, cancellationToken);
            var totalCount = taskLists.Count;
            var taskListDtos = ObjectMapper.Map<List<TaskList>, List<TaskListDto>>(taskLists);
            return new PagedResultDto<TaskListDto>(totalCount, taskListDtos);
        }

        public async Task<TaskListDto> GetAsync(Guid id)
        {
            var taskList = await _taskListRepository.GetAsync(id);
            return taskList != null
                ? ObjectMapper.Map<TaskList, TaskListDto>(taskList)
                : new TaskListDto();
        }

        public async Task<TaskListDto> CreateAsync(CreateTaskListDto model)
        {

            var customerOld = await _taskListRepository.GetByNameAsync(model.Name);

            if (customerOld != null) throw new Exception($"TaskList with name {model.Name} already exists.");

            var taskList = new TaskList(Guid.NewGuid(),
                model.Name,
                model.CustomerId);

            var taskListNew = await _taskListRepository.CreateAsync(taskList);
            return taskListNew != null
                ? ObjectMapper.Map<TaskList, TaskListDto>(taskListNew)
                : new TaskListDto();
        }

        public async Task<TaskListDto> UpdateAsync(UpdateTaskListDto model)
        {
            var taskList = await _taskListRepository.GetAsync(model.Id);

            if (taskList == null) throw new Exception($"TaskList with Id {model.Id} not found.");

            taskList.Name = model.Name;

            var taskListNew = await _taskListRepository.UpdateAsync(taskList);
            return taskListNew != null
                ? ObjectMapper.Map<TaskList, TaskListDto>(taskListNew)
                : new TaskListDto();
        }

        public async Task<TaskListDto> DeleteAsync(Guid id)
        {
            var taskList = await _taskListRepository.GetAsync(id);

            if (taskList == null) throw new Exception($"TaskList with Id {id} not found.");

            var taskListNew = await _taskListRepository.DeleteAsync(taskList);
            return taskListNew != null
                ? ObjectMapper.Map<TaskList, TaskListDto>(taskListNew)
                : new TaskListDto();
        }

    }
}