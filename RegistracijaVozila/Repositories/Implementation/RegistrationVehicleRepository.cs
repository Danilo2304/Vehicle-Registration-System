using Azure.Core;
using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Repositories.Interface;
using RegistracijaVozila.Services.Implementation;
using RegistracijaVozila.Services.Interface;

namespace RegistracijaVozila.Repositories.Implementation
{
    public class RegistrationVehicleRepository : IRegistrationVehicleRepository
    {
        private readonly RegistracijaVozilaDbContext appDbContext;
        private readonly IRegistrationCalculatorService registrationCalculatorService;

        public RegistrationVehicleRepository(RegistracijaVozilaDbContext appDbContext, 
            IRegistrationCalculatorService registrationCalculatorService)
        {
            this.appDbContext = appDbContext;
            this.registrationCalculatorService = registrationCalculatorService;
        }

        public async Task<Registracija> AddRegistrationAsync(Registracija request)
        {
            await appDbContext.Registracije.AddAsync(request);
            await appDbContext.SaveChangesAsync();
            return request;
        }

        public async Task<(List<Registracija> Items, int TotalCount)> GetAllAsync(string? searchQuery = null, int pageNumber = 1,
            int pageSize = 1000)
        {
            var query = appDbContext.Registracije
                .Include(x => x.Vlasnik)
                .Include(x => x.Vozilo)
                .Include(x => x.Vozilo.TipVozila)
                .Include(x => x.Vozilo.MarkaVozila)
                .Include(x=>x.Osiguranje)
                .Include(x => x.Vozilo.ModelVozila).AsQueryable();

            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.Vlasnik.Ime.Contains(searchQuery) ||
                x.Vlasnik.BrojLicneKarte.Contains(searchQuery) ||
                x.Vlasnik.Email.Contains(searchQuery) ||
                x.Vlasnik.JMBG.Contains(searchQuery) ||
                x.Vlasnik.Prezime.Contains(searchQuery) ||
                x.Vozilo.MarkaVozila.Naziv.Contains(searchQuery) ||
                x.Vozilo.ModelVozila.Naziv.Contains(searchQuery) ||
                x.Vozilo.TipVozila.Naziv.Contains(searchQuery) ||
                x.Osiguranje.Naziv.Contains(searchQuery)||
                x.RegistarskaOznaka.Contains(searchQuery));
            }

            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var totalCount = await query.CountAsync();

            return (items, totalCount);
        }

        public async Task<Registracija?> GetByIdAsync(Guid id)
        {
            return await appDbContext.Registracije
                            .Include(x => x.Vlasnik)
                            .Include(x => x.Vozilo.TipVozila)
                            .Include(x => x.Vozilo.MarkaVozila)
                            .Include(x => x.Vozilo.ModelVozila)
                            .Include(x => x.Osiguranje)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Registracija?> DeleteAsync(Guid id)
        {
            var existingRegistration = await appDbContext.Registracije.
                Include(x => x.Vlasnik).
                Include(x => x.Vozilo.TipVozila).
                Include(x => x.Vozilo.MarkaVozila).
                Include(x => x.Osiguranje).
                Include(x => x.Vozilo.ModelVozila).FirstOrDefaultAsync(x => x.Id == id);

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
                Include(x=>x.Osiguranje).
                Include(x => x.Vozilo.ModelVozila).FirstOrDefaultAsync(x => x.Id == request.Id);

            
            existingRegistration.RegistarskaOznaka = request.RegistarskaOznaka;
            existingRegistration.DatumRegistracije = request.DatumRegistracije;
            existingRegistration.PrivremenaRegistracija = request.PrivremenaRegistracija;
            existingRegistration.VoziloId = request.VoziloId;
            existingRegistration.KlijentId = request.KlijentId;
            existingRegistration.OsiguranjeId = request.OsiguranjeId;
            existingRegistration.DatumIstekaRegistracije = request.DatumRegistracije.AddMonths(12);

            await appDbContext.SaveChangesAsync();

            return existingRegistration;
        }
    }
}


