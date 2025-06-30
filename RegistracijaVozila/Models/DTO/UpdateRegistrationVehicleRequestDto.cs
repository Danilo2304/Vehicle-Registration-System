using RegistracijaVozila.Models.Domain;

namespace RegistracijaVozila.Models.DTO
{
    public class UpdateRegistrationVehicleRequestDto
    {
        public Guid Id { get; set; }

        public DateTime DatumRegistracije { get; set; }

        public float CijenaRegistracije { get; set; }

        public bool PrivremenaRegistracija { get; set; }

        public Guid KlijentId { get; set; }

        public Guid VoziloId { get; set; }

        public List<Guid> OsiguranjeIds { get; set; } = new();
    }
}
