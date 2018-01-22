using System.Linq;
using AutoMapper;
using Datingapp.API.Models;
using DatingApp.API.Dtos;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
           CreateMap<User, UserForListDto>()
           .ForMember(dest => dest.PhotoUrl, opt => {
               opt.MapFrom(src => src.Photos.FirstOrDefault(p =>p.IsMain).Url);
           })
           .ForMember(dest =>dest.Age , opt => {
               opt.ResolveUsing(d =>d.DateofBirth.CalculateAge());
           });

           CreateMap<User, UserForDetailedDto>()
              .ForMember(dest =>dest.Age , opt => {
               opt.ResolveUsing(d =>d.DateofBirth.CalculateAge());
               });
           CreateMap<Photo, PhotosForDetailedDto>();

        }
    }
}