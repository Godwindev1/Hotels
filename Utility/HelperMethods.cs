using Hotel.Amadeus;
using Hotel.Logging;

namespace Hotel.Utility
{
    public class HelperMethods
    {
        static public void LogExceptionMessages(ref KeyBasedLogging logger, ref MessageStructuring message, Exception e, ErrorType classInformation)
        {
            if (logger == null || message == null || e == null)
            {
                return;
            }

            message.AddMessage(e.Message);
            logger.LogMessage(message.GetMessage(), classInformation.Key, Severity.ERROR, classInformation);
        }


        static public bool  TestBearerToken(ref MessageStructuring Logmessages, ref KeyBasedLogging logger, ErrorType classInformation)
        {
            if (Request.BearerToken.access_token == null)
            {
                Logmessages.AddMessage("\"Bearer Token Not Yet Retrieved By Application Try Again. \"");
                logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);

                return false;
            }

            return true;

        }
    }
}
