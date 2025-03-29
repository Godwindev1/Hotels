namespace Hotel.Resource.HotelOffers.Policies
{
    public class HotelProduct_PaymentPolicy
    {
        /*	[
        minLength: 2
        maxLength: 2
        pattern: ^[A-Z]{2}$
        Accepted Payment Card Types for the method CREDIT_CARD

        string
        minLength: 2
        maxLength: 2
        pattern: ^[A-Z]{2}$]*/

        public List<string> creditCards { get; set; }
        public string Method { get; set; }
    }
}
