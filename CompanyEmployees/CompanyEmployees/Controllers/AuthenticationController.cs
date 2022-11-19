using AutoMapper;
using CompanyEmployees.ActionFilters;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AuthenticationController(ILoggerManager logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            foreach (var role in userForRegistration.Roles)
            {
                var roleisexist = await _roleManager.RoleExistsAsync(role);
                if (!roleisexist)
                {
                    await _roleManager.CreateAsync(
                        new IdentityRole
                        {
                            Name = role,
                            NormalizedName = role.Normalize()
                        }
                        );
                }
            }
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return StatusCode(201);
        }
    }
}