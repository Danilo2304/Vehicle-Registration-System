namespace RegistracijaVozila.Models.Domain
{
    public class Osiguranje
    {
        public Guid Id { get; set; }

        public string Naziv {  get; set; }

        public string TipOsiguranja { get; set; }

        public ICollection<OsiguranjeRegistracija> OsiguranjeRegistracija { get; set; } 
            = new List<OsiguranjeRegistracija>();

    }
}
