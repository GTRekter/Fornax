using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AzureDevOpsReportGenerator.Models;

namespace AzureDevOpsReportGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {

            string collection = string.Empty;
            string baseUrl = string.Empty;
            string personalaccesstoken = string.Empty;
            string output = Path.GetTempPath();
            DateTime? from = null;
            DateTime? to = null;

            if (args.Length == 0)
            {
                Console.WriteLine("Missing mandatory parameters");
                return;
            }
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-"))
                {
                    switch (args[i].Substring(1))
                    {
                        case "b":
                            baseUrl = args[i + 1];
                            break;
                        case "c":
                            collection = args[i + 1];
                            break;
                        case "p":
                            personalaccesstoken = args[i + 1];
                            break;
                        case "f":
                            from = DateTime.Parse(args[i + 1]);
                            break;
                        case "t":
                            to = DateTime.Parse(args[i + 1]);
                            break;
                        case "o":
                            output = args[i + 1];
                            break;
                        case "h":
                            PrintMenu();
                            return;
                        default:
                            break;
                    }
                }
            }
            
            try 
            {
                var report = new Report();
                var wrapper = new AzureDevOpsWrapper(collection, baseUrl, personalaccesstoken);
                var projects = await wrapper.GetProjectsFromCollection();
                foreach (var project in projects.Value)
                {
                    var repositories = await wrapper.GetRepositoriesFromProject(project.Name);
                    foreach (var repository in repositories.Value)
                    {
                        var pushes = await wrapper.GetPushesFromRepository(project.Name, repository.Id, from, to);
                        foreach (var push in pushes.Value)
                        {
                            var commits = await wrapper.GetCommitsFromRepository(project.Name, repository.Id, null, null, push.Id);
                            report.AddRow(project.Name, repository.Name, push.PushedBy.UniqueName, commits.Count);
                        }
                    }
                }

                string tempPath = Path.Combine(output, $"{DateTime.Now.ToString("MMddyyyyHHmmss")}.csv");
                SaveToCsv<Report.Row>(report.Rows, tempPath);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Report generated at the path: {tempPath}");
                Console.ResetColor();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        // print menu
        private static void PrintMenu()
        {
            Console.WriteLine("usage: AzureDevOpsReportGenerator.exe -b <value> -c <value> -p <value> [-o path] [-f] [-t]");
            Console.WriteLine("These are common commands used in various situations:");
            Console.WriteLine("   b                 BaseUrl to Azure DevOps Server/Services");
            Console.WriteLine("   c                 Name of the collection to analyze");
            Console.WriteLine("   p                 Personal Access Token");
            Console.WriteLine("   f                 If provided, only include history entries created after this date ");
            Console.WriteLine("   t                 If provided, only include history entries created to this date");
            Console.WriteLine("   o                 Absolute path where to save the report");
            Console.WriteLine("   h                 Print menu");
            Console.WriteLine("Example:");
            Console.WriteLine("   AzureDevOpsReportGenerator.exe -b http://windev2106eval:8888/tfs/ -c DefaultCollection -p ***********************");
            Console.WriteLine("   AzureDevOpsReportGenerator.exe -b https://dev.azure.com/ -c IvanPorta -p ******************* -f 6/14/2018 -t 6/14/2020");
        }
        private static void SaveToCsv<T>(List<T> reportData, string path)
        {
            var lines = new List<string>();
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
            var header = string.Join(",", props.ToList().Select(x => x.Name));
            lines.Add(header);
            var valueLines = reportData.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
            lines.AddRange(valueLines);
            File.WriteAllLines(path, lines.ToArray());
        }
    }
}
