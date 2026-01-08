using armavir.transport.core.InternalInterfaces;
using core.abstractions;
using HtmlAgilityPack;

namespace armavir.transport.core.Services;

internal sealed class ParseHtmlServices 
    : IParseHtmlServices
{
    public Result ParseHtml(string html)
    {
        var document = new HtmlDocument();
        document.LoadHtml(html);

        var tableRows = document.DocumentNode.SelectNodes("//tr").Skip(1);
        foreach (var row in tableRows)
        {
            if (row.HasClass("closed"))
            {
                continue;
            }

            var routeNumber = row.SelectNodes("td")[0];
            Console.WriteLine(row.InnerText);
            var company = row.SelectNodes("td")[2];
            
            var routeInfo = row.SelectNodes("td")[1];
            var routeInfoP = routeInfo.SelectNodes("p").ToList();

            var routeName = routeInfoP[0].InnerText;
            var routeForward = routeInfoP[1].InnerText;
            var routeBackward = routeInfoP[2].InnerText;
            
            var routeInKm = routeInfoP[3].FirstChild.InnerText;
            
            var maxTransportCount = routeInfoP[4].InnerText;
        }
        return Result.Success();
    }
}
