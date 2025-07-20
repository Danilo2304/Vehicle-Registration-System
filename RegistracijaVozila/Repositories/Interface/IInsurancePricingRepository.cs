using RegistracijaVozila.Models.Domain;

namespace RegistracijaVozila.Repositories.Interface
{
    public interface IInsurancePricingRepository
    {
        Task<OsiguranjeCijene> CreateAsync(OsiguranjeCijene osiguranjeCijene);

        Task<List<OsiguranjeCijene>> GetAllAsync();

        Task<OsiguranjeCijene?> GetByIdAsync(Guid id);

        Task<OsiguranjeCijene?> GetByInsuranceIdAsync(Guid id, int kw);
    }
}
