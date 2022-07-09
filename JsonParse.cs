using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public struct JsonParse
{
    static string json;


    public static void Init(string jsonString) 
    {
        json = jsonString;
    }

    public static List<ModelMessage> Parse()
    {
        List<ModelMessage> messagers = new();
        JObject resultReq = JObject.Parse(json);
        JToken result = resultReq["result"]!;

        foreach (JToken message in result)
        {
            ModelMessage mm = new();
            mm. FirstName = message["message"]!["from"]! ["first_name"]!.ToString();
            mm. MessageText = message["message"]!["text"]!.ToString();
            mm. UpdateId = message ["update_id"]!.ToString();
            mm. MessageId = message["message"]!["message_id"]! .ToString();
            mm.UserId = message["message"]!["from"]!["id"]! .ToString();
            messagers.Add(mm);
        }
        return messagers;
    }
}