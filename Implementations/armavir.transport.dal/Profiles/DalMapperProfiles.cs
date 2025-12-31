using armavir.transport.dal.Entities;
using AutoMapper;
using dal.abstractions.Models;

namespace armavir.transport.dal.Profiles;

public class DalMapperProfiles : Profile
{
    public DalMapperProfiles()
    {
        CreateMap<CreateTransportCommandRepositoryModel, Transports>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
