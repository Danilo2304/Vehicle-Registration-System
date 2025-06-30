namespace RegistracijaVozila.Models.Domain
{
    public class Klijent
    {
        public Guid Id { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string JMBG { get; set; }

        public string BrojLicneKarte { get; set; }

        public string Email { get; set; }

        public string BrojTelefona { get; set; }

        public string Adresa { get; set; }

        public DateTime DatumRodjenja { get; set; }

        public ICollection<Registracija> Registracije { get; set; } = new List<Registracija>();
    }
}
