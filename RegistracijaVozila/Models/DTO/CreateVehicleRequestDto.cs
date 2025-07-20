using RegistracijaVozila.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace RegistracijaVozila.Models.DTO
{
    public class CreateVehicleRequestDto
    {
        [Required(ErrorMessage = "TipVozilaId field is required")]
        public Guid? TipVozilaId { get; set; }

        [Required(ErrorMessage = "MarkaVozilaId field is required")]
        public Guid? MarkaVozilaId { get; set; }

        [Required(ErrorMessage = "ModelVozilaId field is required")]
        public Guid? ModelVozilaId { get; set; }

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




