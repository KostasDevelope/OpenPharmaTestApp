using Xunit;

namespace OpenPharmaTestApp.EntityFrameworkCore;

[CollectionDefinition(OpenPharmaTestAppTestConsts.CollectionDefinitionName)]
public class OpenPharmaTestAppEntityFrameworkCoreCollection : ICollectionFixture<OpenPharmaTestAppEntityFrameworkCoreFixture>
{

}
