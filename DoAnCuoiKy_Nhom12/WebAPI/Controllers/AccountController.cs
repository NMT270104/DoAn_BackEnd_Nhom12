using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTO;
using WebAPI.Models;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPI.Constants;

namespace WebAPI.Controllers{

    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase{
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration  _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            ApplicationDbContext context,
            ILogger<AccountController> logger,
            IConfiguration configuration,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager
        ){
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<ActionResult> Register(RegisterDTO registerDTO){
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = new User();
                    newUser.UserName = registerDTO.UserName;
                    newUser.Email = registerDTO.Email;
                    var result = await _userManager.CreateAsync(
                        newUser, registerDTO.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation(
                            "User {userName} ({email}) has been created,",
                            newUser.UserName,newUser.Email);
                        if (!await _roleManager.RoleExistsAsync(RoleNames.Customer))
                        {
                            await _roleManager.CreateAsync( 
                                new IdentityRole(RoleNames.Customer));
                        }
                        await _userManager.AddToRoleAsync(newUser,RoleNames.Customer);
                        return StatusCode(210,
                        $"Tài khoản: {newUser.UserName}\n đã được tạo thành công.");
                    }
                    else {throw new Exception(
                        string.Format("Error: {0}", string.Join(' ',
                        result.Errors.Select(e => e.Description)))
                    );}
                }
                else {
                    var details = new ValidationProblemDetails(ModelState);
                    details.Type = "error 400";
                    details.Status = StatusCodes.Status400BadRequest;
                    return new BadRequestObjectResult(details);
                }
            }
            catch (Exception e)
            {
                var exceptionDetails = new ProblemDetails();
                exceptionDetails.Detail = e.Message;
                exceptionDetails.Status = StatusCodes.Status500InternalServerError;
                exceptionDetails.Type = "error 500";
                return StatusCode(StatusCodes.Status500InternalServerError,
                exceptionDetails);
            }
        }


        [HttpPost]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<ActionResult> Login(LoginDTO loginDTO){
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(loginDTO.UserName);
                    if (user == null || !await _userManager.CheckPasswordAsync(user,loginDTO.Password))
                    {
                    throw new Exception("Tài khoản hoặc mật khẩu sai.");
                    }
                    else {
                        var signingCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"])),
                            SecurityAlgorithms.HmacSha256);

                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name,user.UserName));
                        var userRole = await _userManager.GetRolesAsync(user);
                        foreach(var role in userRole){
                            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                        }
                            // claims.AddRange(
                            //     (await _userManager.GetRolesAsync(user))
                            //     .Select(r => Claim(ClaimTypes.Role, r)));
                        var jwtObject = new JwtSecurityToken(
                            issuer: _configuration["JWT:Issuer"],
                            audience: _configuration["JWT:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddSeconds(300),
                            signingCredentials: signingCredentials
                        );
                        var jwtString = new JwtSecurityTokenHandler()
                            .WriteToken(jwtObject);

                        return StatusCode(StatusCodes.Status200OK, jwtString);
                    }
                }
                else {
                    var details = new ValidationProblemDetails(ModelState);
                    details.Type = "bruh";
                    details.Status = StatusCodes.Status400BadRequest;
                    return new BadRequestObjectResult(details);
                }
            }
            catch (Exception e)
            {
                var exceptionDetails = new ProblemDetails();
                exceptionDetails.Detail = e.Message;
                exceptionDetails.Status = StatusCodes.Status401Unauthorized;
                exceptionDetails.Type = "error 401";
                return StatusCode(StatusCodes.Status500InternalServerError,
                exceptionDetails); 
            }
        }

        private Claim Claim(string role, string r)
        {
            throw new NotImplementedException();
        }
    }
}