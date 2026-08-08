using System;
using Volo.Abp.Application.Dtos;

namespace OpenPharmaTestApp.TaskListCustomers
{
    [Serializable]
    public class SearchInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
