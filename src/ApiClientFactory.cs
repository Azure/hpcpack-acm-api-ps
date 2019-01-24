using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using HPC.ACM.API;

namespace HPC.ACM.API.PS
{
    class ApiClientFactory
    {
        public static HPCWebAPI Create(Connection connection)
        {
            var client = new HttpClient();
            if (connection.Profile != null) {
                var headers = client.DefaultRequestHeaders;
                var accessToken = connection.Profile.AccessToken;
                headers.Remove("Authorization");
                headers.Add("Authorization", $"Bearer {accessToken}");
            }
            return new HPCWebAPI(new Uri(connection.ApiBasePoint), client, true);
        }
    }
}
