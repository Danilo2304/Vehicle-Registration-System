namespace RegistracijaVozila.Models.DTO
{
    public class UpdateVehicleBrandRequestDto
    {
        public Guid Id { get; set; }
        public string Naziv { get; set; }

        public Guid TipVozilaId { get; set; }
    }
}
