using RegistracijaVozila.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace RegistracijaVozila.Models.DTO
{
    public class UpdateVehicleDto
    {
        [Required(ErrorMessage = "Id is missing")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "TipVozilaId is missing")]
        public Guid TipVozilaId { get; set; }

        [Required(ErrorMessage = "MarkaVozilaId is missing")]
        public Guid MarkaVozilaId { get; set; }

        [Required(ErrorMessage = "ModelVozilaId is missing")]
        public Guid ModelVozilaId { get; set; }

        public int GodinaProizvodnje { get; set; }

        public float ZapreminaMotora { get; set; }

        public string VrstaGoriva { get; set; }

        public float Masa { get; set; }

        public int SnagaMotora { get; set; }

        public string BrojSasije { get; set; }

        public DateTime DatumRegistracije { get; set; }

        public DateTime DatumPrveRegistracije { get; set; }
    }
}
