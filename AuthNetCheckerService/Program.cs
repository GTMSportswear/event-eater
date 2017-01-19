using System.Threading;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;

namespace AuthNetCheckerService
{
   class Program
   {
      static void Main(string[] args)
      {
         while (true)
         {
            Task.Run(async () =>
            {
               if (await AuthNetIsDown())
                  SendEventToService();
            }).GetAwaiter().GetResult();

            Thread.Sleep(1000 * 5);
         }
      }

      private static async System.Threading.Tasks.Task<bool> AuthNetIsDown()
      {
         using (var client = new HttpClient())
         {
            var html = await client.GetStringAsync("https://status.authorize.net").ConfigureAwait(false);

            var regex = new Regex("body class=\".*status-none.*\"");
            var isDown = regex.IsMatch(html);

            Console.WriteLine($"Authorize.NET is down? {isDown}");
            return isDown;
         }
      }

      private async static void SendEventToService()
      {
         using (var client = new HttpClient())
         {
            await client.PostAsync("http://localhost:64900/event/paymentGatewayDown", null).ConfigureAwait(false);
            Console.WriteLine("Status sent to Event Service?");
         }
      }
   }
}
