namespace RegistracijaVozila.Models.Domain
{
    public class OsiguranjeRegistracija
    {
        public Guid RegistracijaId { get; set; }
        public Registracija Registracija { get; set; }

        public Guid OsiguranjeVozilaId { get; set; }
        public Osiguranje Osiguranje { get; set; }
    }
}
