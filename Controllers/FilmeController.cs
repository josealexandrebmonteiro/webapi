using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase{
    private readonly ApplicationDbContext _context;  


    public FilmeController(ApplicationDbContext context) {
        _context = context;

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Filme>> GetById(long id)
    {
         var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);
         return Ok(filme);
    }
    
    [HttpGet]
    public async Task<List<Filme>> Get()
    {
        return await _context.Filmes.ToListAsync();
    } 

    [HttpPost]
    public async Task <ActionResult<FilmeOutputPostDTO>> Post ([FromBody] FilmeInputPostDTO filmeInputPostDTO){        
        var filme = new Filme (filmeInputPostDTO.Titulo, filmeInputPostDTO.DiretorID);
        _context.Filmes.Add(filme);
        await _context.SaveChangesAsync();
        var filmeOutputPostDTO = new FilmeOutputPostDTO(filme.Id, filme.Titulo, filme.DiretorID);
        return Ok(filmeOutputPostDTO);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Filme>> Put(int id, [FromBody] Filme filme)
    {
        filme.Id = id;
        _context.Filmes.Update(filme);
        await _context.SaveChangesAsync();

        return Ok(filme);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);
      _context.Remove(filme);
      await _context.SaveChangesAsync();

      return Ok(filme);
    }
}