﻿namespace Cool.Common.Options
{
    public class AuthenticationOptions
    {
        public string JwtKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
