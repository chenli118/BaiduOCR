using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace BaiduOCR
{
    public static class AccessToken

    {
        // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
        // 返回token示例
        public static String TOKEN = "24.adda70c11b9786206253ddb70affdc46.2592000.1493524354.282335-1234567";

        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static String clientId = ConfigurationManager.AppSettings["clientId"];//"";
        // 百度云中开通对应服务应用的 Secret Key
        private static String clientSecret = ConfigurationManager.AppSettings["clientSecret"];// "";

        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        //private static String clientId = "9f397b0c6b784d3f9a7de26ef1c20c69";
        // 百度云中开通对应服务应用的 Secret Key
        //private static String clientSecret = "b45d7a587e29442fa69d411cc26b0fc0";

        public static String GetAccessToken()
        {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            return result;
        }
    }
}
