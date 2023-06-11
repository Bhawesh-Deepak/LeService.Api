using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LeService.Api.Helpers
{
    public static class TelegramMessageHelper
    {
        private static string apiToken = "5317160672:AAEo9aoTeXADdxCZIIcO5pl7dr_FIBZBOG8";
        private static List<string> chatIds = new List<string>() { "-1001659632757" };
        public static string SendMessage(string text, string destId = "5015281343")
        {
            try
            {
                chatIds.ForEach(data =>
                {

                    string urlString = $"https://api.telegram.org/bot{apiToken}/sendMessage?chat_id={data}&text={text}";

                    WebClient webclient = new();

                    webclient.DownloadString(urlString);
                });
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            }


            return "Sent";

        }
    }
}
