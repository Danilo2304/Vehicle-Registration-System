namespace RegistracijaVozila.Models.DTO
{
    public class UpdateVehicleTypeRequestDto
    {
        public Guid Id { get; set; }

        public string Naziv { get; set; }

        public string Kategorija { get; set; }
    }
}
