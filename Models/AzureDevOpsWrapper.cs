using System;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using AzureDevOpsReportGenerator.Models;

namespace AzureDevOpsReportGenerator.Models
{
    public class AzureDevOpsWrapper
    {
        private readonly string _collection;
        private readonly string _baseUrl;
        private readonly string _personalaccesstoken;
        public AzureDevOpsWrapper(string collection, string baseUrl, string personalaccesstoken)
        {
            _collection = collection;
            _baseUrl = baseUrl;
            _personalaccesstoken = personalaccesstoken;
        }
        public Task<BaseResponse<Project>> GetProjectsFromCollection()
        {
            var url = $"{_baseUrl}{_collection}/_apis/projects?api-version=6.1-preview.4";
            return CallRestAPI<BaseResponse<Project>>(url);
        }
        public Task<BaseResponse<Repository>> GetRepositoriesFromProject(string projectName)
        {
            var url = $"{_baseUrl}/{_collection}/{projectName}/_apis/git/repositories?api-version=6.1-preview.1";
            return CallRestAPI<BaseResponse<Repository>>(url);
        }
        public Task<BaseResponse<Commit>> GetCommitsFromRepository(string projectName, string RepositoryId, DateTime? from = null, DateTime? to = null, int? pushId = null)
        {
            string fromParameter = null;
            string toParameter = null;
            if (from != null)
            {
                fromParameter = from.ToString() + "&";
            }
            if (to != null)
            {
                toParameter = to.ToString() + "&";
            }
            string pushIdParameter = null;
            if (pushId!= null)
            {
                pushIdParameter = $"pushId={pushId}&";
            }
            var url = $"{_baseUrl}/{_collection}/{projectName}/_apis/git/repositories/{RepositoryId}/commits?{fromParameter}{toParameter}{pushIdParameter}api-version=6.1-preview.1";
            return CallRestAPI<BaseResponse<Commit>>(url);
        }
        public Task<BaseResponse<Push>> GetPushesFromRepository(string projectName, string RepositoryId, DateTime? from = null, DateTime? to = null)
        {
            string fromParameter = null;
            string toParameter = null;
            if (from != null)
            {
                fromParameter = $"searchCriteria.fromDate={from?.ToString()}&";
            }
            if (to != null)
            {
                toParameter = $"searchCriteria.toDate={to?.ToString()}&";
            }
            var url = $"{_baseUrl}/{_collection}/{projectName}/_apis/git/repositories/{RepositoryId}/pushes?{fromParameter}{toParameter}api-version=6.1-preview.1";
            return CallRestAPI<BaseResponse<Push>>(url);
        }
        public Task<CommitDetails> GetCommitDetailsFromCommit(string projectName, string RepositoryId, string commitId)
        {
            var url = $"{_baseUrl}/{_collection}/{projectName}/_apis/git/repositories/{RepositoryId}/commits/{commitId}?api-version=6.1-preview.1";
            return CallRestAPI<CommitDetails>(url);
        }
        private async Task<T> CallRestAPI<T>(string url)
        {
            using (var client = new HttpClient())
            {
                string responseBody = string.Empty;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", _personalaccesstoken))));
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                }
                return JsonSerializer.Deserialize<T>(responseBody);
            }
        }
    }
}
