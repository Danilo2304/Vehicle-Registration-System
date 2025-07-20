namespace RegistracijaVozila.Models.Domain
{
    public class Registracija
    {
        public Guid Id { get; set; }

        public DateTime DatumRegistracije { get; set; }

        public DateTime DatumIstekaRegistracije { get; set; }

        public decimal CijenaRegistracije { get; set; }

        public string RegistarskaOznaka { get; set; }

        public bool PrivremenaRegistracija {  get; set; }

        public Guid KlijentId { get; set; }

        public Klijent Vlasnik { get; set; }

        public Guid VoziloId { get; set; }

        public Vozilo Vozilo { get; set; }

        public Guid OsiguranjeId { get; set; }

        public Osiguranje Osiguranje { get; set; }

    }
}




