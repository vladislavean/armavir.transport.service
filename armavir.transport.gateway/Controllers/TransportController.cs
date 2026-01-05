using armavir.transport.gateway.Contracts;
using AutoMapper;
using core.abstractions.Operations;
using Microsoft.AspNetCore.Mvc;

namespace armavir.transport.gateway.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TransportController(
    IQueryTransportOperations queryTransportOperations,
    ICommandTransportOperations commandTransportOperations,
    IMapper mapper
    ) : ControllerBase
{
    [HttpGet]
    [Route("getTransportById/{id}")]
    public async Task<GetTransportsDto> GetTransportById(Guid id)
    {
        var result = await queryTransportOperations.GetTransportByIdAsync(id);
        if (!result.IsSuccess)
        {
            throw new Exception(result.Error.Message);
        }

        var model = mapper.Map<GetTransportsDto>(result.Value);
        return model;
    }

    [HttpPost]
    [Route("actualizeTransportData")]
    public async Task ActualizeTransportData()
    {
        var result = await commandTransportOperations.ActualizeTransportData();
    }
}
