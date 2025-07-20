using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Repositories.Interface;

namespace RegistracijaVozila.Repositories.Implementation
{
    public class InsurancePricingRepository : IInsurancePricingRepository
    {
        private readonly RegistracijaVozilaDbContext appDbContext;

        public InsurancePricingRepository(RegistracijaVozilaDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<OsiguranjeCijene> CreateAsync(OsiguranjeCijene osiguranjeCijene)
        {
            await appDbContext.AddAsync(osiguranjeCijene);
            await appDbContext.SaveChangesAsync();
            return osiguranjeCijene;
        }

        public async Task<List<OsiguranjeCijene>> GetAllAsync()
        {
            return await appDbContext.OsiguranjeCijene.ToListAsync();
        }

        public async Task<OsiguranjeCijene?> GetByIdAsync(Guid id)
        {
            return await appDbContext.OsiguranjeCijene.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<OsiguranjeCijene?> GetByInsuranceIdAsync(Guid id, int kw)
        {
            return await appDbContext.OsiguranjeCijene.
                Where(x=>x.MinKw<=kw && x.MaxKw>=kw).FirstOrDefaultAsync(x=>x.OsiguranjeId == id);

        }
    }
}
