namespace TurnstileSimulator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using HtmlAgilityPack;
    using Newtonsoft.Json;
    using static System.Console;


    internal static class Program
    {
        private static Settings _settings;
        
        
        
        public static async Task Main()
        {
            _settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("appsettings.json"));
            
            using (var client = new HttpClient {BaseAddress = new Uri(_settings.HostName)})
            {
                await Authenticate(client);
                    
                while (true)
                {
                    WriteLine("Enter employee id and press 'Enter':\nThen enter Entrance id and press enter:\nIs event coming (y/n)?:");

                    var request = new
                    {
                        employeeId = ReadLine(),
                        entranceId = ReadLine(),
                        Event = ReadLine() is string eventName && eventName.Contains('y') ? "Coming" : "Leaving"
                    };

                    try
                    {
                        using (var response = await client.GetAsync($"/Registration/Register{request.Event}?EmployeeId={request.employeeId}&EntranceId={request.entranceId}"))
                        {
                            WriteLine("Success: " + response.IsSuccessStatusCode);
                        }
                    }
                    catch (HttpRequestException)
                    {
                        WriteLine("Network error.");
                    }
                }
            }
        }

        private static async Task Authenticate(HttpClient client)
        {
            string requestVerificationToken;
                
            using (var response = client.GetAsync("/Account/SignIn").GetAwaiter().GetResult())
            {
                response.EnsureSuccessStatusCode();

                var htmlDocument = new HtmlDocument();
                    
                htmlDocument.LoadHtml(await response.Content.ReadAsStringAsync());

                requestVerificationToken = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div/form/input").Attributes["value"].Value;
            }

            var form = new Dictionary<string, string>
            {
                {"Login", _settings.SecurityGuardLogin},
                {"Password", _settings.Password},
                {"__RequestVerificationToken", requestVerificationToken}
            };
                
            using (var response = await client.PostAsync("/Account/SignIn", new FormUrlEncodedContent(form)))
            {
                response.EnsureSuccessStatusCode();
            }
        }
    }
}