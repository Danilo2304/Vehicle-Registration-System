using RegistracijaVozila.Services.Interface;

namespace RegistracijaVozila.Services.Implementation
{
    public class RegistrationCalculatorService : IRegistrationCalculatorService
    {
        private const decimal CalculateInspection = 3500m;
        private const decimal CalculateAdminFee = 1200m;

        public decimal CalculateEcoTax(string ecoClass)
        {
            return ecoClass switch
            {
                "Tečni naftni gas" => 3000,
                "Dizel" => 2500,
                "Kompresovani prirodni gas" =>2250,
                "Benzin" => 2000,
                "Hibrid" => 1500,
                "Električni pogon" => 1200,
                _ => 3000
            };
        }

        public decimal CalculateAgeTax(int vehicleAge)
        {
            if (vehicleAge >= 5 && vehicleAge <=8) return 0.75m;
            if (vehicleAge >= 8 && vehicleAge <= 10) return 0.60m;
            if (vehicleAge >= 10) return 0.35m;
            return 1m;

        }

        public decimal CalculateEngineSizeTax(decimal cm3)
        {
            if (cm3 <= 1150) return 1270;
            if (cm3 <= 1300) return 2570;
            if (cm3 <= 1600) return 4470;
            if (cm3 <= 2000) return 9110;
            if (cm3 <= 2500) return 18220;
            if (cm3 <= 3000) return 28330;
            return 56660; 
        }


        public decimal CalculateInsurance(int kw, decimal pricePerKw)
        {
            return kw * pricePerKw;
        }

        public decimal CalculateRegistrationPrice(int kw,decimal pricePerKw, decimal cm3, int vehicleAge, string ecoClass)
        {
            return CalculateInspection + 
                   CalculateAdminFee + 
                   CalculateEcoTax(ecoClass) + 
                   (CalculateAgeTax(vehicleAge) * CalculateEngineSizeTax(cm3)) +
                   CalculateInsurance(kw,pricePerKw);

        }

        
    }
}
