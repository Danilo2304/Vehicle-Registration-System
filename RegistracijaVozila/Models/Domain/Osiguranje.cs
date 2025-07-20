namespace RegistracijaVozila.Models.Domain
{
    public class Osiguranje
    {
        public Guid Id { get; set; }

        public string Naziv {  get; set; }

        public ICollection<Registracija> Registracije { get; set; } = new List<Registracija>();

        public ICollection<OsiguranjeCijene> OsiguranjeCijene { get; set; }
    }
}
