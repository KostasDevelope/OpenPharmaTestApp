using OpenPharmaTestApp.TaskListCustomers;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Modularity;
using Xunit;

namespace OpenPharmaTestApp.Samples
{
    public abstract class CustomerService_Tests<TStartupModule> : OpenPharmaTestAppApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly ICustomerService _customerService;

        protected CustomerService_Tests()
        {
            _customerService = GetRequiredService<ICustomerService>();
        }

        [Fact]
        public async Task Should_Create_A_Valid_Customer()
        {
            // Arrange
            var input = new CreateCustomerDto
            {
                Name = "Test Open Pharma Company Customer"
            };

            // Act
            var result = await _customerService.CreateAsync(input);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe(input.Name);
        }

        [Fact]
        public async Task Should_Not_Create_Customer_With_Duplicate_Name()
        {
            // Arrange
            var name = "Unique Customer Name";
            await _customerService.CreateAsync(new CreateCustomerDto { Name = name });

            // Act & Assert
            var exception = await Should.ThrowAsync<UserFriendlyException>(async () =>
            {
                await _customerService.CreateAsync(new CreateCustomerDto { Name = name });
            });

            exception.Message.ShouldContain($"Customer with name {name} already exists.");
        }

        [Fact]
        public async Task Should_Get_Customer_By_Id()
        {
            // Arrange
            var created = await _customerService.CreateAsync(new CreateCustomerDto { Name = "Customer For Get" });

            // Act
            var result = await _customerService.GetAsync(created.Id);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(created.Id);
            result.Name.ShouldBe("Customer For Get");
        }

        [Fact]
        public async Task Should_Search_Customers()
        {
            // Arrange
            await _customerService.CreateAsync(new CreateCustomerDto { Name = "Alpha Pharmacy" });
            await _customerService.CreateAsync(new CreateCustomerDto { Name = "Beta Lab" });

            var searchInput = new SearchInput
            {
                Filter = "Alpha",
                MaxResultCount = 10,
                SkipCount = 0
            };

            // Act
            var result = await _customerService.SearchAsync(searchInput, CancellationToken.None);

            // Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(c => c.Name.Contains("Alpha"));
        }

        [Fact]
        public async Task Should_Update_Customer()
        {
            // Arrange
            var created = await _customerService.CreateAsync(new CreateCustomerDto { Name = "Original Name" });

            var updateInput = new UpdateCustomerDto
            {
                Id = created.Id,
                Name = "Updated Name"
            };

            // Act
            var result = await _customerService.UpdateAsync(updateInput);

            // Assert
            result.ShouldNotBeNull();
            result.Name.ShouldBe("Updated Name");
        }

        [Fact]
        public async Task Should_Throw_Exception_On_Update_Non_Existing_Customer()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            var updateInput = new UpdateCustomerDto
            {
                Id = nonExistingId,
                Name = "Non Existing"
            };

            // Act & Assert
            var exception = await Should.ThrowAsync<UserFriendlyException>(async () =>
            {
                await _customerService.UpdateAsync(updateInput);
            });

            exception.Message.ShouldContain($"Customer with Id {nonExistingId} not found.");
        }

        [Fact]
        public async Task Should_Delete_Customer()
        {
            // Arrange
            var created = await _customerService.CreateAsync(new CreateCustomerDto { Name = "Customer To Delete" });

            // Act
            var deletedCustomer = await _customerService.DeleteAsync(created.Id);

            // Assert
            deletedCustomer.ShouldNotBeNull();
            deletedCustomer.Id.ShouldBe(created.Id);
        }

        [Fact]
        public async Task Should_Throw_Exception_On_Delete_Non_Existing_Customer()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act & Assert
            var exception = await Should.ThrowAsync<UserFriendlyException>(async () =>
            {
                await _customerService.DeleteAsync(nonExistingId);
            });

            exception.Message.ShouldContain($"Customer with Id {nonExistingId} not found.");
        }
    }
}
