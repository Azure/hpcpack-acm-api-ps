using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace HPC.ACM.PS
{
    public class Connection
    {
        public AuthenticationResult Profile { get; set; }
        public string ApiBasePoint { get; set; }
    }

    [Cmdlet("Connect", "Acm")]
    [OutputType(typeof(Connection))]
    public class ConnectAcmCommand : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string IssuerUrl { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string ClientId { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string ClientSecret { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string ApiBasePoint { get; set; }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            var authenticationContext = new AuthenticationContext(IssuerUrl);
            var authenticationResult = authenticationContext.AcquireTokenAsync(
                ClientId, new ClientCredential(ClientId, ClientSecret)).GetAwaiter().GetResult();

            WriteObject(new Connection { Profile = authenticationResult, ApiBasePoint = ApiBasePoint });
        }
    }

}
