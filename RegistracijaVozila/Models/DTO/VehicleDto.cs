namespace RegistracijaVozila.Models.DTO
{
    public class VehicleDto
    {
        public Guid Id { get; set; }

        public Guid TipVozilaId { get; set; }

        public Guid MarkaVozilaId { get; set; }

        public Guid ModelVozilaId { get; set; }

        public string TipVozilaNaziv { get; set; }

        public string MarkaVozilaNaziv { get; set; }

        public string ModelVozilaNaziv { get; set; }

        public string RegistarskaOznaka { get; set; }

        public int GodinaProizvodnje { get; set; }

        public float ZapreminaMotora { get; set; }

        public string VrstaGoriva { get; set; }

        public float Masa { get; set; }

        public int SnagaMotora { get; set; }

        public string BrojSasije { get; set; }

        public DateTime DatumRegistracije { get; set; }

        public DateTime DatumPrveRegistracije { get; set; }
    }
}
