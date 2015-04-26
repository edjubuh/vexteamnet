using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VexTeamNetwork.AutomatedDownloader.Models;

namespace VexTeamNetwork.AutomatedDownloader
{
    static class VexDbDownloader
    {
        public static async Task<HashSet<T>> Download<T>(String url)
        {
            // If it's a base url (/get_teams) we need to add the ? in order to use the &flags later
            if (!url.Contains("?")) url += "?";
#if DEBUG
            Debug.WriteLine("Request is: " + url);
#endif
            int downloadIterations = 1;
            const int downloadSize = 1000;

            RootObject<T> rootObject = (RootObject<T>)
                (await Task.Factory.StartNew(() =>
                {
                    var request = WebRequest.Create(url + "&nodata=true");
                    try
                    {
                        using (Stream stream = request.GetResponse().GetResponseStream())
                        {
                            var reader = new StreamReader(stream, Encoding.UTF8);
                            return JsonConvert.DeserializeObject<RootObject<T>>(reader.ReadToEnd());
                        }
                    }
                    catch (WebException)
                    {
#if DEBUG
                        Debug.WriteLine("Lost internet");
#endif
                        throw;
                    }
                    catch { throw; }
                }));
            downloadIterations = rootObject.size / downloadSize;
            if ((rootObject.size % downloadSize) > 0)
                downloadIterations++;
#if DEBUG
            Debug.WriteLine(downloadIterations.ToString() + " download iterations for " + rootObject.size.ToString() + " objects.");
#endif

            var downloadedStrings = new List<Task<string>>();
            for (int i = 0; i < downloadIterations; i++)
                downloadedStrings.Add(
                    (new WebClient()).DownloadStringTaskAsync(
                    url + "&limit_start=" + (i * downloadSize).ToString() + "&limit_number=" + downloadSize.ToString()));

            HashSet<T> list = new HashSet<T>();
            while (downloadedStrings.Count > 0)
            {
                Task<string> finishedDownload = await Task.WhenAny(downloadedStrings.ToArray());
                downloadedStrings.Remove(finishedDownload);
                try
                {
                    RootObject<T> o = (RootObject<T>)
                        (await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<RootObject<T>>(finishedDownload.Result)));
                    if (o.status)
                        list.UnionWith(o.result);
                }
                catch (WebException)
                {
#if (DEBUG)
                    Debug.WriteLine("Lost Internet");
#endif
                    throw;
                }
                catch { throw; }
            }
            return list;
        }
    }
}
