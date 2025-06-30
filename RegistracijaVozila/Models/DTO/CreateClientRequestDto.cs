using System.ComponentModel.DataAnnotations;

namespace RegistracijaVozila.Models.DTO
{
    public class CreateClientRequestDto
    {
        [Required(ErrorMessage ="Name of client is required")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Surname of client is required")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Social security number of client is required")]
        public string JMBG { get; set; }

        [Required(ErrorMessage = "Id carb number of client is required")]
        public string BrojLicneKarte { get; set; }

        [Required(ErrorMessage = "Email of client is required")]
        public string Email { get; set; }

        public string BrojTelefona { get; set; }

        public string Adresa { get; set; }

        public DateTime DatumRodjenja { get; set; }
    }
}


