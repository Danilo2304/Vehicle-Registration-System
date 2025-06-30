using RegistracijaVozila.Models.Domain;

namespace RegistracijaVozila.Models.DTO
{
    public class RegistrationVehicleDto
    {
        public Guid Id { get; set; }

        public DateTime DatumRegistracije { get; set; }

        public DateTime DatumIstekaRegistracije { get; set; }

        public float CijenaRegistracije { get; set; }

        public bool PrivremenaRegistracija { get; set; }

        public Guid KlijentId { get; set; }

        public ClientDto Vlasnik { get; set; }

        public Guid VoziloId { get; set; }

        public VehicleDto Vozilo { get; set; }

        public List<InsuranceDto> Osiguranja { get; set; } = new();
    }
}
