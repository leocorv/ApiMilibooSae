using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
//using SAE_S4_MILIBOO.Models.Type;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TP_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MilibooDBContext? milibooDBContext;
        private readonly IConfiguration _config;
        private readonly IDataRepositoryClient<Client> _datarepositoryClient;
        private List<Client> appClients = new List<Client>
        {
            //new User { FullName = "Vincent COUTURIER", Email = "vince@gmail.com", Password = "1234", UserRole = "Admin" },
            //new User { FullName = "Marc MACHIN", Email = "marc", Password = "1234", UserRole = "User" }
        };
        public LoginController(IConfiguration config, IDataRepositoryClient<Client> datarepositoryclient)
        {
            _config = config;
            _datarepositoryClient= datarepositoryclient;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            IActionResult response = Unauthorized();
            Client user = await AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJwtToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }
        private async Task<Client> AuthenticateUser(Login login)
        {
            var client = await _datarepositoryClient.GetClientByEmail(login.Mail);

            if(client != null && client.Value != null)
            {
                return client.Value;
            }

            return null;

            //return appClients.SingleOrDefault(x => x.Mail.ToUpper() == login.Mail.ToUpper() &&
            //x.Password == login.Password);
        }
        private async Task<string> GenerateJwtToken(Client userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Mail),
                //new Claim("fullName", userInfo.FullName.ToString()),
                //new Claim("role",userInfo.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            Client newClt = userInfo;
            newClt.DerniereConnexion = DateTime.Now;

            await _datarepositoryClient.UpdateAsync(userInfo, newClt);

            //return new JwtSecurityTokenHandler().WriteToken(token);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
