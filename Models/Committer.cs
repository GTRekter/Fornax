using System;
using System.Text.Json.Serialization;

namespace AzureDevOpsReportGenerator.Models
{
    public class Committer
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}