using Newtonsoft.Json.Linq;

// JsonParse.Init(new HttpClient().GetStringAsync("https://api.telegram.org/bot5310541428:AAFcIkPbzL3KueVcTkREI7sFHsNRY0Tx0Rk/getUpdates").Result);
// List<ModelMessage> msgs = JsonParse.Parse();
// for (int i = 0; i < msgs.Count; i++)
// {
//     System.Console.WriteLine(ModelMessage.ToString(msgs[i]));
// }
//  Repository.Load(); 

Bot.Init("token");
Bot.Start();
Console.ReadLine();



// Bot.Init("token");
// Bot.SendMessage("843968473","Как ты там вообще?");