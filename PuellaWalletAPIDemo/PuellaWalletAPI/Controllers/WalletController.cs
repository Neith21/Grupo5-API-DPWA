using Microsoft.AspNetCore.Mvc;
using PuellaWalletData.Data;
using PuellaWalletData.Repositories.Wallets;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuellaWalletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;

        public WalletController(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        // GET: api/<WalletController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var wallets = await _walletRepository.GetAllWalletsAsync();

            return Ok(wallets);
        }

        // GET api/<WalletController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WalletController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WalletController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WalletController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
