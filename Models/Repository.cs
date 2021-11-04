using System;
using System.Text.Json.Serialization;

namespace AzureDevOpsReportGenerator.Models
{
    public class Repository
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("project")]
        public Project Project { get; set; }

        [JsonPropertyName("defaultBranch")]
        public string DefaultBranch { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("remoteUrl")]
        public string RemoteUrl { get; set; }

        [JsonPropertyName("sshUrl")]
        public string SshUrl { get; set; }

        [JsonPropertyName("webUrl")]
        public string WebUrl { get; set; }

        [JsonPropertyName("isDisabled")]
        public bool IsDisabled { get; set; }
    }
}
