using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OpenPharmaTestApp.TasksList
{
    public interface ICustomerRepository : IBasicRepository<Customer, Guid>
    {
        Task<List<Customer>> SearchAsync(string filter, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, bool isDeleted = false, CancellationToken cancellationToken = default);
        Task<int> GetCountSearchAsync(string filter, bool isDeleted = false, CancellationToken cancellationToken = default);
        Task<Customer?> GetAsync(Guid id);
        Task<Customer?> CreateAsync(Customer taskList);
        Task<Customer?> UpdateAsync(Customer taskList);
        Task<Customer?> DeleteAsync(Customer taskList);
        Task<Customer?> GetByNameAsync(string name);
    }
}
