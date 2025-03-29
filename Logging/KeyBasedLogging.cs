using System.Security.Cryptography.X509Certificates;

namespace Hotel.Logging
{
    public class KeyBasedLogging
    {
        private Dictionary<string, string> KeyedLogMessages = new();


        public  KeyBasedLogging()
        {

        }

        public String RecieveMessage(string Key)
        {
            if (KeyedLogMessages.ContainsKey(Key))
            {
                var res = KeyedLogMessages[Key];
                KeyedLogMessages.Remove(Key);
                return res;
            }
            else
                return "No Log available For Error {  } ";
        }



        public void LogMessage(string message, string key, Severity severity, ErrorType ServiceInfor)
        {
            String FinalMessage = $"{ServiceInfor.ServiceName} {severity.ToString()}: {message} ";

            if (!KeyedLogMessages.ContainsKey(key))
            {
                KeyedLogMessages[key] = FinalMessage;
            }
            else
            {
                KeyedLogMessages[key] += "\n" + FinalMessage; // Append messages
            }
        }

    }
}
