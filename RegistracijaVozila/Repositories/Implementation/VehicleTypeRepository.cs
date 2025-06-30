using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Repositories.Interface;

namespace RegistracijaVozila.Repositories.Implementation
{
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        private readonly RegistracijaVozilaDbContext appDbContext;

        public VehicleTypeRepository(RegistracijaVozilaDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<TipVozila> AddAsync(TipVozila tipVozila)
        {
            await appDbContext.TipoviVozila.AddAsync(tipVozila);
            await appDbContext.SaveChangesAsync();
            return tipVozila;
        }

        public async Task<TipVozila?> DeleteAsync(Guid id)
        {
            var existingVehicleType = await appDbContext.TipoviVozila.FirstOrDefaultAsync(x => x.Id==id);

            if (existingVehicleType != null)
            {
                appDbContext.TipoviVozila.Remove(existingVehicleType);
                await appDbContext.SaveChangesAsync();
                return existingVehicleType;
            }

            return null;

        }

        public async Task<List<TipVozila>> GetAllAsync()
        {
            return await appDbContext.TipoviVozila.ToListAsync();
        }

        public async Task<TipVozila?> GetByIdAsync(Guid id)
        {
            return await appDbContext.TipoviVozila.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TipVozila?> UpdateAsync(TipVozila tipVozila)
        {
            var existingVehicleType = await appDbContext.TipoviVozila.FirstOrDefaultAsync(x => x.Id == tipVozila.Id);

            if(existingVehicleType == null)
            {
                return null;
            }

            existingVehicleType.Naziv = tipVozila.Naziv;
            existingVehicleType.Kategorija = tipVozila.Kategorija;

            await appDbContext.SaveChangesAsync();

            return existingVehicleType;
        }
    }
}
