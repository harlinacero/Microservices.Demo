using Microservices.Demo.Report.API.Domain.Entities;
using Microservices.Demo.Report.API.Infraestucture.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using RestEase;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;
using Steeltoe.Discovery.Eureka;

namespace Microservices.Demo.Report.API.Infraestucture.Agents.Policies
{
    public class PolicyClient : IPolicyClient
    {
        private readonly IPolicyClient client;
        private readonly IDiscoveryClient _discoveryClient;
        public readonly ServicesUrl _servicesUrl;

        private static IAsyncPolicy retryPolicy = Polly.Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(3));

        public PolicyClient(IOptions<ServicesUrl> servicesUrl, IDiscoveryClient discoveryClient)
        {
            _servicesUrl = servicesUrl.Value;
            _discoveryClient = discoveryClient;
            //var handler = new DiscoveryHttpClientHandler(discoveryClient);
            ////Certificado no valido
            //handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            //var httpClient = new HttpClient(handler, false)
            //{
            //    BaseAddress = new Uri(_servicesUrl.PolicyApiUrl)
            //};
            //client = RestClient.For<IPolicyClient>(httpClient);
        }

        public async Task<IEnumerable<Domain.Entities.Policy>> GetAsync()
        {
            try
            {
                var handler = new DiscoveryHttpClientHandler(_discoveryClient);
                //Certificado no valido
                handler.ServerCertificateCustomValidationCallback = delegate { return true; };
                using var httpClient = new HttpClient(handler, false);
                var response = await httpClient.GetAsync(_servicesUrl.PolicyApiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Error" + response.StatusCode);
                }
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Domain.Entities.Policy>>(content);
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }
    }
}
