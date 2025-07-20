using RegistracijaVozila.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace RegistracijaVozila.Models.DTO
{
    public class CreateRegistrationVehicleRequestDto
    {
        public string RegistarskaOznaka { get; set; }

        public DateTime DatumRegistracije { get; set; }

        public bool PrivremenaRegistracija { get; set; }

        public Guid KlijentId { get; set; }

        public Guid VoziloId { get; set; }

        public Guid OsiguranjeId { get; set; }

    }
}
