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

            CreateMap<Osiguranje, UpdateInsuranceRequestDto>().ReverseMap();

            CreateMap<VehicleTypeDto, TipVozila>().ReverseMap();

            CreateMap<CreateVehicleTypeRequestDto, TipVozila>().ReverseMap();

            CreateMap<UpdateVehicleTypeRequestDto, TipVozila>().ReverseMap();

            CreateMap<MarkaVozila, VehicleBrandDto >()
                .ForMember(dest => dest.TipVozila, opt => opt.MapFrom(src => src.TipVozila.Naziv))
                .ForMember(dest => dest.Kategorija, opt => opt.MapFrom(src => src.TipVozila.Kategorija))
                .ReverseMap();

            CreateMap<MarkaVozila, UpdateVehicleBrandRequestDto>().ReverseMap();

            CreateMap<CreateVehicleBrandRequestDto, MarkaVozila>().ReverseMap();

            CreateMap<ModelVozila,VehicleModelDto>()
                .ForMember(dest=>dest.MarkaVozilaNaziv, opt=>opt.MapFrom(src=>src.MarkaVozila.Naziv))
                .ForMember(dest=>dest.TipVozilaId, opt=>opt.MapFrom(src=>src.MarkaVozila.TipVozila.Id))
                .ForMember(dest=>dest.TipVozilaNaziv, opt=>opt.MapFrom(src=>src.MarkaVozila.TipVozila.Naziv))
                .ReverseMap();

            CreateMap<CreateVehicleModelRequestDto, ModelVozila>().ReverseMap();

            CreateMap<UpdateVehicleModelRequestDto, ModelVozila>().ReverseMap();

            CreateMap<Registracija, RegistrationVehicleDto>()
                .ForMember(dest => dest.Vlasnik, opt => opt.MapFrom(src => src.Vlasnik))
                .ForMember(dest => dest.Vozilo, opt => opt.MapFrom(src => src.Vozilo))
                .ForMember(dest => dest.Osiguranje, opt=>opt.MapFrom(src=>src.Osiguranje))
                .ReverseMap();

            CreateMap<Registracija, CreateRegistrationVehicleRequestDto>().ReverseMap();

            CreateMap<Registracija, UpdateRegistrationVehicleRequestDto>().ReverseMap();

            CreateMap<UserDto,IdentityUser>().ReverseMap();

            CreateMap<LoginResponseDto, IdentityUser>().ReverseMap();

            CreateMap<LoginRequestDto, IdentityUser>().ReverseMap();

            CreateMap<OsiguranjeCijene, CreateInsurancePriceRequestDto>().ReverseMap();

            CreateMap<OsiguranjeCijene, InsurancePriceDto>().ReverseMap();

        }
    }
}
