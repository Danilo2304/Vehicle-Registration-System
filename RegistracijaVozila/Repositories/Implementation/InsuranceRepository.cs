using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Repositories.Interface;

namespace RegistracijaVozila.Repositories.Implementation
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly RegistracijaVozilaDbContext appDbContext;

        public InsuranceRepository(RegistracijaVozilaDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Osiguranje> CreateInsuranceAsync(Osiguranje insurance)
        {
            await appDbContext.Osiguranja.AddAsync(insurance);
            await appDbContext.SaveChangesAsync();
            return insurance;

        }

        public async Task<Osiguranje?> DeleteAsync(Guid id)
        {
            var insurance = await appDbContext.Osiguranja.FirstOrDefaultAsync(x=>x.Id == id);

            appDbContext.Osiguranja.Remove(insurance);
            await appDbContext.SaveChangesAsync();

            return insurance;
        }

        public async Task<List<Osiguranje>> GetAllAsync()
        {
            return await appDbContext.Osiguranja.ToListAsync();
        }

        public async Task<Osiguranje?> GetInsuranceByIdAsync(Guid id)
        {
            return await appDbContext.Osiguranja.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Osiguranje?> UpdateAsync(Osiguranje request)
        {
            var existingInsurance = await appDbContext.Osiguranja.FirstOrDefaultAsync(x => x.Id == request.Id);

            existingInsurance.Naziv = request.Naziv;

            await appDbContext.SaveChangesAsync();

            return existingInsurance;
        }
    }
}
