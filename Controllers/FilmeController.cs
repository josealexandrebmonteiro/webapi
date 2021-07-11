using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase{
    private readonly ApplicationDbContext _context;  


    public FilmeController(ApplicationDbContext context) {
        _context = context;

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FilmeOutputGetIdDTO>> GetById(long id)
    {
        
        var filme = await _context.Filmes.Include(filme=>filme.Diretor).FirstOrDefaultAsync(filme => filme.Id == id);
        if (filme == null){
            return NotFound ("Filme Não Encontrado");
        }
        var filmeOutputGetIdDTO = new FilmeOutputGetIdDTO (filme.Id, filme.Titulo, filme.Diretor.Nome);
        return Ok(filmeOutputGetIdDTO);       
        
    }
    
    [HttpGet]
    public async Task<ActionResult<List<FilmeOutPutGetDTO>>> Get()  {

        

        var filmes =  await _context.Filmes.ToListAsync();
        
        var outputDTOlist = new List<FilmeOutPutGetDTO>();
        foreach (Filme filme in filmes) {
            outputDTOlist.Add(new FilmeOutPutGetDTO(filme.Id, filme.Titulo));
        }
        
        if (!filmes.Any()) { //! inverte a condição
            return NotFound ("Não Existe Diretores Cadastrados");
        }
        

        return outputDTOlist;      
               
        
        
    } 

    [HttpPost]
    public async Task <ActionResult<FilmeOutputPostDTO>> Post ([FromBody] FilmeInputPostDTO filmeInputPostDTO){        
        
        var diretor = await _context.Diretores.FirstOrDefaultAsync (diretor => diretor.Id == filmeInputPostDTO.DiretorId);
        if (diretor == null){
            return NotFound("Diretor Não Encontrado");
        }
        var filme = new Filme(filmeInputPostDTO.Titulo, diretor.Id);
        _context.Filmes.Add (filme);
        await _context.SaveChangesAsync();
        var filmeOutputPostDTO = new FilmeOutputPostDTO(filme.Id, filme.Titulo);
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
      if (filme == null){
        return NotFound ("Filme Não Encontrado");
      }
      _context.Remove(filme);
      await _context.SaveChangesAsync();

      return Ok(filme);    
      
    }
}