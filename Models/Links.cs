using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsReportGenerator.Models
{
    public class Links
    {
        // [JsonPropertyName("self")]
        // public Self Self { get; set; }

        [JsonPropertyName("repository")]
        public Repository Repository { get; set; }

        [JsonPropertyName("changes")]
        public Changes Changes { get; set; }

        [JsonPropertyName("web")]
        public Web Web { get; set; }

        [JsonPropertyName("tree")]
        public Tree Tree { get; set; }
    }
}