using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Results;

namespace RegistracijaVozila.Models.DTO
{
    public class RegistrationVehicleDto
    {
        public Guid Id { get; set; }

        public string RegistarskaOznaka { get; set; }

        public DateTime DatumRegistracije { get; set; }

        public DateTime DatumIstekaRegistracije { get; set; }

        public decimal CijenaRegistracije { get; set; }

        public bool PrivremenaRegistracija { get; set; }

        public Guid KlijentId { get; set; }

        public ClientDto Vlasnik { get; set; }

        public Guid VoziloId { get; set; }

        public VehicleDto Vozilo { get; set; }

        public Guid OsiguranjeId { get; set; }

        public InsuranceDto Osiguranje { get; set; }
    }
}
