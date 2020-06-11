using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace SalaryCalculates
{
    public interface ILogger
    {
        public void Log(string msg);
    }
    public  interface IInflationRate
    {
        public double GetInflationRateByYear(int year);
    }
    public class AmountService
    {
        private readonly IInflationRate inflationRate;
        private readonly ILogger logger;

        public AmountService(IInflationRate inflationRate,ILogger logger)
        {
            this.inflationRate = inflationRate;
            this.logger = logger;
        }
        public double GetAmountCalculateByYear(double amount, int year)
        {
            logger.Log($"GetAmountCalculateByYear Paramaters {amount}{year}");
            if (year<2000)
            {
                throw new ArgumentException("Geçersiz yıl");
            }
            if (amount<0)
            {
                throw new ArgumentException("Geçersiz amount");
            }
            var result = amount + amount * inflationRate.GetInflationRateByYear(year);

            return result;
        }
    }
}
