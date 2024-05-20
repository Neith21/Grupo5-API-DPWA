using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PuellaWalletData.Models;
using PuellaWalletData.Repositories.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuellaWalletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IValidator<TransactionModel> _validator;

        public TransactionController(ITransactionRepository transactionRepository, IValidator<TransactionModel> validator)
        {
            _transactionRepository = transactionRepository;
            _validator = validator;
        }


        // GET: api/<TransactionController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();

            return Ok(transactions);
        }

        // GET api/<TransactionController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        // POST api/<TransactionController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransactionModel transaction)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(transaction);
            if (!validationResult.IsValid)
                return UnprocessableEntity(validationResult);

            await _transactionRepository.AddTransactionAsync(transaction);
            return Created();
        }

        // PUT api/<TransactionController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TransactionModel transaction)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(transaction);
            if (!validationResult.IsValid)
                return UnprocessableEntity(validationResult);

            if (id != transaction.IdTransaction)
            {
                return BadRequest("ID mismatch");
            }

            var transactionEditable = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transactionEditable == null)
            {
                return NotFound();
            }

            await _transactionRepository.EditTransactionAsync(transaction);

            var updatedTransaction = await _transactionRepository.GetTransactionByIdAsync(id);
            return Ok(updatedTransaction);
        }

        // DELETE api/<TransactionController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _transactionRepository.DeleteTransactionAsync(id);
            return Ok();
        }
    }
}
