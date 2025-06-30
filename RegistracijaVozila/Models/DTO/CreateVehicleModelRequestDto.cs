namespace RegistracijaVozila.Models.DTO
{
    public class CreateVehicleModelRequestDto
    {
        public string Naziv { get; set; }

        public Guid MarkaVozilaId { get; set; }
    }
}
