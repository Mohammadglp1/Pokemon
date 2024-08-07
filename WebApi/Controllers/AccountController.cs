using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ThePokemonProject;
[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> usermanager, ITokenService tokenService, SignInManager<AppUser> signInManager)
    {
        _userManager = usermanager;
        _tokenService = tokenService;
        _signInManager = _signInManager;
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await _userManager.Users.FirstOrDefaultAsync(x =>
        x.UserName == loginDto.Username.ToLower());
        if (user == null) return Unauthorized("Invalid User");
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized("Username/Password Invalid");
        return Ok(
          new NewUserDto
          {
              UserName = user.UserName,
              Email = user.Email,
              Token = _tokenService.CreateToken(user)
          }
        );
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var appUser = new AppUser
        {
            UserName = registerDto.Username,
            Email = registerDto.Email
        };


        var createdUser = await _userManager.CreateAsync(appUser, registerDto.PassWord);
        if (createdUser.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

            if (roleResult.Succeeded)
            {
                return Ok(
                    new NewUserDto
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser)
                    }
                );
            }
            else
            {
                return StatusCode(500, roleResult.Errors);
            }
        }
        else
        {
            return StatusCode(500, createdUser.Errors);
        }
    }




}

