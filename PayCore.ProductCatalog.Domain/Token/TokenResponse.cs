using System;


namespace PayCore.ProductCatalog.Domain.Token
{
    public class TokenResponse
    {
        public DateTime ExpireTime { get; set; }
        public string AccessToken { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public int SessionTimeInSecond { get; set; }
    }
}
