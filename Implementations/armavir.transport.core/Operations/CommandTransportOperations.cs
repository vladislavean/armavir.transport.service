using armavir.transport.core.InternalInterfaces;
using AutoMapper;
using core.abstractions;
using core.abstractions.Models;
using core.abstractions.Operations;
using core.abstractions.Options;
using dal.abstractions.Models;
using dal.abstractions.Repositories;
using Microsoft.Extensions.Options;

namespace armavir.transport.core.Operations;

internal sealed class CommandTransportOperations(
    ITransportCommandRepository transportCommandRepository,
    IGetHtmlServices getHtmlServices,
    IMapper mapper,
    IOptions<TransportPageOptions> transportPageOptions,
    IParseHtmlServices parseHtmlServices
    ) 
    : ICommandTransportOperations
{
    private readonly string _transportPageBaseUrl = transportPageOptions.Value.BaseUrl;
    
    public async Task<Result> CreateTransportAsync(CreateTransportOperationModel createTransportOperationModel)
    {
        var repositoryModel = mapper.Map<CreateTransportCommandRepositoryModel>(createTransportOperationModel); 
        await transportCommandRepository.CreateTransport(repositoryModel);
        return Result.Success();
    }

    public async Task<Result> DeleteTransport(Guid id)
    {
        await transportCommandRepository.DeleteTransport(id);
        return Result.Success();
    }

    public async Task<Result> ActualizeTransportData()
    {
        var htmlContent = await getHtmlServices.GetPageHtmlAsync(_transportPageBaseUrl);
        if (htmlContent.IsFailure)
        {
            return Result.Failure(htmlContent.Error);
        }

        var result = parseHtmlServices.ParseHtml(htmlContent.Value);
        
        return Result.Success();
    }
}
