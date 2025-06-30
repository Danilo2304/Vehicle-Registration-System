namespace RegistracijaVozila.Models.Domain
{
    public class Registracija
    {
        public Guid Id { get; set; }

        public DateTime DatumRegistracije { get; set; }

        public DateTime DatumIstekaRegistracije { get; set; }

        public float CijenaRegistracije { get; set; }

        public bool PrivremenaRegistracija {  get; set; }

        public Guid KlijentId { get; set; }

        public Klijent Vlasnik { get; set; }

        public Guid VoziloId { get; set; }

        public Vozilo Vozilo { get; set; }

        public ICollection<OsiguranjeRegistracija> OsiguranjeRegistracija { get; set; } 
            = new List<OsiguranjeRegistracija>();

    }
}




