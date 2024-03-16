using AutoMapper;
using RunGroopApp.Models;
using RunGroopApp.ViewModels;

namespace RunGroopApp.Helpers.MappingProfiles
{
    public class EditClubMappingProfile:Profile
    {
        public EditClubMappingProfile()
        {
                CreateMap<EditClubViewModel, Club>()
                    .ForMember(dest => dest.Image, opt => opt.Ignore())
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                    {
                        Street = src.Address.Street,
                        City = src.Address.City,
                        State = src.Address.State,

                    }));
              CreateMap<Club, EditClubViewModel>()
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
