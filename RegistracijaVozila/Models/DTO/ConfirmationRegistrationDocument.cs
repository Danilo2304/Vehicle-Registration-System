using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace RegistracijaVozila.Models.DTO
{
    public class ConfirmationRegistrationDocument : IDocument
    {
        private readonly RegistrationVehicleDto model;

        public ConfirmationRegistrationDocument(RegistrationVehicleDto model)
        {
            this.model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);

                page.Header().PaddingBottom(30).Text("Potvrda o registraciji vozila")
                .FontSize(20).Bold().AlignCenter();

                page.Content().Column(column =>
                {
                    column.Spacing(10);

                    column.Item().Text($"Ime vlasnika: {model.Vlasnik.Ime}");
                    column.Item().Text($"Prezime vlasnika: {model.Vlasnik.Prezime}");
                    column.Item().Text($"Email: {model.Vlasnik.Email}");
                    column.Item().Text($"Broj lične karte: {model.Vlasnik.BrojLicneKarte}");
                    column.Item().Text($"JMBG: {model.Vlasnik.JMBG}");
                    column.Item().Text($"Adresa: {model.Vlasnik.Adresa}");
                    column.Item().Text($"Broj telefona: {model.Vlasnik.BrojTelefona}");
                    column.Item().Text($"Tip vozila: {model.Vozilo.TipVozilaNaziv}");
                    column.Item().Text($"Marka vozila: {model.Vozilo.MarkaVozilaNaziv}");
                    column.Item().Text($"Model vozila: {model.Vozilo.ModelVozilaNaziv}");
                    column.Item().Text($"Registarska oznaka: {model.RegistarskaOznaka}");
                    column.Item().Text($"Datum registracije: {model.DatumRegistracije:dd.MM.yyyy}");
                    column.Item().Text($"Datum isteka registracije: " +
                        $"{model.DatumIstekaRegistracije:dd.MM.yyyy}");
                    column.Item().Text($"Broj šasije: {model.Vozilo.BrojSasije}");
                    column.Item().Text($"Zapremina motora: {model.Vozilo.ZapreminaMotora}cm3");
                    column.Item().Text($"Snaga motora: {model.Vozilo.SnagaMotora}kw");
                    column.Item().Text($"Godina proizvodnje: {model.Vozilo.GodinaProizvodnje}");
                    column.Item().Text($"Datum prve registracije: {model.Vozilo.DatumPrveRegistracije.Year}");
                    column.Item().Text($"Težina vozila: {model.Vozilo.Masa}kg");
                    column.Item().Text($"Vrsta goriva: {model.Vozilo.VrstaGoriva}");
                    column.Item().Text($"Osiguranje {model.Osiguranje.Naziv}");
                    column.Item().Text($"Ukupna cijena registracije: {model.CijenaRegistracije} dinara");
                });

                page.Footer().AlignCenter().Text(txt =>
                {
                    txt.Span("Ministarstvo unutrašnjih poslova Republike Srbije @ " + DateTime.Now.Year);
                });
            });
        }
    }
}
