using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using HPC.ACM.API;

namespace HPC.ACM.PS
{
    [Cmdlet("Get", "AcmNode")]
    [OutputType(typeof(API.Models.Node))]
    public class GetAcmNodeCommand : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true
        )]
        public Connection Connection { get; set; }

        private HPCWebAPI ApiClient { get; set; }

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            ApiClient = ApiClientFactory.Create(Connection);
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            var nodes = ApiClient.GetNodes();
            WriteObject(nodes, true);
        }
    }
}
