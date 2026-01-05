using armavir.transport.core.InternalInterfaces;
using core.abstractions;
using System.Text;

namespace armavir.transport.core.Services;

internal sealed class GetHtmlServices(
    IHttpClientFactory clientFactory
    ) : IGetHtmlServices
{
    private readonly HttpClient _httpClient = clientFactory.CreateClient();
    
    public async Task<Result<string>> GetPageHtmlAsync(string pageUrl)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var result = await _httpClient.GetAsync(pageUrl);

        if (!result.IsSuccessStatusCode)
        {
            return Error.Failure("Не удалось получить актуальные данные с дашборда");
        }

        var bytes = await result.Content.ReadAsByteArrayAsync();
        
        var encoding = Encoding.GetEncoding("windows-1251");
        var html = encoding.GetString(bytes);

        return html;
    }
}
