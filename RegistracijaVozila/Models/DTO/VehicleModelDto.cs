namespace RegistracijaVozila.Models.DTO
{
    public class VehicleModelDto
    {
        public Guid Id { get; set; }

        public string Naziv { get; set; }

        public Guid MarkaVozilaId { get; set; }

        public string MarkaVozilaNaziv { get; set; }
    }
}
