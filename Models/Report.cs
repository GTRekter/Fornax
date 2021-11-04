using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevOpsReportGenerator.Models
{
    public class Report
    {
        public List<Row> Rows { get; set; }
        public Report()
        {
            Rows = new List<Row>();
        }
        public void AddRow(string project, string repository, string user, int numberOfCommits)
        {
            var row = Rows.FirstOrDefault(r => r.Project.Equals(project) && r.Repository.Equals(repository) && r.User.Equals(user));
            if(row == null)
            {
                Rows.Add(new Row(project, repository, user, numberOfCommits));
            } 
            else 
            {
                row.NumberOfPush = row.NumberOfPush + 1;
                row.NumberOfCommit = row.NumberOfCommit + numberOfCommits;
            }      
        }
        public class Row
        {
            public Row(string project, string repository, string user, int numberOfCommits)
            {
                Project = project;
                Repository = repository;
                User = user;
                NumberOfPush = 1;
                NumberOfCommit = numberOfCommits;
            }
            public string Project { get; set; }
            public string Repository { get; set; }
            public string User { get; set; }
            public int NumberOfPush { get; set; }
            public int NumberOfCommit { get; set; }
        }
    }
}
