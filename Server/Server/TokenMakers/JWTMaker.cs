using Microsoft.IdentityModel.Tokens;
using Server.Interfaces.TokenMakerInterfaces;
using Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Server.TokenMakers
{
    public class JWTMaker : ITokenMaker
    {
        public string CreateToken(User user, SymmetricSecurityKey secretKey)
        {
            List<Claim> claims = new List<Claim>();
            if (user.UserType == Enums.EUserType.ADMIN)
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            if (user.UserType == Enums.EUserType.GUEST)
                claims.Add(new Claim(ClaimTypes.Role, "guest"));
            claims.Add(new Claim("Sys_user", "logged_in"));
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:44386", //url servera koji je izdao token
                claims: claims, //claimovi
                expires: DateTime.Now.AddMinutes(60), //vazenje tokena u minutama
                signingCredentials: signinCredentials //kredencijali za potpis
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
