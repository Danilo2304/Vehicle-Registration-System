namespace RegistracijaVozila.Models.Domain
{
    public class ModelVozila
    {
        public Guid Id { get; set; }
        public string Naziv { get; set; } 

        public Guid MarkaVozilaId { get; set; }
        public MarkaVozila MarkaVozila { get; set; }
    }
}
