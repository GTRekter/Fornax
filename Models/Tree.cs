using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsReportGenerator.Models
{
    public class Tree
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }
}