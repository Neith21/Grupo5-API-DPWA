using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PuellaWalletData.Models;
using PuellaWalletData.Repositories.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuellaWalletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserModel> _validator;

        public UserController(IUserRepository userRepository, IValidator<UserModel> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }


        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var users = await _userRepository.GetAllUsersAsync();
           return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserModel user)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(user);
            if (!validationResult.IsValid)
                return UnprocessableEntity(validationResult);
            await _userRepository.AddUserAsync(user);
            return Created();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserModel user)
        {
            var userEditable = await _userRepository.GetUserByIdAsync(id);
            if (userEditable == null)
            {
                return NotFound();
            }

            ValidationResult validationResult = await _validator.ValidateAsync(user);
            if (!validationResult.IsValid)
                return UnprocessableEntity(validationResult);

            await _userRepository.EditUserAsync(user);
            return Accepted();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> edit(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return Ok();
        }
    }
}
