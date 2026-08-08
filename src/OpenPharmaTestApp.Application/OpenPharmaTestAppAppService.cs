using OpenPharmaTestApp.Localization;
using Volo.Abp.Application.Services;

namespace OpenPharmaTestApp;

/* Inherit your application services from this class.
 */
public abstract class OpenPharmaTestAppAppService : ApplicationService
{
    protected OpenPharmaTestAppAppService()
    {
        LocalizationResource = typeof(OpenPharmaTestAppResource);
    }
}
