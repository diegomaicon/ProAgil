
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProAgil.Domain.Identity;

namespace ProAgil.WebAPI.Controllers

{   [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        public readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(IConfiguration config,
                             UserManager<User> userManager,
                             SignInManager<User> signInManager,
                             IMapper mapper)
       {
            _config = config;  
            _userManager= userManager; 
            _signInManager = signInManager; 
            _mapper = mapper; 
       }

       [HttpGet("GetUser")]
       public async Task<IActionResult> GetUser()
       {
           return Ok(new User());
       }  
    }
}