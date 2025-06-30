namespace RegistracijaVozila.Models.DTO
{
    public class CreateVehicleBrandRequestDto
    {
        public string Naziv { get; set; }

        public Guid TipVozilaId { get; set; }
    }
}
