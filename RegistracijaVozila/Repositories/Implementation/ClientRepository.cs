using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Repositories.Interface;

namespace RegistracijaVozila.Repositories.Implementation
{
    public class ClientRepository : IClientRepository
    {
        private readonly RegistracijaVozilaDbContext appDbContext;

        public ClientRepository(RegistracijaVozilaDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Klijent> AddClientAsync(Klijent klijent)
        {
            await appDbContext.Klijenti.AddAsync(klijent);
            await appDbContext.SaveChangesAsync();
            return klijent;
        }

        public async Task<Klijent?> DeleteClientAsync(Guid id)
        {
            var deletedClient = await appDbContext.Klijenti.FirstOrDefaultAsync(x => x.Id == id);


            appDbContext.Klijenti.Remove(deletedClient);
            await appDbContext.SaveChangesAsync();
            return deletedClient;

        }

        public async Task<(List<Klijent> Items, int TotalCount)> GetAllAsync
            (string? searchQuery = null, int pageNumber = 1, int pageSize = 1000)
        {
            var query = appDbContext.Klijenti.AsQueryable();

            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(
                x => x.Ime.Contains(searchQuery) ||
                x.Prezime.Contains(searchQuery) ||
                x.Email.Contains(searchQuery) ||
                x.BrojLicneKarte.Contains(searchQuery) ||
                x.JMBG.Contains(searchQuery));
            }

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await query.CountAsync();

            return (items, totalCount);
        }

        public async Task<Klijent?> GetClijentByIdAsync(Guid id)
        {
            return await appDbContext.Klijenti.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Klijent?> UpdateClientAsync(Klijent client)
        {
            var existingClient = await appDbContext.Klijenti.FirstOrDefaultAsync(x => x.Id == client.Id);

            if (existingClient == null)
            {
                return null;
            }

            existingClient.Ime = client.Ime;
            existingClient.Prezime = client.Prezime;
            existingClient.JMBG = client.JMBG;
            existingClient.BrojLicneKarte = client.BrojLicneKarte;
            existingClient.DatumRodjenja = client.DatumRodjenja;
            existingClient.Email = client.Email;
            existingClient.Adresa = client.Adresa;
            existingClient.BrojTelefona = client.BrojTelefona;

            await appDbContext.SaveChangesAsync();

            return existingClient;

        }
    }
}
