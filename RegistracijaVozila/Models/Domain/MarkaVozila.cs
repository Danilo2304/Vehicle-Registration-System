namespace RegistracijaVozila.Models.Domain
{
    public class MarkaVozila
    {
        public Guid Id { get; set; }
        public string Naziv { get; set; } 

        public Guid TipVozilaId { get; set; }
        public TipVozila TipVozila { get; set; }

        public ICollection<ModelVozila> Modeli { get; set; }
    }
}
