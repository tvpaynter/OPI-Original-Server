using System;
using Microsoft.IdentityModel.Tokens;

namespace StatementIQ.Common.Web.Models
{
    public class AuthenticationOptionsModel
    {
        public Type EventsType { get; set; }
        public string TokenIssuer { get; set; }
        public string TokenAudience { get; set; }
        public SymmetricSecurityKey SecretKey { get; set; }
        public TimeSpan ClockSkew { get; set; }
    }
}