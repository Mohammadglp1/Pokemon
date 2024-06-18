// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.IdentityModel.Tokens;
// using ThePokemonProject.Dto;
// using ThePokemonProject.Models;

// namespace ThePokemonProject;

// public class TokenService : ITokenService
// {
//     private readonly IConfiguration _configuration;
//     private readonly SymmetricSecurityKey _key;
//     public TokenService(IConfiguration configuration)
// {
//         configuration = _configuration;
//         _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration("JWT:SigningKey")));
//     }
//     public string CreateToken(Pokemon pokemon)
//     {
//         var claims = new List<Claim>
//         {
//           new Claim(JwtRegisteredClaimNames.GivenName,pokemon.Name)
          
//           new Claim(JwtRegisteredClaimNames.Birthdate,pokemon.BirthDate)
//         }
//     }
// }
