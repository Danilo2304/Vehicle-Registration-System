using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Models.DTO;

namespace RegistracijaVozila.Repositories.Interface
{
    public interface IInsuranceRepository
    {
        Task<Osiguranje> CreateInsuranceAsync(Osiguranje insurance);

        Task<List<Osiguranje>> GetAllAsync();

        Task<Osiguranje?> GetInsuranceByIdAsync(Guid id);

        Task<Osiguranje?> DeleteAsync(Guid id);

        Task<Osiguranje?> UpdateAsync(Osiguranje request);
    }
}
