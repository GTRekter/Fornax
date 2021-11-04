using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsReportGenerator.Models
{
    public class Push
    {
        [JsonPropertyName("pushId")]
        public int Id { get; set; }

        [JsonPropertyName("pushedBy")]
        public PushedBy PushedBy { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}