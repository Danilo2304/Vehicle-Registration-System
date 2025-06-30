using RegistracijaVozila.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace RegistracijaVozila.Models.DTO
{
    public class CreateRegistrationVehicleRequestDto
    {
        public DateTime DatumRegistracije { get; set; }

        public float CijenaRegistracije { get; set; }

        public bool PrivremenaRegistracija { get; set; }

        public Guid KlijentId { get; set; }

        public Guid VoziloId { get; set; }

        public List<Guid> OsiguranjeIds { get; set; } = new();

    }
}
