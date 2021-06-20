using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class DiretorController : ControllerBase{
    private readonly ApplicationDbContext _context;  

//inejtar classe de contexto (inejção de dependencia)
    public DiretorController(ApplicationDbContext context) {
        _context = context;

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Diretor>> GetById(long id)
    {
         var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
         return Ok(diretor);
    }
    
    [HttpGet]
    public async Task<List<Diretor>> Get()
    {
        return await _context.Diretores.ToListAsync();
    } 

    [HttpPost]
    public async Task <ActionResult<Diretor>> Post ([FromBody] Diretor diretor){        
        _context.Diretores.Add(diretor);
        await _context.SaveChangesAsync();

        return Ok (diretor);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Diretor>> Put(long id, [FromBody] Diretor diretor)
    {
        diretor.Id = id;
        _context.Diretores.Update(diretor);
        await _context.SaveChangesAsync();

        return Ok(diretor);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
      var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
      _context.Remove(diretor);
      await _context.SaveChangesAsync();

      return Ok(diretor);
    }
}