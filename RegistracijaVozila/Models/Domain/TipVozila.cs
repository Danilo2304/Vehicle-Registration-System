namespace RegistracijaVozila.Models.Domain
{
    public class TipVozila
    {
        public Guid Id { get; set; }
        public string Naziv { get; set; }  
        public string Kategorija { get; set; }

        public ICollection<MarkaVozila> Marke { get; set; }
    }
}
