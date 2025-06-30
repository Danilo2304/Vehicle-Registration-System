using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Repositories.Interface;
using RegistracijaVozila.Results;

namespace RegistracijaVozila.Repositories.Implementation
{
    public class VehicleBrandRepository: IVehicleBrandRepository
    {
        private readonly RegistracijaVozilaDbContext appDbContext;

        public VehicleBrandRepository(RegistracijaVozilaDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<MarkaVozila> AddAsync(MarkaVozila markaVozila)
        {
            await appDbContext.MarkeVozila.AddAsync(markaVozila);
            await appDbContext.SaveChangesAsync();

            await appDbContext.Entry(markaVozila).Reference(m => m.TipVozila).LoadAsync();

            return markaVozila;

        }

        public async Task<MarkaVozila?> DeleteAsync(Guid id)
        {
            var vehicleBrand = await appDbContext.MarkeVozila.
                Include(x=>x.TipVozila).FirstOrDefaultAsync(x => x.Id == id);

            if(vehicleBrand == null)
            {
                return null;
            }

            appDbContext.MarkeVozila.Remove(vehicleBrand);
            await appDbContext.SaveChangesAsync();

            return vehicleBrand;
        }

        public async Task<MarkaVozila?> GetByIdAsync(Guid id)
        {
            var vehicleBrand = await appDbContext.MarkeVozila.FirstOrDefaultAsync(x=>x.Id==id);

            if (vehicleBrand == null)
            {
                return null;
            }

            await appDbContext.Entry(vehicleBrand).Reference(m => m.TipVozila).LoadAsync();

            return vehicleBrand;
        }

        public async Task<List<MarkaVozila>> ListByTypeId(Guid id)
        {
            return await appDbContext.MarkeVozila.Where(x => x.TipVozilaId == id).ToListAsync();
        }

        public async Task<MarkaVozila?> UpdateAsync(MarkaVozila markaVozila)
        {
            var existingVehicleBrand = await appDbContext.MarkeVozila.
                Include(x=>x.TipVozila).FirstOrDefaultAsync
                (x => x.Id == markaVozila.Id);

            if(existingVehicleBrand == null)
            {
                return null;
            }

            existingVehicleBrand.Naziv = markaVozila.Naziv;
            existingVehicleBrand.TipVozilaId = markaVozila.TipVozilaId;

            await appDbContext.SaveChangesAsync();

            return existingVehicleBrand;
        }
    }
}
