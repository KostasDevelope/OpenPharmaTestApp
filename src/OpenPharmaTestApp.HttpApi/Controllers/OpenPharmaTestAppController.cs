using OpenPharmaTestApp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace OpenPharmaTestApp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class OpenPharmaTestAppController : AbpControllerBase
{
    protected OpenPharmaTestAppController()
    {
        LocalizationResource = typeof(OpenPharmaTestAppResource);
    }
}
