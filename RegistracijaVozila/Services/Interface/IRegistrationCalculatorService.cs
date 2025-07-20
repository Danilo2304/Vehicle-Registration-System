namespace RegistracijaVozila.Services.Interface
{
    public interface IRegistrationCalculatorService
    {
        decimal CalculateInsurance(int kw, decimal pricePerKw);

        decimal CalculateEngineSizeTax(decimal cm3);

        decimal CalculateEcoTax(string ecoClass);

        decimal CalculateRegistrationPrice(int kw, decimal pricePerKw, decimal cm3, int vehicleAge, string ecoClass);
    }
}
