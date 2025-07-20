namespace RegistracijaVozila.Models.DTO
{
    public class CreateInsurancePriceRequestDto
    {
        public int MinKw { get; set; }

        public int MaxKw { get; set; }

        public decimal PricePerKw { get; set; }

        public Guid OsiguranjeId { get; set; }
    }
}
