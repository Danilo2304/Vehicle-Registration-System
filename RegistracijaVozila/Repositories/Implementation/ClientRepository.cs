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

        public async Task<List<Klijent>> GetAllAsync()
        {
            return await appDbContext.Klijenti.ToListAsync();
        }

        public async Task<Klijent?> GetClijentByIdAsync(Guid id)
        {
            return await appDbContext.Klijenti.FirstOrDefaultAsync(x => x.Id == id);

            
            
        }

        public async Task<Klijent?> UpdateClientAsync(Klijent client)
        {
            var existingClient = await appDbContext.Klijenti.FirstOrDefaultAsync(x => x.Id ==  client.Id);

            if(existingClient == null)
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
