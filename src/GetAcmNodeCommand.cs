using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using HPC.ACM.API;

namespace HPC.ACM.API.PS
{
    [Cmdlet("Get", "AcmNode")]
    [OutputType(typeof(API.Models.Node))]
    public class GetAcmNodeCommand : AcmCommand
    {
        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            var nodes = ApiClient.GetNodes();
            WriteObject(nodes, true);
        }
    }
}
