using Microsoft.Extensions.Configuration;
using OpenPharmaTestApp.TasksList;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace OpenPharmaTestApp.TaskListCustomers
{
    public class CustomerTaskListService : ApplicationService, ICustomerTaskListService
    {
        private readonly ICustomerTaskListRepository _customerTaskListRepository;
        private readonly IConfiguration _configuration;
        public CustomerTaskListService(ICustomerTaskListRepository customerTaskListRepository, IConfiguration configuration)
        {
            _customerTaskListRepository = customerTaskListRepository;
            _configuration = configuration;
        }

        public async Task<CustomerTaskListDto> GetAsync(Guid customerId, Guid taskListId)
        {
            var customerTaskList = await _customerTaskListRepository.GetAsync(customerId, taskListId);

            if (customerTaskList == null)
                throw new UserFriendlyException(
                    message: $"Customer Task List with CustomerId {customerId} and TaskListId {taskListId} not found.",
                    code: "404",
                    details: $"Customer Task List with CustomerId {customerId} and TaskListId {taskListId} not found.");

            return customerTaskList != null
                ? ObjectMapper.Map<CustomerTaskList, CustomerTaskListDto>(customerTaskList)
                : new CustomerTaskListDto();
        }

        public async Task<CustomerTaskListDto> CreateAsync(CreateCustomerTaskListDto model)
        {
            var customerTaskList = await _customerTaskListRepository.GetAsync(model.CustomerId, model.TaskListId);

            if (customerTaskList != null) throw new
                UserFriendlyException(
                    message: $"Customer Task List with CustomerId {model.CustomerId} and TaskListId {model.TaskListId} already exists.",
                    code: "500",
                    details: $"Customer Task List with CustomerId {model.CustomerId} and TaskListId {model.TaskListId} already exists.");


            if (!int.TryParse(_configuration["TaskListSettings:LimitAssign"], out int limitAssign)) limitAssign = 3;

            var assignCount = await _customerTaskListRepository.GetAssignTaskListCountAsync(model.TaskListId);
            if (assignCount >= limitAssign)
                throw new UserFriendlyException(
                    message: $"Task List with TaskListId {model.TaskListId} has reached the maximum number of assignments ({limitAssign}).",
                    code: "500",
                    details: $"Task List with TaskListId {model.TaskListId} has reached the maximum number of assignments ({limitAssign}).");


            customerTaskList = new CustomerTaskList(model.CustomerId, model.TaskListId);

            var customerTaskListNew = await _customerTaskListRepository.CreateAsync(customerTaskList);
            return customerTaskListNew != null
                ? ObjectMapper.Map<CustomerTaskList, CustomerTaskListDto>(customerTaskListNew)
                : new CustomerTaskListDto();
        }

        public async Task<CustomerTaskListDto> DeleteAsync(Guid customerId, Guid taskListId)
        {
            var customerTaskList = await _customerTaskListRepository.GetAsync(customerId, taskListId);

            if (customerTaskList == null) throw new
                UserFriendlyException(
                    message: $"Customer Task List with CustomerId {customerId} and TaskListId {taskListId} not found.",
                    code: "500",
                    details: $"Customer Task List with CustomerId {customerId} and TaskListId {taskListId} not found.");

            await _customerTaskListRepository.DeleteAsync(customerTaskList);
            return customerTaskList != null
                ? ObjectMapper.Map<CustomerTaskList, CustomerTaskListDto>(customerTaskList)
                : new CustomerTaskListDto();
        }

    }
}