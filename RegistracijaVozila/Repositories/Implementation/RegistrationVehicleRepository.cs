using Azure.Core;
using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Repositories.Interface;

namespace RegistracijaVozila.Repositories.Implementation
{
    public class RegistrationVehicleRepository : IRegistrationVehicleRepository
    {
        private readonly RegistracijaVozilaDbContext appDbContext;

        public RegistrationVehicleRepository(RegistracijaVozilaDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Registracija> AddRegistrationAsync(Registracija request)
        {
            request.DatumIstekaRegistracije = request.DatumRegistracije.AddMonths(12);

            await appDbContext.Registracije.AddAsync(request);
            await appDbContext.SaveChangesAsync();
            return request;
        }

        public async Task<List<Registracija>> GetAllAsync()
        {
            return await appDbContext.Registracije
                .Include(x => x.Vlasnik)
                .Include(x => x.Vozilo)
                    .ThenInclude(v => v.TipVozila)
                .Include(x => x.Vozilo)
                    .ThenInclude(v => v.MarkaVozila)
                .Include(x => x.Vozilo)
                    .ThenInclude(v => v.ModelVozila)
                .Include(x => x.OsiguranjeRegistracija)
                    .ThenInclude(xo => xo.Osiguranje)
                .ToListAsync();
        }

        public async Task<Registracija?> GetByIdAsync(Guid id)
        {
            return await appDbContext.Registracije
                            .Include(x => x.Vlasnik)
                            .Include(x => x.Vozilo.TipVozila)
                            .Include(x => x.Vozilo.MarkaVozila)
                            .Include(x => x.Vozilo.ModelVozila)
                            .Include(x => x.OsiguranjeRegistracija)
                            .ThenInclude(xo => xo.Osiguranje)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Registracija?> DeleteAsync(Guid id)
        {
            var existingRegistration = await appDbContext.Registracije.
                Include(x => x.Vlasnik).
                Include(x => x.Vozilo.TipVozila).
                Include(x => x.Vozilo.MarkaVozila).
                Include(x => x.Vozilo.ModelVozila).
                Include(x => x.OsiguranjeRegistracija).
                ThenInclude(xo => xo.Osiguranje).FirstOrDefaultAsync(x => x.Id == id);

            appDbContext.Registracije.Remove(existingRegistration);

            await appDbContext.SaveChangesAsync();

            return existingRegistration;
        }

        public async Task<Registracija?> UpdateAsync(Registracija request)
        {
            var existingRegistration = await appDbContext.Registracije.
                Include(x => x.Vlasnik).
                Include(x => x.Vozilo.TipVozila).
                Include(x => x.Vozilo.MarkaVozila).
                Include(x => x.Vozilo.ModelVozila).
                Include(x => x.OsiguranjeRegistracija).
                ThenInclude(xo => xo.Osiguranje).FirstOrDefaultAsync(x => x.Id == request.Id);

            existingRegistration.CijenaRegistracije = request.CijenaRegistracije;
            existingRegistration.DatumRegistracije = request.DatumRegistracije;
            existingRegistration.PrivremenaRegistracija = request.PrivremenaRegistracija;
            existingRegistration.OsiguranjeRegistracija = request.OsiguranjeRegistracija;
            existingRegistration.VoziloId = request.VoziloId;
            existingRegistration.KlijentId = request.KlijentId;
            existingRegistration.DatumIstekaRegistracije = request.DatumRegistracije.AddMonths(12);

            await appDbContext.SaveChangesAsync();

            return existingRegistration;
        }
    }
}


