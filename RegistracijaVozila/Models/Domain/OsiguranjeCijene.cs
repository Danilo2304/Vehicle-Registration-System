namespace RegistracijaVozila.Models.Domain
{
    public class OsiguranjeCijene
    {
        public Guid Id { get; set; }

        public Guid OsiguranjeId { get; set; }
        public Osiguranje Osiguranje { get; set; }

        public int MinKw { get; set; }

        public int MaxKw { get; set; }

        public decimal PricePerKw { get; set; }
    }
}
