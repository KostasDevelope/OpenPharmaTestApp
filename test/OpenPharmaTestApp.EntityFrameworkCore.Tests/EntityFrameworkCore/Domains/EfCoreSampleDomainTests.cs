using OpenPharmaTestApp.Samples;
using Xunit;

namespace OpenPharmaTestApp.EntityFrameworkCore.Domains;

[Collection(OpenPharmaTestAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<OpenPharmaTestAppEntityFrameworkCoreTestModule>
{

}
