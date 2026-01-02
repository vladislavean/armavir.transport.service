using armavir.transport.gateway.Contracts;
using AutoMapper;
using core.abstractions.Models;

namespace armavir.transport.gateway.Profiles;

public class GatewayMapperProfile : Profile
{
    public GatewayMapperProfile()
    {
        CreateMap<GetTransportOperationModel, GetTransportsDto>();
        CreateMap<GetTransportStopsOperationModel, GetTransportStopsDto>();
    }
}
