using AutoMapper;
using UniversidadAPI.DTOs;
using UniversidadAPI.Models.Entities;

namespace UniversidadAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeos para Estudiante
            CreateMap<Estudiante, EstudianteReadDTO>()
                .ForMember(dest => dest.NombreCompleto,
                    opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellido}"));

            CreateMap<EstudianteCreateDTO, Estudiante>();

            CreateMap<EstudianteUpdateDTO, Estudiante>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null));  // Ignora propiedades nulas

            // Mapeos para Profesor

            CreateMap<Profesor, ProfesorReadDTO>()
                .ForMember(dest => dest.NombreCompleto,
                    opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellido}"));

            CreateMap<ProfesorCreateDTO, Profesor>();
            CreateMap<ProfesorUpdateDTO, Profesor>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            // Mapeos para Materia

            CreateMap<Materia, MateriaReadDTO>();
            CreateMap<MateriaCreateDTO, Materia>();
            CreateMap<MateriaUpdateDTO, Materia>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Mapeos para ProfesorMateria

            CreateMap<ProfesorMateria, ProfesorMateriaReadDTO>()
                .ForMember(dest => dest.ProfesorNombre,
                    opt => opt.MapFrom(src => $"{src.Profesor.Nombre} {src.Profesor.Apellido}"))
                .ForMember(dest => dest.MateriaNombre,
                    opt => opt.MapFrom(src => src.Materia.Nombre));

            CreateMap<ProfesorMateriaCreateDTO, ProfesorMateria>();

            // Mapeos para Inscripcion

            CreateMap<Inscripcion, InscripcionReadDTO>()
                .ForMember(dest => dest.EstudianteNombre,
                    opt => opt.MapFrom(src => $"{src.Estudiante.Nombre} {src.Estudiante.Apellido}"))
                .ForMember(dest => dest.ProfesorNombre,
                    opt => opt.MapFrom(src => $"{src.Profesor.Nombre} {src.Profesor.Apellido}"))
                .ForMember(dest => dest.MateriaNombre,
                    opt => opt.MapFrom(src => src.Materia.Nombre));

            CreateMap<InscripcionCreateDTO, Inscripcion>();

        }
    }
}
