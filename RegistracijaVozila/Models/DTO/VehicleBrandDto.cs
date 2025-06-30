namespace RegistracijaVozila.Models.DTO
{
    public class VehicleBrandDto
    {
        public Guid Id { get; set; }

        public string Naziv { get; set; }

        public Guid TipVozilaId { get; set; }

        public string TipVozila { get; set; }

        public string Kategorija { get; set; }
    }
}
