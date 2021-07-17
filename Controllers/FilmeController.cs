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

    /// <summary>
    /// Localizar um Filme
    /// </summary>
    /// <param name="id">Id Do Filme</param>   
    /// <returns>O Filme Encontrado</returns>
    /// <response code="200">Filme foi Encontrado com sucesso</response>
    /// <response code="500">Erro interno inesperado</response>
    /// <response code="400">Erro de validação</response>

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

     /// <summary>
    /// Lista de Todos os Filmes
    /// </summary>
    /// <returns>Lista com todos Filmes encontrados </returns>
    /// <response code="200">Lista Retornada com sucesso</response>
    /// <response code="500">Erro interno inesperado</response>
    /// <response code="400">Erro de validação</response>
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

    /// <summary>
    /// Cria um Filme
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /filme
    ///     {
    ///        "nome": "Harry Potter e a Ordem da Fenix",
    ///        "diretorId": 1
    ///     }
    ///
    /// </remarks>
    /// <param name="filmeInputPostDTO">Nome e Diretor do Filme</param>
    /// <returns>Filme criado</returns>
    /// <response code="200">Filme foi criado com sucesso</response>
    /// <response code="500">Erro interno inesperado</response>
    /// <response code="400">Erro de validação</response>

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

    /// <summary>
    /// Altera um Filme
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /filme/ {id}
    ///     {
    ///        "nome": "Harry Potter e a Ordem da Fenix",
    ///        "diretorId": 1
    ///     }
    ///
    /// </remarks>
    /// <param name="id">Id Do Filme</param>
    /// <param name="filmeInputDto">Nome e Diretor do Filme</param>
    /// <returns>Filme alterado</returns>
    /// <response code="200">Filme foi alterado com sucesso</response>
    /// <response code="500">Erro interno inesperado</response>
    /// <response code="400">Erro de validação</response>

    [HttpPut("{id}")]
    public async Task<ActionResult<FilmeOutputPutDTO>> Put(long id, [FromBody] FilmeInputPutDTO filmeInputDto) {
        var filme = new Filme(filmeInputDto.Titulo, filmeInputDto.DiretorId);

        if (filmeInputDto.DiretorId == 0) {
            return NotFound("Id do diretor é inválido!");
        }

        filme.Id = id;
        _context.Filmes.Update(filme);
        await _context.SaveChangesAsync();

        var filmeOutputPutDTO = new FilmeOutputPutDTO(filme.Id, filme.Titulo);
        return Ok(filmeOutputPutDTO);
    }


    /// <summary>
    /// Apaga um Filme
    /// </summary>
    /// <param name="id">Id Do Filme</param>   
    /// <returns>Filme Apagado</returns>
    /// <response code="200">Filme foi apagado com sucesso</response>
    /// <response code="500">Erro interno inesperado</response>
    /// <response code="400">Erro de validação</response>

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