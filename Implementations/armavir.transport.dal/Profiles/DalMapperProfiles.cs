using armavir.transport.dal.Entities;
using AutoMapper;
using dal.abstractions.Models;
using System.Runtime.InteropServices.ComTypes;

namespace armavir.transport.dal.Profiles;

public class DalMapperProfiles : Profile
{
    public DalMapperProfiles()
    {
        CreateMap<CreateTransportCommandRepositoryModel, Transports>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TransportStops, opt => opt.Ignore());

        CreateMap<Transports, GetTransportRepositoryModel>()
            .ForMember(dest => dest.Stops, opt
                => opt.MapFrom((_, _, _, x ) => x.Items[nameof(GetTransportRepositoryModel.Stops)]));
    }
}
