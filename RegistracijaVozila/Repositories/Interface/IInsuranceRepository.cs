using RegistracijaVozila.Models.Domain;

namespace RegistracijaVozila.Repositories.Interface
{
    public interface IInsuranceRepository
    {
        Task<Osiguranje> CreateInsuranceAsync(Osiguranje insurance);

        Task<List<Osiguranje>> GetAllAsync();

        Task<Osiguranje?> GetInsuranceByIdAsync(Guid id);
    }
}
