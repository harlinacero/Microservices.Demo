using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Configuration
{
    public class RabbitMQSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public List<string> Hostnames { get; set; }
        public int RequestTimeout { get; set; }
        public int PublishConfirmTimeout { get; set; }
        public int RecoveryInterval { get; set; }
        public bool PersistentDeliveryMode { get; set; }
        public bool AutoCloseConnection { get; set; }
        public bool AutomaticRecovery { get; set; }
        public bool TopologyRecovery { get; set; }        
    }
}
