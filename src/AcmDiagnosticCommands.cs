using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using HPC.ACM.API;
using HPC.ACM.API.Models;

namespace HPC.ACM.API.PS
{
    [Cmdlet("Get", "AcmTest")]
    [OutputType(typeof(Models.DiagnoticTest))]
    public class GetAcmTestCommand : AcmCommand
    {
        protected override void ProcessRecord()
        {
            var tests = ApiClient.GetDiagnosticTests();
            WriteObject(tests, true);
        }
    }

    [Cmdlet("Get", "AcmDiagnosticJob", DefaultParameterSetName = "list")]
    [OutputType(typeof(Models.Job))]
    public class GetAcmDiagnosticJobCommand : AcmCommand
    {
        [Parameter(
            ParameterSetName = "single",
            Position = 0,
            ValueFromPipelineByPropertyName = true)]
        public int? Id { get; set; }

        [Parameter(
            ParameterSetName = "list",
            ValueFromPipelineByPropertyName = true)]
        public int? LastId { get; set; }

        [Parameter(
            ParameterSetName = "list",
            ValueFromPipelineByPropertyName = true)]
        public int? Count { get; set; }

        protected override void ProcessRecord()
        {
            if (Id != null) {
                var job = ApiClient.GetDiagnosticJob((int)Id);
                WriteObject(job);
            }
            else {
                var jobs = ApiClient.GetDiagnosticJobs(LastId, Count);
                WriteObject(jobs, true);
            }
        }
    }

    [Cmdlet("Get", "AcmDiagnosticJobAggregationResult")]
    [OutputType(typeof(object))]
    public class GetAcmDiagnosticJobAggregationResultCommand : AcmCommand
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        public int Id { get; set; }

        protected override void ProcessRecord()
        {
            var result = ApiClient.GetDiagnosticJobAggregationResult(Id);
            WriteObject(result);
        }
    }

    [Cmdlet("Start", "AcmDiagnosticJob", DefaultParameterSetName = "simple")]
    [OutputType(typeof(Models.Job))]
    public class StartAcmDiagnosticJobCommand : AcmCommand
    {
        [Parameter(
            ParameterSetName = "job",
            Position = 0,
            ValueFromPipelineByPropertyName = true)]
        public Models.Job Job { get; set; }

        [Parameter(
            ParameterSetName = "simple",
            ValueFromPipelineByPropertyName = true)]
        public string[] Nodes { get; set; }

        [Parameter(
            ParameterSetName = "simple",
            ValueFromPipelineByPropertyName = true)]
        public string Category { get; set; }

        [Parameter(
            ParameterSetName = "simple",
            ValueFromPipelineByPropertyName = true)]
        public string Name { get; set; }


        protected override void ProcessRecord()
        {
            Models.Job job = null;
            if (Job != null) {
                job = ApiClient.CreateDiagnosticJob(Job);
            }
            else {
                var test = new Models.DiagnoticTest(name: Name, category: Category);
                job = new Models.Job(targetNodes: Nodes, diagnosticTest: test);
                job = ApiClient.CreateDiagnosticJob(job);
            }
            WriteObject(job);
        }
    }

    [Cmdlet("Stop", "AcmDiagnosticJob")]
    public class StopAcmDiagnosticJobCommand : AcmCommand
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipelineByPropertyName = true)]
        public int Id { get; set; }

        protected override void ProcessRecord()
        {
            ApiClient.CancelDiagnosticJob(Id, new Models.JobUpdate("cancel"));
        }
    }
}
