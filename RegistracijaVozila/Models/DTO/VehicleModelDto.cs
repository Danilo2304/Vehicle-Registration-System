namespace RegistracijaVozila.Models.DTO
{
    public class VehicleModelDto
    {
        public Guid Id { get; set; }

        public string Naziv { get; set; }

        public Guid MarkaVozilaId { get; set; }

        public string MarkaVozilaNaziv { get; set; }

        public Guid TipVozilaId { get; set; }

        public string TipVozilaNaziv { get; set; }
    }
}
