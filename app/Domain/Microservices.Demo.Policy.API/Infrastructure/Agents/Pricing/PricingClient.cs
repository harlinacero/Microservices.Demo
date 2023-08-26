namespace Microservices.Demo.Policy.API.Infrastructure.Agents.Pricing
{
    using Microservices.Demo.Policy.API.Infrastructure.Agents.Pricing.Commands;
    using Microservices.Demo.Policy.API.Infrastructure.Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Polly;
    using RestEase;
    using Steeltoe.Common.Discovery;
    using Steeltoe.Discovery;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class PricingClient : IPricingClient
    {
        private readonly IPricingClient client;
        public readonly ServicesUrl _servicesUrl;

        private static IAsyncPolicy retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(3));

        public PricingClient(IOptions<ServicesUrl> servicesUrl, IDiscoveryClient discoveryClient)
        {
            _servicesUrl = servicesUrl.Value;
            var handler = new DiscoveryHttpClientHandler(discoveryClient);
            //Certificado no valido
            handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            var httpClient = new HttpClient(handler, false)
            {
                BaseAddress = new Uri(_servicesUrl.PricingApiUrl)
            };
            client = RestClient.For<IPricingClient>(httpClient);
        }

        public Task<CalculatePriceResult> CalculatePrice([Body] CalculatePriceCommand cmd)
        {
            try
            {
                return retryPolicy.ExecuteAsync(async () => await client.CalculatePrice(cmd));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
