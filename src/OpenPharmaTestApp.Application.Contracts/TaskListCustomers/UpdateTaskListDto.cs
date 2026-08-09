using System;
using System.ComponentModel.DataAnnotations;

namespace OpenPharmaTestApp.TaskListCustomers
{
    [Serializable]
    public class UpdateTaskListDto
    {
        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string? Name { get; set; }
        [Required]
        public Guid Id { get; set; }
    }
}
