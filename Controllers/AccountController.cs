using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;


namespace BankAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class AccountController: ControllerBase
{
    private readonly AccountService _service;
    public AccountController(AccountService account)
    {
       _service=account;
    }

    [HttpGet]
    public IEnumerable<Account> Get()
    {
        return _service.GetAll();
    }


    [HttpGet("{id}")]
    public ActionResult<Account> GetById(int id)
    {
       var Account = _service.GetById(id);
       if (Account is null)
        return NotFound();

        return Account;
    }

    [HttpPost]
    public IActionResult Create(Account account)
    {
        var newAccount = _service.Create(account);
        return CreatedAtAction(nameof(GetById), new {id =newAccount.Id}, newAccount);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Account account)
    {
       if (id!=account.Id)
            return BadRequest();
        
        var AccountToUpdate = _service.GetById(id);

        if(AccountToUpdate is not null)
        {
            _service.Update(id, account);
            return NoContent();
        }
        else{
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
    var AccountToDelete = _service.GetById(id);

        if(AccountToDelete is not null)
        {
            _service.Delete(id);
            return Ok();
        }
        else{
            return NotFound();
        }
    }
}