using System.Text.Json;

namespace Hotel.Logging
{
    public class MessageStructuring
    {
        private int MessageSerialNumber = 0;
        private string Message { get; set; }
        public MessageStructuring()
        {

        }

        public void AddJson(string message, string json )
        {
            using JsonDocument doc = JsonDocument.Parse(json);
            var prettyJson =  JsonSerializer.Serialize(doc.RootElement, new JsonSerializerOptions { WriteIndented = true });

            if (MessageSerialNumber == 0)
            {
                Message += $"{MessageSerialNumber}: {message + json} ";
            }
            else
            {
                Message += $"\n{MessageSerialNumber}: {message + json} ";
            }

            MessageSerialNumber++;
        }

        public void AddMessage(string message)
        {
            if (MessageSerialNumber == 0)
            {
                Message += $"{MessageSerialNumber}: {message} ";
            }
            else
            {
                Message += $"\n{MessageSerialNumber}: {message} ";
            }

            MessageSerialNumber++;
        }

        public string GetMessage()
        {
            Message += "\n message End \n ";
            return Message;
        }


    }
}
