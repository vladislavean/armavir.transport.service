using armavir.transport.core.InternalModels;
using core.abstractions;

namespace armavir.transport.core.InternalInterfaces;

internal interface IParseHtmlServices
{
    Result<ICollection<ParsedRoutesServiceModel>> ParseHtml(string html);
}