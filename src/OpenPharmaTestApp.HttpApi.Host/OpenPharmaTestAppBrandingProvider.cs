using Microsoft.Extensions.Localization;
using OpenPharmaTestApp.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace OpenPharmaTestApp;

[Dependency(ReplaceServices = true)]
public class OpenPharmaTestAppBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<OpenPharmaTestAppResource> _localizer;

    public OpenPharmaTestAppBrandingProvider(IStringLocalizer<OpenPharmaTestAppResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
