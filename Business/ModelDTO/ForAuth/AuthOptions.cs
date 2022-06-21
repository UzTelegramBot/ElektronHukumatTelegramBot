using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ModelDTO
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "ThereIsSecurityKey";
        public static SymmetricSecurityKey GetSymmetricSecurityKey()=> 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
