namespace RegistracijaVozila.Models.Domain
{
    public class Vozilo
    {
        public Guid Id { get; set; }

        public Guid TipVozilaId { get; set; }
        public TipVozila TipVozila { get; set; }

        public Guid MarkaVozilaId { get; set; }
        public MarkaVozila MarkaVozila { get; set; }

        public Guid ModelVozilaId { get; set; }
        public ModelVozila ModelVozila { get; set; }

        public int GodinaProizvodnje { get; set; }

        public float ZapreminaMotora { get; set; }

        public string VrstaGoriva { get; set;}

        public float Masa { get; set; }

        public int SnagaMotora { get; set; }

        public string BrojSasije { get; set; }

        public DateTime DatumPrveRegistracije { get; set; }

        public Registracija Registracija { get; set; }
    }
}
