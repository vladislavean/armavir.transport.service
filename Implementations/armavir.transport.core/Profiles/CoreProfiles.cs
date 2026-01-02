using AutoMapper;
using core.abstractions.Models;
using dal.abstractions.Models;

namespace armavir.transport.core.Profiles;

public class CoreProfiles : Profile
{
    public CoreProfiles()
    {
        CreateMap<CreateTransportOperationModel, CreateTransportCommandRepositoryModel>();

        CreateMap<GetTransportRepositoryModel, GetTransportOperationModel>();
        CreateMap<GetTransportStopsRepositoryModel, GetTransportStopsOperationModel>();
    }
}