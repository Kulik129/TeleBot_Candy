public struct  Bot
{
    static string token;
    static string baseUri;

    static HttpClient hc = new HttpClient();

    public static void Start()
    {
        int  offset = 0;
        while (true)
        {
            string url =  $"{baseUri}getUpdates?offset={offset}";
            string json = hc.GetStringAsync(url).Result;
            //  System.Console.WriteLine(json);

            JsonParse.Init(json);
            List<ModelMessage> msgs = JsonParse.Parse();

            foreach (ModelMessage msg in msgs)
            {
                System.Console.WriteLine(msg); 
                Repository.Append(msg);
                string uid = msg.UserId;
               if (!Game.db.ContainsKey(uid))
               {    
                    int candy = new Random().Next(22,33);    
                    Game.db.Add(uid, candy);
                    SendMessage(uid, $"Привет!\n Конфет осталось: {candy}.\n Возьми от 1 до 4", msg.MessageId);
               }
                else
                {
                    int user = 0;
                    bool flag = int.TryParse(msg.MessageText, out user);
                    if (! flag)
                    {
                        SendMessage(uid, $"Введи число", msg.MessageId);
                    }
                    if (user >= 1 && user <= 4)
                    {
                        Game.db[msg.UserId] -= user;
                    }
                    else
                    {
                        SendMessage(uid, $"Не то число ты вводишь", msg.MessageId);
                    }
                    SendMessage(uid, $"Конфет осталось: {Game.db[msg.UserId]}.\n Возьми от 1 до 4", msg.MessageId);
                }
                
                //  System.Console.WriteLine(offset );
                
                offset = (Int32.Parse(msg.UpdateId) +1);
                Thread.Sleep(200);
            }
            Repository.Save();
            Thread.Sleep(2000);
        }
    }

    public static void Init(string publicToken)
    {
        token = publicToken;
        baseUri = "https://api.telegram.org/bot5583734191:AAE1GvqctoeO8RCCaGjiayIQrhMYx1ZukAY/";
    }
    
    public static void SendMessage(string id, string text, string replyToMessageId = "")
    {
        string url = $"{baseUri}sendMessage?chat_id={id}&text={text}&reply_to_message_id={replyToMessageId}";
        var req = hc.GetStringAsync(url).Result;
    }

}