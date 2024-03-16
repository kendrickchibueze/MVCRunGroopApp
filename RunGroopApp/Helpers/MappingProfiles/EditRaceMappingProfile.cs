using AutoMapper;
using RunGroopApp.Models;
using RunGroopApp.ViewModels;

namespace RunGroopApp.Helpers.MappingProfiles
{
    public class EditRaceMappingProfile:Profile
    {
        public EditRaceMappingProfile()
        {
            CreateMap<EditRaceViewModel, Race>()
                    .ForMember(dest => dest.Image, opt => opt.Ignore())
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                    {
                        Street = src.Address.Street,
                        City = src.Address.City,
                        State = src.Address.State,

                    }));
            CreateMap<Race, EditRaceViewModel>()
                 .ForMember(dest => dest.Image, opt => opt.Ignore())
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                 {
                     Street = src.Address.Street,
                     City = src.Address.City,
                     State = src.Address.State,

                 }));
        }
    }
}
