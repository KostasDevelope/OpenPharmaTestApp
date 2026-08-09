using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace OpenPharmaTestApp.TasksList
{
    public class CustomerTaskListDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Customer, Guid> _customerRepository;
        private readonly IRepository<TaskList, Guid> _taskListRepository;
        private readonly IRepository<CustomerTaskList> _customerTaskListRepository;

        public CustomerTaskListDataSeedContributor(
            IRepository<Customer, Guid> customerRepository,
            IRepository<TaskList, Guid> taskListRepository,
            IRepository<CustomerTaskList> customerTaskListRepository)
        {
            _customerRepository = customerRepository;
            _taskListRepository = taskListRepository;
            _customerTaskListRepository = customerTaskListRepository;
        }


        private async Task<Customer> SeedCustomer(Customer customer)
        {
            var customerIs = await _customerRepository.FirstOrDefaultAsync(s => s.Name.ToLower() == customer.Name.ToLower());
            if (customerIs == null) customerIs = await _customerRepository.InsertAsync(customer);
            return customerIs;
        }

        private async Task<TaskList> SeedTaskList(TaskList taskList)
        {
            var taskListIs = await _taskListRepository.FirstOrDefaultAsync(s => s.Name.ToLower() == taskList.Name.ToLower());
            if (taskListIs == null) taskListIs = await _taskListRepository.InsertAsync(taskList);
            return taskListIs;
        }

        private async Task<CustomerTaskList> SeedCustomerTaskList(Customer customer, TaskList taskList)
        {
            var customerTaskList = await _customerTaskListRepository.FirstOrDefaultAsync(s => s.CustomerId == customer.Id && s.TaskListId == taskList.Id);
            if (customerTaskList == null) customerTaskList = await _customerTaskListRepository.InsertAsync(new CustomerTaskList(customer.Id, taskList.Id));
            return customerTaskList;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var customers = new List<Customer> {
            await SeedCustomer(new Customer(
                id: Guid.NewGuid(),
                name: "alex.smith89@gmail.com"
            )),

            await SeedCustomer(new Customer(
                id: Guid.NewGuid(),
                name: "maria.ivanova@yandex.com"
            )),

            await SeedCustomer(new Customer(
                id: Guid.NewGuid(),
                name: "dev.dmitry@outlook.com"
            )),

            await SeedCustomer(new Customer(
                id: Guid.NewGuid(),
                name: "elena.petrova@mail.com"
            )),

            await SeedCustomer(new Customer(
                id: Guid.NewGuid(),
                name: "sergey.k@proton.me"
            )),

             await SeedCustomer(new Customer(
                id: Guid.NewGuid(),
                name: "maria.ivanova@yandex.com"
            )),

             await SeedCustomer(new Customer(
               id: Guid.NewGuid(),
               name: "tech.support@companytest.org"
           )),

            await SeedCustomer(new Customer(
               id: Guid.NewGuid(),
               name: "max.volkov@icloud.com"
           )),

            await SeedCustomer(new Customer(
               id: Guid.NewGuid(),
               name: "olga.sidorova@example.com"
           )),

            await SeedCustomer(new Customer(
               id: Guid.NewGuid(),
               name: "test.user2026@testmail.io"
           )) };

            var taskLists = new List<TaskList> {

             await SeedTaskList(new TaskList(
                id: Guid.NewGuid(),
                name: "Review authorization module code",
                customerId: customers[0].Id
            )),

             await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Update REST API documentation",
                   customerId: customers[0].Id
                )),

              await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Fix filter display bug in the catalog",
                   customerId: customers[0].Id
                )),

               await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Prepare Q3 sales report",
                   customerId: customers[0].Id
                )),

                await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Set up CI/CD pipeline for the staging environment",
                   customerId: customers[1].Id
                )),

                await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Approve mobile app design mockups",
                   customerId: customers[1].Id
                )),

                  await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Optimize SQL queries in the notification service",
                   customerId: customers[1].Id
                )),

                 await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Conduct interview for the Frontend Developer position",
                   customerId: customers[2].Id
                )),

                await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Prepare presentation for the client demo",
                   customerId: customers[2].Id
                )),
                 await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Update dependency versions and NuGet packages",
                   customerId: customers[2].Id
                )),

                await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Write unit tests for the payment module",
                   customerId: customers[3].Id
                )),
                await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Configure monitoring and alerts in Grafana",
                   customerId: customers[4].Id
                )),
                await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Draft technical specification for CRM integration",
                   customerId: customers[5].Id
                )),
                await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Perform web application security audit",
                   customerId: customers[6].Id
                )),             
                await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Localize system interface into English",
                   customerId: customers[7].Id
                )),                       
                await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Order new hardware for the development team",
                   customerId: customers[8].Id
                )),                          
                await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Design database schema for the analytics module",
                   customerId: customers[9].Id
                )),

                await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Refactor payment processing service",
                   customerId: customers[9].Id
                )),
                await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Organize team retrospective meeting",
                   customerId: customers[9].Id
                )),
                await SeedTaskList(new TaskList(
                   id: Guid.NewGuid(),
                   name: "Review and update backlog tickets in task tracker",
                   customerId: customers[9].Id
                ))
            };

            await SeedCustomerTaskList(customers[0], taskLists[0]);
            await SeedCustomerTaskList(customers[0], taskLists[1]);
            await SeedCustomerTaskList(customers[0], taskLists[2]);
            await SeedCustomerTaskList(customers[1], taskLists[3]);
            await SeedCustomerTaskList(customers[1], taskLists[4]);
            await SeedCustomerTaskList(customers[2], taskLists[5]);
            await SeedCustomerTaskList(customers[2], taskLists[6]);
            await SeedCustomerTaskList(customers[3], taskLists[7]);
            await SeedCustomerTaskList(customers[4], taskLists[8]);
            await SeedCustomerTaskList(customers[5], taskLists[9]);
            await SeedCustomerTaskList(customers[6], taskLists[10]);
        }

    }
}
