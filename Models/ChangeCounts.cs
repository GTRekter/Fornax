using System;
using System.Text.Json.Serialization;

namespace AzureDevOpsReportGenerator.Models
{
    public class ChangeCounts
    {
        [JsonPropertyName("Add")]
        public int Add { get; set; }

        [JsonPropertyName("Edit")]
        public int Edit { get; set; }

        [JsonPropertyName("Delete")]
        public int Delete { get; set; }
    }
}