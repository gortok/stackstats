using System;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace StatsViewer.Models

{
    public class StatsRequest
    {
        public Uri Url { get; set; }

        public StatsRequest(Uri uri)
        {
            this.Url = uri;
        }

        public StatsRequest(string candidateUri)
        {
            if (!candidateUri.StartsWith("https://") || !candidateUri.StartsWith("http://"))
            {
                candidateUri = "https://" + candidateUri;
            }
            if (Uri.IsWellFormedUriString(candidateUri, UriKind.Absolute))
            {
                Url = new UriBuilder(candidateUri).Uri;
            }
            else
            {
                Url = new Uri("http://example.com");
            }
        }
    }

    public class StatsRequestProcess
    {
        public int UserId { get; private set; }
        public string SiteName { get; private set; }
        private Uri uri;

        public StatsRequestProcess(StatsRequest request)
        {
            uri = request.Url;
            SiteName = request.Url.Host ?? "";
            if (Int32.TryParse(Regex.Match(request.Url.AbsolutePath, @"/users/([\d]+)/?", RegexOptions.Compiled).Groups[1].Value,
                out var userId))
            {
                UserId = userId;
            }
        }

        public bool IsValid()
        {
            return SiteName != string.Empty && UserId != 0;
        }
    }
}