using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Repositories.Interface;

namespace RegistracijaVozila.Repositories.Implementation
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly RegistracijaVozilaDbContext appDbContext;

        public VehicleModelRepository(RegistracijaVozilaDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<ModelVozila>> ListByBrandId(Guid id)
        {
            return await appDbContext.ModeliVozila
                .Include(x=>x.MarkaVozila)
                .ThenInclude(x=>x.TipVozila)
                .Where(x => x.MarkaVozilaId == id).ToListAsync();
        }

        public async Task<ModelVozila> AddAsync(ModelVozila modelVozila)
        {
            await appDbContext.ModeliVozila.AddAsync(modelVozila);
            await appDbContext.SaveChangesAsync();
            await appDbContext.Entry(modelVozila).Reference(m => m.MarkaVozila).LoadAsync();
            return modelVozila;
        }

        public async Task<ModelVozila?> DeleteAsync(Guid id)
        {
            var existingVehicle = await appDbContext.ModeliVozila
                .Include(x=>x.MarkaVozila)
                .ThenInclude(x=>x.TipVozila)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingVehicle == null)
            {
                return null;
            }

            appDbContext.ModeliVozila.Remove(existingVehicle);
            await appDbContext.SaveChangesAsync();
            return existingVehicle;
        }

        public async Task<ModelVozila?> GetByIdAsync(Guid id)
        {
            return await appDbContext.ModeliVozila
                .Include(x=>x.MarkaVozila)
                .ThenInclude(x=>x.TipVozila)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ModelVozila?> UpdateAsync(ModelVozila modelVozila)
        {
            var existingVehicle = await appDbContext.ModeliVozila
                 .Include(x=>x.MarkaVozila)
                 .ThenInclude(x=>x.TipVozila)
                 .FirstOrDefaultAsync(x => x.Id == modelVozila.Id);

            if (existingVehicle != null)
            {
                existingVehicle.Naziv = modelVozila.Naziv;
                existingVehicle.MarkaVozilaId = modelVozila.MarkaVozilaId;
                await appDbContext.SaveChangesAsync();
                return existingVehicle;
            }

            return null;
        }
    }
}
