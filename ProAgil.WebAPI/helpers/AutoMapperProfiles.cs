using System.Linq;
using AutoMapper;
using ProAgil.Domain.Models;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.helpers
{
    public class AutoMapperProfiles : Profile
    {
       public AutoMapperProfiles()
      {
         CreateMap<Evento,EventoDto>()
            .ForMember(dest => dest.Palestrantes, opt => {
               opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Palestrante).ToList());
            }).ReverseMap();
   
         CreateMap<Palestrante,PalestranteDTO>()
            .ForMember(dest => dest.Eventos, opt => {
               opt.MapFrom(scr => scr.PalestranteEventos.Select(x => x.Evento).ToList()); 
            }).ReverseMap();
   
         CreateMap<RedeSocial,RedeSocialDTO>().ReverseMap();

         CreateMap<Lote,LoteDTO>().ReverseMap(); 

      } 
   }
}