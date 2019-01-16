using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using HPC.ACM.API;
using HPC.ACM.API.Models;

namespace HPC.ACM.API.PS
{
    [Cmdlet("Get", "AcmNode", DefaultParameterSetName = "list")]
    [OutputType(typeof(Models.Node))]
    public class GetAcmNodeCommand : AcmCommand
    {
        [Parameter(
            ParameterSetName = "single",
            Position = 0,
            ValueFromPipelineByPropertyName = true)]
        public string Id { get; set; }

        [Parameter(
            ParameterSetName = "list",
            ValueFromPipelineByPropertyName = true)]
        public string LastId { get; set; }

        [Parameter(
            ParameterSetName = "list",
            ValueFromPipelineByPropertyName = true)]
        public int? Count { get; set; }

        protected override void ProcessRecord()
        {
            if (Id != null) {
                var node = ApiClient.GetNode(Id);
                WriteObject(node);
            }
            else {
                var nodes = ApiClient.GetNodes(LastId, Count);
                WriteObject(nodes, true);
            }
        }
    }
}
