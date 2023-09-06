using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TheConsoleApp
{
    public class Utils
    {
        public static Dictionary<string, List<string>> ExtractErrorFromWebApiResponce(string body)
        {
            var responce = new Dictionary<string, List<string>>();
            //JsonElement which help us To work with Arbitary Json Structure that comes from web api;
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(body);
            var errorJsonElement = jsonElement.GetProperty("errors");
            foreach (var fieldErrorsWith in errorJsonElement.EnumerateObject())
            {
                var fiels = fieldErrorsWith.Name;
                var errors = new List<string>();
                foreach (var errorKind in fieldErrorsWith.Value.EnumerateArray())//Value of fieldErrorsWith property is a array of errors
                {
                    var error = errorKind.GetString();
                    errors.Add(error);
                }
                responce.Add(fiels, errors);
            }
            return responce;
        }
    }
}
