using armavir.transport.core.InternalInterfaces;
using armavir.transport.core.InternalModels;
using core.abstractions;
using HtmlAgilityPack;

namespace armavir.transport.core.Services;

internal sealed class ParseHtmlServices 
    : IParseHtmlServices
{
    public Result<ICollection<ParsedRoutesServiceModel>> ParseHtml(string html)
    {
        var document = new HtmlDocument();
        document.LoadHtml(html);

        List<ParsedRoutesServiceModel> resultModels = new();

        var tableRows = document.DocumentNode.SelectNodes("//tr").Skip(1);
        foreach (var row in tableRows)
        {
            if (row.HasClass("closed") || row.SelectNodes("td") == null)
            {
                continue;
            }

            var routeNumber = row.SelectNodes("td")[0];
            var company = row.SelectNodes("td")[2];

            var routeInfo = row.SelectNodes("td")[1];
            var routeInfoP = routeInfo.SelectNodes("p").ToList();

            if (routeInfoP.Count == 5)
            {
                var routeName = routeInfoP[0].InnerText;
                var routeForward = routeInfoP[1].InnerText;
                var routeBackward = routeInfoP[2].InnerText;

                var routeInKm = routeInfoP[3].FirstChild.InnerText;

                var maxTransportCount = routeInfoP[4].InnerText;

                var model = new ParsedRoutesServiceModel
                {
                    RouteNumber = routeNumber.InnerText,
                    Company = company.InnerText,
                    RouteName = routeName,
                    RouteForward = routeForward,
                    RouteBackward = routeBackward,
                    RouteInKm = routeInKm,
                    MaxTransportCount = maxTransportCount
                };
                resultModels.Add(model);
            }
            else
            {
                var routeName = routeInfoP[0].InnerText;
                var routeForward = routeInfoP[1].InnerText;
                var routeBackward = routeInfoP[1].InnerText;

                var routeInKm = routeInfoP[2].FirstChild.InnerText;

                var maxTransportCount = routeInfoP[3].InnerText;
                
                var model = new ParsedRoutesServiceModel
                {
                    RouteNumber = routeNumber.InnerText,
                    Company = company.InnerText,
                    RouteName = routeName,
                    RouteForward = routeForward,
                    RouteBackward = routeBackward,
                    RouteInKm = routeInKm,
                    MaxTransportCount = maxTransportCount
                };
                resultModels.Add(model);
            }
        }

        return resultModels;
    }
}
