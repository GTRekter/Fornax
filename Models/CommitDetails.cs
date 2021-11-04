using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsReportGenerator.Models
{
    public class CommitDetails : Commit
    {
        [JsonPropertyName("parents")]
        public List<object> Parents { get; set; }

        [JsonPropertyName("treeId")]
        public string TreeId { get; set; }

        [JsonPropertyName("push")]
        public Push Push { get; set; }

        [JsonPropertyName("_links")]
        public Links Links { get; set; }
    }
}