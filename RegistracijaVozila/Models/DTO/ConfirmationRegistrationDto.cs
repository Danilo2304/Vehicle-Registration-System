namespace RegistracijaVozila.Models.DTO
{
    public class ConfirmationRegistrationDto
    {
        public string ImeVlasnika { get; set; }

        public string Marka { get; set; }

        public string Model { get; set; }

        public string RegistarskaOznaka { get; set; }

        public DateTime DatumRegistracije { get; set; }

        public DateTime DatumIsteka { get; set; }

        public decimal CijenaRegistracije { get; set; }
    }
}
