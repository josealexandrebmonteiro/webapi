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
    public async Task<ActionResult<DiretorOutputGetIdDTO>> GetById(long id)
    {
        //var diretor = new Diretor(diretorInputGetIdDTO.Nome);
        //await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
        //return Ok(diretor);
        var diretorOutputGetIdDTO = new DiretorOutputGetIdDTO (diretor.Id, diretor.Nome);
        return Ok(diretorOutputGetIdDTO);
    }
    
    [HttpGet]
    public async Task<List<Diretor>> Get() {
        
        return await _context.Diretores.ToListAsync();

    }

    [HttpPost]
    public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputPostDTO) {
        var diretor = new Diretor(diretorInputPostDTO.Nome);
        _context.Diretores.Add(diretor);
        
        await _context.SaveChangesAsync();

        var diretorOutputPostDTO = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);
        return Ok(diretorOutputPostDTO);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DiretorOutputPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputDto) {
        var diretor = new Diretor(diretorInputDto.Nome);
        diretor.Id = id;
        _context.Diretores.Update(diretor);
        await _context.SaveChangesAsync();

        var diretorOutputDto = new DiretorOutputPutDTO(diretor.Id, diretor.Nome);
        return Ok(diretorOutputDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DiretorOutputDeleteDTO>> Delete(long id) {
        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
        _context.Remove(diretor);
        await _context.SaveChangesAsync();
        //return Ok();

        var diretorOutputDeleteDTO = new DiretorOutputDeleteDTO (diretor.Id, diretor.Nome);
        return Ok(diretorOutputDeleteDTO);

    }
}