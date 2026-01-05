using core.abstractions;

namespace armavir.transport.core.InternalInterfaces;

internal interface IGetHtmlServices
{
    Task<Result<string>> GetPageHtmlAsync(string pageUrl);
}
