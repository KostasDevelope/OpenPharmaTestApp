using OpenPharmaTestApp.Samples;
using Xunit;

namespace OpenPharmaTestApp.EntityFrameworkCore.Applications;

[Collection(OpenPharmaTestAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<OpenPharmaTestAppEntityFrameworkCoreTestModule>
{
}

[Collection(OpenPharmaTestAppTestConsts.CollectionDefinitionName)]
public class OpenPharmaCustomerServiceTests : CustomerService_Tests<OpenPharmaTestAppEntityFrameworkCoreTestModule>
{

}