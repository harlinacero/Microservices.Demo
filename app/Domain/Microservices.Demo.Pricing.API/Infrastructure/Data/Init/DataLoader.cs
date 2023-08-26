using Microservices.Demo.Pricing.API.Infrastructure.Data.Entities;
using Microservices.Demo.Pricing.API.Infrastructure.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.Init
{
    public class DataLoader
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDictionary<string, Func<Tariff>> builders = new Dictionary<string, Func<Tariff>>
        {
            {"TRI", DemoTariffFactory.Travel },
            {"HSI", DemoTariffFactory.House },
            {"FAI", DemoTariffFactory.Farm },
            {"CAR", DemoTariffFactory.Car }
        };

        public DataLoader(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Seed()
        {
            await AddTariffIfNotExists("TRI");

            await AddTariffIfNotExists("HSI");

            await AddTariffIfNotExists("FAI");

            await AddTariffIfNotExists("CAR");

            await _unitOfWork.CommitChanges();
        }

        private async Task AddTariffIfNotExists(string code)
        {
            var alreadyExists = await _unitOfWork.TariffsRepository.Exists(code);

            if (!alreadyExists)
            {
                _unitOfWork.TariffsRepository.Add(builders[code]());
            }
        }
    }
}
