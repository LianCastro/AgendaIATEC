using Application.Events;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Event, Event>();
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.HostUsername, opt => opt.MapFrom(e => e.Participants
                    .FirstOrDefault(x => x.IsHost).User.UserName));
            CreateMap<UserEvents, Participant>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(ue => ue.User.UserName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(ue => ue.User.DisplayName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(ue => ue.User.Email));
        }
    }
}
