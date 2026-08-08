using Volo.Abp.Settings;

namespace OpenPharmaTestApp.Settings;

public class OpenPharmaTestAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(OpenPharmaTestAppSettings.MySetting1));
    }
}
