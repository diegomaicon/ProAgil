using System.Linq;
using AutoMapper;
using ProAgil.Domain.Identity;
using ProAgil.Domain.Models;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.helpers
{
    public class AutoMapperProfiles : Profile
    {
       public AutoMapperProfiles()
      {
        CreateMap<Evento, EventoDto>()
                .ForMember(dest => dest.Palestrantes, opt => {
                    opt.MapFrom(src => src.PalestranteEventos.Select(pe => pe.Palestrante).ToList());
                })
                .ForMember(dest => dest.DataEvento, opt => {
                    opt.MapFrom(src => src.DataEvento.ToString("dd/MM/yyyy HH:mm"));
                })
                .ReverseMap();
   
         CreateMap<Palestrante,PalestranteDTO>()
            .ForMember(dest => dest.Eventos, opt => {
               opt.MapFrom(scr => scr.PalestranteEventos.Select(x => x.Evento).ToList()); 
            }).ReverseMap();
   
         CreateMap<RedeSocial,RedeSocialDTO>().ReverseMap();

         CreateMap<Lote,LoteDTO>().ReverseMap(); 
         CreateMap<User,UserDTO>().ReverseMap(); 
         CreateMap<User,UserLoginDTO>().ReverseMap(); 
      } 
   }
}