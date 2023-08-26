using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Client.Web.ApiGateway.Infrastructure.Configuration
{
    public class WebApiGatewaySettings
    {
        public string Secret { get; set; }
        public string[] AllowedChatOrigins { get; set; }
    }
}
