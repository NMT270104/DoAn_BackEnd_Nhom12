using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebAPI.Constants;

using WebAPI.Models;

namespace MyBGList.Controllers
{
    [Authorize(Roles = RoleNames.Administrator)]
    [Route("[controller]/[action]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<SeedController> _logger;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<User> _userManager;

        public SeedController(
            ApplicationDbContext context,
            ILogger<SeedController> logger,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            _context = context;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> AuthData()
        {
            int rolesCreated = 0;
            int usersAddedToRoles = 0;

            if (!await _roleManager.RoleExistsAsync(RoleNames.Customer))
            {
                await _roleManager.CreateAsync(
                    new IdentityRole(RoleNames.Customer));
                rolesCreated++;
            }
            if (!await _roleManager.RoleExistsAsync(RoleNames.Administrator))
            {
                await _roleManager.CreateAsync(
                    new IdentityRole(RoleNames.Administrator));
                rolesCreated++;
            }

            var testCustomer = await _userManager
                .FindByNameAsync("TestCustomer");
            if (testCustomer != null
                && !await _userManager.IsInRoleAsync(
                    testCustomer, RoleNames.Customer))
            {
                await _userManager.AddToRoleAsync(testCustomer, RoleNames.Customer);
                usersAddedToRoles++;
            }

            var testAdministrator = await _userManager
                .FindByNameAsync("TestAdministrator");
            if (testAdministrator != null
                && !await _userManager.IsInRoleAsync(
                    testAdministrator, RoleNames.Administrator))
            {
                await _userManager.AddToRoleAsync(
                    testAdministrator, RoleNames.Customer);
                await _userManager.AddToRoleAsync(
                    testAdministrator, RoleNames.Administrator);
                usersAddedToRoles++;
            }

            return new JsonResult(new
            {
                RolesCreated = rolesCreated,
                UsersAddedToRoles = usersAddedToRoles
            });
        }
    }
}
