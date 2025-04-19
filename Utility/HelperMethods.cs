using Hotel.Amadeus;
using Hotel.Logging;
using System.Globalization;

namespace Hotel.Utility
{
    public class HelperMethods
    {
        public static string EnsureDateFormat(string input)
        {
            // Try parsing it with the correct format Used By Amadeus
            if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result.ToString("yyyy-MM-dd");
            }

            // Try parsing it using general formats
            if (DateTime.TryParse(input, out result))
            {
                return result.ToString("yyyy-MM-dd");
            }

            //TODO:
            //Implement A Different exception that Throws the Error to Grpc
            throw new FormatException("Invalid date format.");
        }
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
