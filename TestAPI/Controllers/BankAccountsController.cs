using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBOL;
using TestDAL;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        readonly ITestDb testDb;
        private readonly ILogger<BankAccountsController> logger;

        public BankAccountsController(ITestDb testDb, ILogger<BankAccountsController> logger)
        {
            this.testDb = testDb;
            this.logger = logger;
        }
         
        [HttpGet("getByUserId/{id}")]
        public async Task<IActionResult> GetByUserId(string id)
        {
            var result = await testDb.GetByUserId(id).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await testDb.GetById(id).FirstOrDefaultAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BankAccount account)
        {
            try
            {
                    account.CreatedOn = DateTime.Now;
                    var result = await testDb.Create(account);
                    return CreatedAtAction("GetById", new { id = account.AccountId }, account);
            }
            catch (Exception E)
            {
                return ErrorMsg(E);
            }
        }
             
        [HttpPut("deposit/{id}")]
        public async Task<IActionResult> Deposit(int id, decimal amount)
        {
            try
            {
                    if (testDb.GetById(id) != null)
                    {
                        var result = await testDb.Deposit(id, amount);
                        return Ok(DateTime.Now);
                    }
                    else
                    {
                        return NotFound();
                    }
            }
            catch (Exception E)
            {
                return ErrorMsg(E);
            }
        }

        [HttpPut("withdraw/{id}")]
        public async Task<IActionResult> Withdraw(int id, decimal amount)
        {
            try
            {
                if (testDb.GetById(id) != null)
                {
                    var result = await testDb.Withdraw(id, amount);
                    return Ok(DateTime.Now);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception E)
            {
                return ErrorMsg(E);
            }
        }

        private IActionResult ErrorMsg(Exception E)
        {
            logger.LogError($"Error: {E}");
            var msg = "System encountered error. Please contact the Bank Admin, thank you!";
            return StatusCode(500, msg);
        }
    }
}
