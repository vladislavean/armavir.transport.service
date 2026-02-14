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
    IStopQueryRepository stopQueryRepository,
    IStopCommandRepository stopCommandRepository,
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

        var parseResult = parseHtmlServices.ParseHtml(htmlContent.Value).Value;
        var stopsToCreate = new List<string>();

        foreach (var route in parseResult)
        {
            var forwardRoute = route.RouteForward.Split('-');
            var backwardRoute = route.RouteBackward.Split('-');
            
            var allStops = forwardRoute.Concat(backwardRoute).ToArray();
            var existsStops = await stopQueryRepository.GetStopsByNamesBatchAsync(allStops);
            
            var nonExistsStops = allStops.Except(existsStops.Select(x => x.Name)).ToList();
            
            stopsToCreate.AddRange(nonExistsStops);
            
            var maxCount = int.TryParse(route.MaxTransportCount.Split(' ')[4], out var max) ? max : 1;
            
            var routeForwardInKm = double.TryParse(route.RouteInKm.Split(' ')[1], out var forwardRouteKm) 
                ? forwardRouteKm : 0;

            var routeBackwardInKm = double.TryParse(route.RouteBackward.Split(' ')[1], out var backwardRouteKm)
                ? backwardRouteKm : 0;
            
            var model = new CreateTransportCommandRepositoryModel
            {
                Name = route.RouteName,
                Number = route.RouteNumber,
                IsActive = true,
                Company = route.Company,
                MaxCount = maxCount,
                ShortRoute = Math.Min(routeBackwardInKm, routeBackwardInKm),
                LongRoute = Math.Max(routeForwardInKm, routeForwardInKm),
            }; 
            await transportCommandRepository.CreateTransport(model);
            
            // добавить добавление в TransportStops
        }

        return Result.Success();
    }
}
