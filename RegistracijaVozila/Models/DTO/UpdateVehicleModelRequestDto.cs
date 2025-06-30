namespace RegistracijaVozila.Models.DTO
{
    public class UpdateVehicleModelRequestDto
    {
        public Guid Id { get; set; }
        public string Naziv { get; set; }

        public Guid MarkaVozilaId { get; set; }
    }
}
