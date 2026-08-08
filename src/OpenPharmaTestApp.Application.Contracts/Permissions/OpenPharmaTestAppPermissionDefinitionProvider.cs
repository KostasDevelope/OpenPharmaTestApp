using OpenPharmaTestApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace OpenPharmaTestApp.Permissions;

public class OpenPharmaTestAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(OpenPharmaTestAppPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(OpenPharmaTestAppPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<OpenPharmaTestAppResource>(name);
    }
}
