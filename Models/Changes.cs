using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsReportGenerator.Models
{
    public class Changes
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }
}