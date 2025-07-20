using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Repositories.Interface;

namespace RegistracijaVozila.Repositories.Implementation
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly RegistracijaVozilaDbContext appDbContext;

        public VehicleRepository(RegistracijaVozilaDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Vozilo> AddAsync(Vozilo vozilo)
        {
            await appDbContext.Vozila.AddAsync(vozilo);
            await appDbContext.SaveChangesAsync();

            return await appDbContext.Vozila.
                Include(v => v.TipVozila).
                Include(v => v.MarkaVozila).
                Include(v => v.ModelVozila).
                FirstOrDefaultAsync(v => v.Id == vozilo.Id);
        }

        public async Task<Vozilo?> DeleteVehicleAsync(Guid id)
        {
            var existingVehicle = await appDbContext.Vozila.
                Include(v=>v.TipVozila).
                Include(v=>v.MarkaVozila).
                Include(v=>v.ModelVozila).FirstOrDefaultAsync(x => x.Id == id);

            if(existingVehicle ==  null)
            {
                return null;
            }

            appDbContext.Vozila.Remove(existingVehicle);
            await appDbContext.SaveChangesAsync();

            return existingVehicle;
            
        }

        public async Task<(List<Vozilo> Items, int TotalCount)> GetAllAsync(string? searchQuery = null, int pageSize = 1000, int pageNumber = 1)
        {
            var query = appDbContext.Vozila
                .Include(x => x.TipVozila)
                .Include(x => x.MarkaVozila)
                .Include(x => x.ModelVozila)
                .AsQueryable();


            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.TipVozila.Naziv.Contains(searchQuery) ||
                x.MarkaVozila.Naziv.Contains(searchQuery) ||
                x.ModelVozila.Naziv.Contains(searchQuery) ||
                x.TipVozila.Naziv.Contains(searchQuery));
            }

            
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var totalCount = await query.CountAsync();

            return (items, totalCount); 
        }

        public async Task<Vozilo?> GetVehicleByIdAsync(Guid id)
        {
            return await appDbContext.Vozila.Include(x => x.TipVozila).Include(x => x.MarkaVozila).
                Include(x => x.ModelVozila).FirstOrDefaultAsync(x => x.Id == id);

            
        }

        public async Task<Vozilo?> UpdateVehicleAsync(Vozilo vozilo)
        {
            var existingVozilo = await appDbContext.Vozila.
                Include(x=>x.TipVozila).
                Include(x=>x.MarkaVozila).
                Include(x=>x.ModelVozila).FirstOrDefaultAsync(x => x.Id == vozilo.Id);

            existingVozilo.TipVozilaId = vozilo.TipVozilaId;
            existingVozilo.ModelVozilaId = vozilo.ModelVozilaId;
            existingVozilo.MarkaVozilaId = vozilo.MarkaVozilaId;
            existingVozilo.GodinaProizvodnje = vozilo.GodinaProizvodnje;
            existingVozilo.ZapreminaMotora = vozilo.ZapreminaMotora;
            existingVozilo.SnagaMotora = vozilo.SnagaMotora;
            existingVozilo.Masa = vozilo.Masa;
            existingVozilo.VrstaGoriva = vozilo.VrstaGoriva;
            existingVozilo.BrojSasije = vozilo.BrojSasije;
            existingVozilo.DatumPrveRegistracije = vozilo.DatumPrveRegistracije;

            await appDbContext.SaveChangesAsync();

            return existingVozilo;

        }
    }
}
