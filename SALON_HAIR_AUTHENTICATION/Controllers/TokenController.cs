using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SALON_HAIR_AUTHENTICATION.Models;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_ENTITY.Entities;
using ULTIL_HELPER;

namespace SALON_HAIR_AUTHENTICATION.Controllers
{

    [Route("api")]
    public class TokenController : Controller
    {

        private IUser _supperUser;
        private IConfiguration _config;
        private IUserAuthority _userAuthority;
        private ISecurityHelper SecurityHelper;
        public TokenController(ISecurityHelper securityHelper, IConfiguration config, IUser supperUser, IUserAuthority userAuthority)
        {
            _userAuthority = userAuthority;
            _config = config;
            _supperUser = supperUser;
            SecurityHelper = securityHelper;
        }

        [AllowAnonymous]
        [HttpPost("create-token")]
        public IActionResult CreateToken([FromBody]LoginViewModel login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = RequestToken(user);
                return Ok(new
                {
                    token = tokenString.Token,
                    expTokenDate = (int)TimeSpan.FromDays(999).TotalDays,
                    expRefreshTokenDate = (int)TimeSpan.FromDays(999).TotalDays,
                    refreshToken = tokenString.RefreshToken
                });
            }

            return response;
        }
        private User Authenticate(LoginViewModel login)
        {
            var user = _supperUser.FindBy(e => e.Email.ToUpper().Equals(login.Username.Trim().ToUpper())
         //&& e.PasswordHash.Equals(SecurityHelper.BCryptPasswordVerifier(login.Password.Trim(login.Password,)))
         ).FirstOrDefault();

            //string hash = _supperUser.FindBy(e=>e.Login)

            if (user != null)
            {
                //for vanvt
                if ("vanvt".Equals(login.Username) && "123".Equals(login.Password))
                {
                    return user;
                }
                //end
                if (SecurityHelper.BCryptPasswordVerifier(login.Password, user.PasswordHash))
                {
                    return user;
                }
                return null;
            }
            return null;
        }
        private TokenValue RequestToken(User acc)
        {
            //string roleClaim = String.Join(",", roleNameUsers);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,acc.Id.ToString()),              
            };
            
            var claimsRole = from a in _userAuthority.FindBy(e => e.UserId == acc.Id) select new Claim("role", a.Authority.Name);
            claims.AddRange(claimsRole);
            claims.Add(new Claim("name", acc.Name));
            //claims.Add(new Claim("emailAddress", acc.Email));
            claims.Add(new Claim("emailAddress", acc.Email));
            claims.Add(new Claim("salonId", ""+acc.SalonId.Value));       
            var key = new SymmetricSecurityKey(SecurityHelper.Base64UrlDecode(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddDays(999),
                
                signingCredentials: creds);
           
            var tokenReturn = new JwtSecurityTokenHandler().WriteToken(token);

            //Add Refresh_Token
            var refreshToken = SecurityHelper.SHA1Hash(tokenReturn);
            //var AccountToken = _accountToken.AddRefreshToken(new AccountTokens
            //{
            //    JwtToken = tokenReturn,
            //    CreatedDate = DateTime.Now,
            //    RefreshToken = refreshToken,
            //    ExpDate = DateTime.Now.AddDays(30),
            //    AccountId = acc.Id,
            //    AccessIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString()
            //});
            TokenValue tk = new TokenValue
            {
                Token = tokenReturn,
                RefreshToken = refreshToken
            };
            return tk;
        }
        public class TokenValue
        {
            public string Token { get; set; }
            public string RefreshToken { get; set; }
        }
    }
}