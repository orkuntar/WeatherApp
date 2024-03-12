using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string apiKey = "d8c7373ae357096554b031937264487d";
            string city = "";

            Console.Write("Enter Your City: ");
            city = Console.ReadLine();

            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            using(HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    string cityName = data["name"];
                    double tempature = data["main"]["temp"];
                    string weatherDescription = data["weather"][0]["description"];

                    Console.WriteLine($"City: {cityName}");
                    Console.WriteLine($"Tempature: {tempature}");
                    Console.WriteLine($"Weather: {weatherDescription}");
                }
                catch(HttpRequestException e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
        }
    }
}