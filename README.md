# Fornax
Console application that uses the API exposed by both Azure DevOps Services and Azure DevOps Server to create reports.

```
usage: AzureDevOpsReportGenerator.exe -b <value> -c <value> -p <value> [-o path] [-f] [-t]
These are common commands used in various situations:
   b                 BaseUrl to Azure DevOps Server/Services
   c                 Name of the collection to analyze
   p                 Personal Access Token
   f                 If provided, only include history entries created after this date
   t                 If provided, only include history entries created to this date
   o                 Absolute path where to save the report
   h                 Print menu
Example:
   AzureDevOpsReportGenerator.exe -b http://windev2106eval:8888/tfs/ -c DefaultCollection -p ****************************
   AzureDevOpsReportGenerator.exe -b https://dev.azure.com/ -c GTRekter -p ****************************
 ```
 
 The minimum permission are the following:
![image](https://user-images.githubusercontent.com/25728713/140409283-4a7d8d52-1aac-4c51-9f39-ab4820793983.png)
