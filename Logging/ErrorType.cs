namespace Hotel.Logging
{
    public struct ErrorType
    {
        public string ServiceName {get; set; }
        public string Key { get; set; }
    }

    public enum Severity
    { 
        ERROR, WARNING, INFO
    }

}
