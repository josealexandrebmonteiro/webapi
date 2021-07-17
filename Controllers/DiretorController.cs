using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;


[ApiController]
[Route("[controller]")]
public class DiretorController : ControllerBase{
    private readonly ApplicationDbContext _context;  

//inejtar classe de contexto (inejção de dependencia)
    public DiretorController(ApplicationDbContext context) {
        _context = context;

    }

    /// <summary>
    /// Localizar um Diretor
    /// </summary>
    /// <param name="id">Id Do Diretor</param>   
    /// <returns>O diretor Encontrado</returns>
    /// <response code="200">Diretor foi Encontrado com sucesso</response>
    /// <response code="500">Erro interno inesperado</response>
    /// <response code="400">Erro de validação</response>

    [HttpGet("{id}")]
    public async Task<ActionResult<DiretorOutputGetIdDTO>> GetById(long id)
    {
    
        
            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
            if (diretor == null){
                return NotFound("Diretor Não Encontrado");
            }
            var diretorOutputGetIdDTO = new DiretorOutputGetIdDTO (diretor.Id, diretor.Nome);
            return Ok(diretorOutputGetIdDTO);      
        
            
               
    }

    /// <summary>
    /// Lista de Todos os Diretores
    /// </summary>
    /// <returns>Lista com todos diretores encontrados </returns>
    /// <response code="200">Lista Retornada com sucesso</response>
    /// <response code="500">Erro interno inesperado</response>
    /// <response code="400">Erro de validação</response>
    
    [HttpGet]
    public async Task<ActionResult<List<DiretorOutputGetDTO>>> Get() 
    {
    
            
        var diretores = await _context.Diretores.ToListAsync();
        var outputDTOList = new List<DiretorOutputGetDTO>();
        
        foreach (Diretor diretor in diretores){
            
            outputDTOList.Add (new DiretorOutputGetDTO (diretor.Id,diretor.Nome));
        } 

        if (!diretores.Any()) { //! inverte a condição
            return NotFound ("Não Existe Diretores Cadastrados");
        }
        
        return outputDTOList;
        
    }

    /// <summary>
    /// Cria um diretor
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /diretor
    ///     {
    ///        "nome": "David Yates"
    ///     }
    ///
    /// </remarks>
    /// <param name="diretorInputPostDTO">Nome do diretor</param>
    /// <returns>O diretor criado</returns>
    /// <response code="200">Diretor foi criado com sucesso</response>
    /// <response code="500">Erro interno inesperado</response>
    /// <response code="400">Erro de validação</response>

    [HttpPost]
    public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputPostDTO)     
    {
    
        
        
        var diretor = new Diretor(diretorInputPostDTO.Nome);
        _context.Diretores.Add(diretor);
        
        await _context.SaveChangesAsync();

        var diretorOutputPostDTO = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);
        return Ok(diretorOutputPostDTO);        
        
    }
    
    /// <summary>
    /// Altera um Diretor
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /diretor/ {id}
    ///     {
    ///        "nome": "David Yates",
    ///     }
    ///
    /// </remarks>
    /// <param name="id">Id Do Diretor</param>
    /// <param name="diretorInputDto">Nome do diretor</param>
    /// <returns>O diretor alterado</returns>
    /// <response code="200">Diretor foi alterado com sucesso</response>
    /// <response code="500">Erro interno inesperado</response>
    /// <response code="400">Erro de validação</response>

    [HttpPut("{id}")]
    public async Task<ActionResult<DiretorOutputPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputDto) 
    {   
        var diretor = new Diretor(diretorInputDto.Nome);     
        diretor.Id = id;     
        _context.Diretores.Update(diretor);
        await _context.SaveChangesAsync();

        var diretorOutputDto = new DiretorOutputPutDTO(diretor.Id, diretor.Nome);
        return Ok(diretorOutputDto);     
        
        
        
    }

    /// <summary>
    /// Apaga um Diretor
    /// </summary>
    /// <param name="id">Id Do Diretor</param>   
    /// <returns>O diretor Apagado</returns>
    /// <response code="200">Diretor foi apagado com sucesso</response>
    /// <response code="500">Erro interno inesperado</response>
    /// <response code="400">Erro de validação</response>

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)  {
       
        
        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
        _context.Remove(diretor);
        await _context.SaveChangesAsync();
        return Ok();
    }
}