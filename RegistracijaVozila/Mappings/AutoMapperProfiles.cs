using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Models.DTO;

namespace RegistracijaVozila.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Vozilo, CreateVehicleRequestDto>().ReverseMap();

            CreateMap<Vozilo, UpdateVehicleDto>().ReverseMap();

            CreateMap<Vozilo, VehicleDto>().
                ForMember(dest => dest.TipVozilaNaziv, opt => opt.MapFrom(src => src.TipVozila.Naziv)).
                ForMember(dest => dest.MarkaVozilaNaziv, opt => opt.MapFrom(src => src.MarkaVozila.Naziv)).
                ForMember(dest => dest.ModelVozilaNaziv, opt => opt.MapFrom(src => src.ModelVozila.Naziv)).ReverseMap();

            CreateMap<Klijent, CreateClientRequestDto>().ReverseMap();

            CreateMap<Klijent, ClientDto>().ReverseMap();

            CreateMap<Klijent, UpdateClientRequestDto>().ReverseMap();

            CreateMap<Osiguranje, CreateInsuranceRequestDto>().ReverseMap();

            CreateMap<Osiguranje, InsuranceDto>().ReverseMap();

            CreateMap<VehicleTypeDto, TipVozila>().ReverseMap();

            CreateMap<CreateVehicleTypeRequestDto, TipVozila>().ReverseMap();

            CreateMap<UpdateVehicleTypeRequestDto, TipVozila>().ReverseMap();

            CreateMap<MarkaVozila, VehicleBrandDto >().ForMember(
                dest=>dest.TipVozila, opt=>opt.MapFrom(src=>src.TipVozila.Naziv)).
                ForMember(dest=>dest.Kategorija, opt=>opt.MapFrom(src=>src.TipVozila.Kategorija)).ReverseMap();

            CreateMap<MarkaVozila, UpdateVehicleBrandRequestDto>().ReverseMap();

            CreateMap<CreateVehicleBrandRequestDto, MarkaVozila>().ReverseMap();

            CreateMap<VehicleModelDto, ModelVozila>().ReverseMap();

            CreateMap<CreateVehicleModelRequestDto, ModelVozila>().ReverseMap();

            CreateMap<UpdateVehicleModelRequestDto, ModelVozila>().ReverseMap();

            CreateMap<Registracija, RegistrationVehicleDto>()
                .ForMember(dest => dest.Vlasnik, opt => opt.MapFrom(src => src.Vlasnik))
                .ForMember(dest => dest.Vozilo, opt => opt.MapFrom(src => src.Vozilo))
                .ForMember(dest => dest.Osiguranja, opt => opt.MapFrom(
                    src => src.OsiguranjeRegistracija.Select(or => or.Osiguranje).ToList()))
                .ReverseMap();

            CreateMap<Registracija, CreateRegistrationVehicleRequestDto>()
                .ForMember(dest => dest.OsiguranjeIds, opt => opt.MapFrom(
                    src => src.OsiguranjeRegistracija.Select(or => or.OsiguranjeVozilaId).ToList()))
                .ReverseMap();

            CreateMap<Registracija, UpdateRegistrationVehicleRequestDto>().
                ForMember(dest => dest.OsiguranjeIds, opt => opt.MapFrom(
                    src => src.OsiguranjeRegistracija.Select(or => or.OsiguranjeVozilaId).ToList()))
                .ReverseMap();

            CreateMap<UserDto,IdentityUser>().ReverseMap();

        }
    }
}
