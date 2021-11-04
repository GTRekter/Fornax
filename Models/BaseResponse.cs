using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsReportGenerator.Models
{
    public class BaseResponse<T>
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("value")]
        public List<T> Value { get; set; }
    }
}