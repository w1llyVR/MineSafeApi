using Application.Dto.FotoCondicion;
using Application.Dto.MedidaCorrectiva;
using Application.Dto.ReporteActa;
using Application.Dto.TipoCondicion;
using Application.Dto.Usuario;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Request
            CreateMap<TipoCondicionRequestDto_Create, TipoCondicion>();
            CreateMap<TipoCondicionRequestDto_Update, TipoCondicion>();
            CreateMap<UsuarioRequestDto, Usuario>();
            CreateMap<ReporteActaRequestDto_Create, ReporteActa>();
            CreateMap<ReporteActaRequestDto_Update, ReporteActa>();
            CreateMap<MedidaCorrectivaRequestDto_Create, MedidaCorrectiva>();
            CreateMap<MedidaCorrectivaRequestDto_Update, MedidaCorrectiva>();
            CreateMap<FotoCondicionRequestDto_Create, FotoCondicion>();
            CreateMap<FotoCondicionRequestDto_Update, FotoCondicion>();

            //Response
            CreateMap<TipoCondicion, TipoCondicionResponseDto>();
            CreateMap<Usuario, UsuarioResponseDto>();
            CreateMap<UsuarioLogin, UsuarioLoginResponseDto>();
            CreateMap<ReporteActa, ReporteActaResponseDto>();
            CreateMap<MedidaCorrectiva, MedidaCorrectivaResponseDto>();
            CreateMap<FotoCondicion, FotoCondicionResponseDto>();
        }


    }
}
