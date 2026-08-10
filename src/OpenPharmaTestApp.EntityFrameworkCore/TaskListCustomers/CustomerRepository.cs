using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    public class CustomerRepository : EfCoreRepository<OpenPharmaTestAppDbContext, Customer, Guid>, ICustomerRepository
    {
        private readonly IConfiguration _configuration;
        public CustomerRepository(IDbContextProvider<OpenPharmaTestAppDbContext> dbContextProvider,
            IConfiguration configuration) : base(dbContextProvider)
        {
            _configuration = configuration;
        }

        public async Task<List<Customer>> SearchAsync(string filter,
          string? sorting = "desc",
          int maxResultCount = 10,
          int skipCount = 0,
          bool isDeleted = false,
          CancellationToken cancellationToken = default)
        {
            var taskLists = await (await GetDbSetAsync())
                .WhereIf(!string.IsNullOrEmpty(filter), p => p.Name.Contains(filter))
                .Where(p => p.IsDeleted == isDeleted)
                .OrderBy($"creationTime {(sorting.IsNullOrEmpty() ? "desc" : sorting)}")
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


        public async Task<Customer?> GetAsync(Guid id)
        {
            return await (await GetDbSetAsync()).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Customer?> CreateAsync(Customer customer)
        {
            return await InsertAsync(customer, autoSave: true);
        }

        public async Task<Customer?> UpdateAsync(Customer customer)
        {
            return await base.UpdateAsync(customer);
        }

        public async Task DeleteAsync(Customer customer)
        {
            await base.DeleteAsync(customer);
        }

        public async Task<Customer?> GetByNameAsync(string name)
        {
            return await (await GetDbSetAsync())
                .FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
        }
    }
}
