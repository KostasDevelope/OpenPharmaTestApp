using OpenPharmaTestApp.TasksList;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OpenPharmaTestApp.TaskListCustomers
{
    public class CustomerService : ApplicationService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerTaskListRepository _customerTaskListRepository;
        public CustomerService(ICustomerRepository customerRepository,
            ICustomerTaskListRepository customerTaskListRepository)
        {
            _customerTaskListRepository = customerTaskListRepository;
            _customerRepository = customerRepository;
        }
        public async Task<PagedResultDto<CustomerDto>> SearchAsync(SearchInput searchInput, CancellationToken cancellationToken)
        {
            var taskLists = await _customerRepository.SearchAsync(
                searchInput.Filter,
                searchInput.Sorting,
                searchInput.MaxResultCount,
                searchInput.SkipCount,
                false,
                cancellationToken
            );
            var totalCount = await _customerRepository.GetCountSearchAsync(searchInput.Filter, false, cancellationToken);
            var customerDtos = ObjectMapper.Map<List<Customer>, List<CustomerDto>>(taskLists);
            return new PagedResultDto<CustomerDto>(totalCount, customerDtos);
        }

        public async Task<CustomerDto> GetAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(id);
            return customer != null
                ? ObjectMapper.Map<Customer, CustomerDto>(customer)
                : new CustomerDto();
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerDto model)
        {
            var customerOld = await _customerRepository.GetByNameAsync(model.Name);

            if (customerOld != null) throw new UserFriendlyException(
                message: $"Customer with name {model.Name} already exists.",
                code: "500",
                details: $"Customer with name {model.Name} already exists.");

            var customer = new Customer(Guid.NewGuid(), model.Name);

            var customerNew = await _customerRepository.CreateAsync(customer);
            return customerNew != null
                ? ObjectMapper.Map<Customer, CustomerDto>(customerNew)
                : new CustomerDto();
        }

        public async Task<CustomerDto> UpdateAsync(UpdateCustomerDto model)
        {
            var customer = await _customerRepository.GetAsync(model.Id);

            if (customer == null) throw new UserFriendlyException(
                message: $"Customer with Id {model.Id} not found.",
                code: "500",
                details: $"Customer with Id {model.Id} not found.");

            customer.Name = model.Name;

            var customerNew = await _customerRepository.UpdateAsync(customer);

            return customerNew != null
                ? ObjectMapper.Map<Customer, CustomerDto>(customerNew)
                : new CustomerDto();
        }

        public async Task<CustomerDto> DeleteAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(id);

            if (customer == null) throw new UserFriendlyException(
                 message: $"Customer with Id {id} not found."
                 , code: "500"
                , details: $"Customer with Id {id} not found.");


            var assignCount = await _customerTaskListRepository.GetAssignCustomerCountAsync(id);

            if (assignCount > 0)
                throw new UserFriendlyException(
                    message: $"Customer with Id {id} is assigned to {assignCount} task lists and cannot be deleted."
                    , code: "500"
                    , details: $"Customer with Id {id} is assigned to {assignCount} task lists and cannot be deleted.");

            await _customerRepository.DeleteAsync(customer);
            return customer != null
                ? ObjectMapper.Map<Customer, CustomerDto>(customer)
                : new CustomerDto();
        }

    }
}