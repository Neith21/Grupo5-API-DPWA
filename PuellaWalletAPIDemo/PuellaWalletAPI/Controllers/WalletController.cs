using Microsoft.AspNetCore.Mvc;
using PuellaWalletData.Data;
using PuellaWalletData.Models;
using PuellaWalletData.Repositories.Wallets;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuellaWalletAPI.Controllers
{
    [Route("api/Wallets")]
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
        public async Task<IActionResult> Get(int id)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }
            return Ok(wallet);
        }

        // POST api/<WalletController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WalletModel wallet)
        {
            await _walletRepository.AddWalletAsync(wallet);
            return Created();
        }

        // PUT api/<WalletController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] WalletModel wallet)
        {
            if (id != wallet.IdWallet)
            {
                return BadRequest("ID mismatch");
            }

            var walletEditable = await _walletRepository.GetWalletByIdAsync(id);
            if (walletEditable == null)
            {
                return NotFound();
            }

            await _walletRepository.EditWalletAsync(wallet);

            // Recuperar el recurso actualizado
            var updatedWallet = await _walletRepository.GetWalletByIdAsync(id);
            return Ok(updatedWallet);
        }

        // DELETE api/<WalletController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> edit(int id)
        {
            await _walletRepository.DeleteWalletAsync(id);
            return Ok();
        }
    }
}
