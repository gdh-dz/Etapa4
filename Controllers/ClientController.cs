using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;


namespace BankAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class ClientController: ControllerBase
{
    private readonly ClientService _service;
    public ClientController(ClientService client)
    {
       _service=client;
    }

    [HttpGet]
    public IEnumerable<Client> Get()
    {
        return _service.GetAll();
    }


    [HttpGet("{id}")]
    public ActionResult<Client> GetById(int id)
    {
       var client = _service.GetById(id);
       if (client is null)
        return NotFound();

        return client;
    }

    [HttpPost]
    public IActionResult Create(Client client)
    {
        var newclient = _service.Create(client);
        return CreatedAtAction(nameof(GetById), new {id =newclient.Id}, newclient);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Client client)
    {
       if (id!=client.Id)
            return BadRequest();
        
        var clientToUpdate = _service.GetById(id);

        if(clientToUpdate is not null)
        {
            _service.Update(id, client);
            return NoContent();
        }
        else{
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
    var clientToDelete = _service.GetById(id);

        if(clientToDelete is not null)
        {
            _service.Delete(id);
            return Ok();
        }
        else{
            return NotFound();
        }
    }
}