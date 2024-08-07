using ThePokemonProject.Models;

namespace ThePokemonProject;

public interface ITokenService
{
string CreateToken (AppUser user);
}
