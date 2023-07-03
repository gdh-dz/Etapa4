using Microsoft.AspNetCore.Mvc;
using BankAPI.Data;
using BankAPI.Data.BankModels;

namespace BankAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly BankContext _context;
    public AccountController(BankContext context)
    {
        _context=context;

    }

    [HttpGet]
    public IEnumerable<Account>Get()
    {
        return _context.Accounts.ToList();
    }

     [HttpGet("{id}")]
    public ActionResult<Account> GetById(int id)
    {
       var Account = _context.Accounts.Find(id);
       if (Account is null)
        return NotFound();

        return Account;
    }

    [HttpPost]
    public IActionResult Create(Account account)
    {
        if (!_context.Clients.Any(c => c.Id == account.ClientId))
            return BadRequest();
            
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new {id =account.Id}, account);
    }

      [HttpPut("{id}")]
    public IActionResult Update(int id, Account account)
    {
       if (id!=account.Id)
            return BadRequest();
        
        var existingAccount = _context.Accounts.Find(id);
        if(existingAccount is null)
            return NotFound();
        
        existingAccount.AccountType = account.AccountType;
        existingAccount.ClientId = account.ClientId;
        existingAccount.Balance = account.Balance;

        _context.SaveChanges();

        return NoContent();
    }

     [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
    var existingAccount = _context.Accounts.Find(id);

        if(existingAccount is null)
            return NotFound();
        
    _context.Accounts.Remove(existingAccount);
    _context.SaveChanges();

    return Ok();
    }





}