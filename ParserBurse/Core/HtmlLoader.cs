﻿using ParserBurse.Burse;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParserBurse.Core
{
    public class HtmlLoader
    {
        HttpClient client;
        string url;
        string mainUrl;

        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            mainUrl = settings.BaseUrl;
            url = $"{mainUrl}/{settings.Prefix}";
        }

        public async Task<string> GetSourceByPageId(int id)
        {
            var currentUrl = url.Replace("{CurrentId}", id.ToString()); ;
            var response = await client.GetAsync(currentUrl);
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }

        public async Task<string> GetSourceByMainPage()
        {
            var response = await client.GetAsync(mainUrl);
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
