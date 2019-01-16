using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using HPC.ACM.API;

namespace HPC.ACM.API.PS
{
    public class AcmCommand : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        public Connection Connection { get; set; }

        protected HPCWebAPI ApiClient { get; set; }

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            ApiClient = ApiClientFactory.Create(Connection);
        }
    }
}
