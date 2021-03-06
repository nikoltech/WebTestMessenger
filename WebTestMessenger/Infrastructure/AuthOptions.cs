﻿namespace WebTestMessenger.Infrastructure
{
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient"; 
        const string KEY = "mysupersecret_secretkey!027"; 
        public const int LIFETIME = 20; // 20 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
