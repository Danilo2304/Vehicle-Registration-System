using RegistracijaVozila.Models.Domain;

namespace RegistracijaVozila.Repositories.Interface
{
    public interface IVehicleModelRepository
    {
        Task<List<ModelVozila>> ListByBrandId(Guid id);

        Task<ModelVozila> AddAsync(ModelVozila modelVozila);

        Task<ModelVozila?> DeleteAsync(Guid id);

        Task<ModelVozila?> GetByIdAsync(Guid id);

        Task<ModelVozila?> UpdateAsync(ModelVozila modelVozila);
    }
}
