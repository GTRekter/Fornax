using System;
using System.Text.Json.Serialization;

namespace AzureDevOpsReportGenerator.Models
{
    public class Commit
    {
        [JsonPropertyName("commitId")]
        public string Id { get; set; }

        [JsonPropertyName("author")]
        public Author Author { get; set; }

        [JsonPropertyName("committer")]
        public Committer Committer { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("changeCounts")]
        public ChangeCounts ChangeCounts { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("remoteUrl")]
        public string RemoteUrl { get; set; }

        [JsonPropertyName("commentTruncated")]
        public bool? CommentTruncated { get; set; }
    }
}