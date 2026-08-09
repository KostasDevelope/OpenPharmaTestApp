using Microsoft.Extensions.Configuration;
using OpenPharmaTestApp.TasksList;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace OpenPharmaTestApp.TaskListCustomers
{
    public class CustomerTaskListService : ApplicationService, ICustomerTaskListService
    {
        private readonly ICustomerTaskListRepository _customerTaskListRepository;
        private readonly IConfiguration _configuration;
        public CustomerTaskListService(ICustomerTaskListRepository customerRepository, IConfiguration configuration)
        {
            _customerTaskListRepository = customerRepository;
            _configuration = configuration;
        }

        public async Task<CustomerTaskListDto> GetAsync(Guid customerId, Guid taskListId)
        {
            var customerTaskList = await _customerTaskListRepository.GetAsync(customerId, taskListId);
            return customerTaskList != null
                ? ObjectMapper.Map<CustomerTaskList, CustomerTaskListDto>(customerTaskList)
                : new CustomerTaskListDto();
        }

        public async Task<CustomerTaskListDto> CreateAsync(CreateCustomerTaskListDto model)
        {
            var customerTaskList = await _customerTaskListRepository.GetAsync(model.CustomerId, model.TaskListId);

            if (customerTaskList != null) throw new Exception($"Customer Task List with CustomerId {model.CustomerId} and TaskListId {model.TaskListId} already exists.");

            //int limitAssign = 3;
            int.TryParse(_configuration["TaskListSettings:LimitAssign"], out int limitAssign);

            var assignCount = await _customerTaskListRepository.GetAssignCountAsync(model.CustomerId);
            if (assignCount >= limitAssign) throw new Exception($"Customer with Id {model.CustomerId} has reached the maximum number of assigned task lists ({limitAssign}).");

            customerTaskList = new CustomerTaskList(model.CustomerId, model.CustomerId);

            var customerTaskListNew = await _customerTaskListRepository.CreateAsync(customerTaskList);
            return customerTaskListNew != null
                ? ObjectMapper.Map<CustomerTaskList, CustomerTaskListDto>(customerTaskListNew)
                : new CustomerTaskListDto();
        }

        public async Task<CustomerTaskListDto> DeleteAsync(Guid customerId, Guid taskListId)
        {
            var customerTaskList = await _customerTaskListRepository.GetAsync(customerId, taskListId);

            if (customerTaskList != null) throw new Exception($"Customer Task List with CustomerId {customerId} and TaskListId {taskListId} already exists.");


            customerTaskList = await _customerTaskListRepository.DeleteAsync(customerTaskList);
            return customerTaskList != null
                ? ObjectMapper.Map<CustomerTaskList, CustomerTaskListDto>(customerTaskList)
                : new CustomerTaskListDto();
        }

    }
}