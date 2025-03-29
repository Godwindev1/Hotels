namespace Hotel.Utility
{
    public class ParseJsonLists
    {
        public String ParseListAsJsonText(List<string> Objects)
        {
            if (Objects != null)
            {
                String RequestedObjects = "[";

                foreach (var Amenity in Objects)
                {
                    RequestedObjects += $" \"{Amenity}\", ";
                }

                RequestedObjects += "]";

                return RequestedObjects;
            }


            return null;
        }

    }
}
