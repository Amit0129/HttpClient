

using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace TheConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://localhost:7177/api/people";
            string uriWeather = "https://localhost:7177/WeatherForecast";
            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            bool check = true;
            while (check)
            {
                Console.WriteLine("Write 1 for Get Method");
                Console.WriteLine("Write 2 for Post Method Using PostAsJsonAsync");
                Console.WriteLine("Write 3 for Post Method Using PostAsync");
                Console.WriteLine("Write 4 for Send Value In Header ");
                Console.WriteLine("Write 5 for Put Method ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        using (var client = new HttpClient())
                        {
                            //HTTP GetMethod

                            var response = await client.GetAsync(url);
                            //response.EnsureSuccessStatusCode();
                            if (response.IsSuccessStatusCode)
                            {
                                var responseString = await response.Content.ReadAsStringAsync();//String Representation Of response Content/Body
                                var people = JsonSerializer.Deserialize<IEnumerable<Person>>(responseString, jsonSerializerOptions);//Convert String to Json object 1st Method
                                Console.WriteLine(responseString);                                                                                               //var people = JsonConvert.DeserializeObject<IEnumerable<Person>>(responseString);//2nd Method we have to Install Newtonsoft.Json Nuget for this
                            }
                        }
                        break;
                    case 2:
                        //Console.WriteLine("Enter a Person Name");
                        //string name = Console.ReadLine();
                        using (var httpClient = new HttpClient())
                        {
                            //Http Post Method 
                            //Example 1
                            var newPerson = new Person()
                            {
                                //Name = name
                            };
                            var response = await httpClient.PostAsJsonAsync(url, newPerson);//It serialize the parameter and isssu Http POST.
                            if (response.StatusCode  == System.Net.HttpStatusCode.BadRequest)//{For null Name Error} It Will Show the Name Is is required Http Status Code Error. 
                            {
                                var person = await response.Content.ReadAsStringAsync();
                                var errorFromWebApi = Utils.ExtractErrorFromWebApiResponce(person);
                                foreach (var fieldWithError in errorFromWebApi)
                                {
                                    Console.WriteLine($"{fieldWithError.Key}");
                                    foreach (var error in fieldWithError.Value)
                                    {
                                        Console.WriteLine($"{ error}");
                                    }
                                }
                            }
                            if (response.IsSuccessStatusCode)
                            {
                                var person = await response.Content.ReadAsStringAsync();
                                Console.WriteLine(person);
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine("Enter a Person Name");
                        string nameOfPerson = Console.ReadLine();
                        using (var httpClient = new HttpClient())
                        {
                            //Http Post Method 
                            //Example 2
                            var newPerson = new Person()
                            {
                                Name = nameOfPerson
                            };
                            var newPersonSerialize = JsonSerializer.Serialize(newPerson);
                            var stringContent = new StringContent(newPersonSerialize, Encoding.UTF8, "application/json");//We can use xml instade of json for sending xml
                            var responce = await httpClient.PostAsync(url,stringContent);
                        }
                        break;
                    case 4:
                        using(var httpClient = new HttpClient())
                        {
                            using(var requestMessage = new HttpRequestMessage(HttpMethod.Get, uriWeather))
                            {
                                requestMessage.Headers.Add("weatherAmount", "10");
                                var responce = await httpClient.SendAsync(requestMessage);
                                var weatherForcasstInfo = JsonSerializer.Deserialize<List<WeatherForecast>>(
                                    await responce.Content.ReadAsStringAsync(),jsonSerializerOptions);
                                foreach (var weatherCast in weatherForcasstInfo)
                                {
                                    Console.WriteLine(weatherCast);
                                }
                                Console.WriteLine($"Amount of Weather data #1 : {weatherForcasstInfo.Count}");
                            }

                        }
                        break;
                    case 5:
                        using(var httpClient = new HttpClient())
                        {
                            Console.WriteLine("Enter Person Id You want to update");
                            int personId = Convert.ToInt32(Console.ReadLine());
                            var person = new Person()
                            {
                                Name = "Pranav Wagmare"
                            };
                            await httpClient.PutAsJsonAsync($"{url}/{personId}", person);
                            var people = await httpClient.GetFromJsonAsync<List<Person>>(url);
                        }
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Enter 1 for continue");
                int run = Convert.ToInt32(Console.ReadLine());
                if (run == 1)
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
            }
        }
    }
}