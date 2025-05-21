using Hotel.Logging;

namespace Hotel.Services.data
{
    public class QueueMessage
    {
        private MessageStructuring JsonMessages;
        private string FunctionName;
        private string ServiceName;
        private int  StatusCode;


        public QueueMessage() {
            JsonMessages = new MessageStructuring();
        }

        public void AddJsonMessage(string Json)
        {
            JsonMessages.AddJson("  ", Json);   
        }

        public void AddInfo(string functionName, string serviceName, int statusCode)
        {
            FunctionName = functionName;
            ServiceName = serviceName;
            StatusCode = statusCode;
        }

        public void AddMessage(string message)
        {
            JsonMessages.AddMessage(message);
        }


        public string ParseMessage()
        {
            string MessageResult = $"Function:  {FunctionName} \n Service: {ServiceName} \n statuscode: {StatusCode.ToString()} \n"
                + $"Information Handle: {JsonMessages.GetMessage()} \n";

            return MessageResult;
        }
    }
}
