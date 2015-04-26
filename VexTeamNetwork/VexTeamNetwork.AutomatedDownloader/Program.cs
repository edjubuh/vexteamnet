using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure;
using System.Configuration;
using System.Threading;
using Microsoft.Azure;
using System.Diagnostics;
using System.IO;

namespace VexTeamNetwork.AutomatedDownloader
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            //var host = new JobHost();

            Download.RefreshDatabase(TextWriter.Null);
            Console.ReadLine();
        }
    }
}
