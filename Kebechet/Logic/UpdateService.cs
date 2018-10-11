using System;
using System.Linq;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Extensions.Configuration;

namespace Kebechet.Logic
{
    public class UpdateService : IUpdateService
    {
        // Constructors

        public UpdateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Fields

        private readonly IConfiguration _configuration;

        // Methods

        public UpdateResult UpdateHost(string host, string ipAddress)
        {
            var hostDomain = _configuration["Update-Settings.Host-Domain"];
            var principalConfigPrefix = "Update-Settings.";
            var clientId = _configuration[principalConfigPrefix + ""];
            var clientSecret = _configuration[principalConfigPrefix + ""];
            var tenantId = _configuration[principalConfigPrefix + ""];
            var subscriptionId = _configuration[principalConfigPrefix + ""];

            var sp = new ServicePrincipalLoginInformation
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            };

            var azure = Azure.Authenticate(new AzureCredentials(sp, tenantId, AzureEnvironment.AzureGlobalCloud)).WithSubscription(subscriptionId);

            var dnsZones = azure.DnsZones.List();
            var dnsZone = dnsZones.SingleOrDefault(z => z.Name == hostDomain);
            if (dnsZone == null)
                return UpdateResult.DnsZoneNotFound;

            var recordSets = dnsZone.ARecordSets.List();
            var recordSet = recordSets.SingleOrDefault(rs => rs.Name == host);
            if (recordSet == null)
                return UpdateResult.RecordSetNotFound;

            dnsZone.Update().UpdateARecordSet(recordSet.Name)
                   .WithoutIPv4Address(recordSet.IPv4Addresses[0]).WithIPv4Address(ipAddress).WithTimeToLive(60 * 5)
                   .Parent().Apply();

            return UpdateResult.Success;
        }
    }
}
