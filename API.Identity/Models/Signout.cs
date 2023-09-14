namespace API.Identity.Models
{
    public class Signout
    {
        public string client_id { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }

    }
}
