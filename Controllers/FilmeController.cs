using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase{
    private readonly ApplicationDbContext _context;  
    private readonly IFilmeService _filmeService;


    public FilmeController(ApplicationDbContext context, IFilmeService filmerService) {
        _context = context;
        _filmeService = filmerService;

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
        var filme = await _filmeService.GetById(id);
        var filmeOutputGetIdDTO = new FilmeOutputGetIdDTO(filme.Id, filme.Titulo, filme.Diretor.Nome);
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
    public async Task<ActionResult<FilmeListOutputGetAllDTO>> Get(CancellationToken cancellationToken, int limit = 5, int page = 1) {
        return await _filmeService.GetByPageAsync(limit, page, cancellationToken);
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
    public async Task <ActionResult<FilmeOutputPostDTO>> Post ([FromBody] FilmeInputPostDTO filmeInputPostDTO) {
        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == filmeInputPostDTO.DiretorId); //verificar
        var filme = await _filmeService.Cria(new Filme (filmeInputPostDTO.Titulo, diretor.Id));
        var filmeOutputDto = new FilmeOutputPostDTO (filme.Id, filme.Titulo);
        return Ok(filmeOutputDto);
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
    public async Task<ActionResult<FilmeOutputPutDTO>> Put(long id, [FromBody] FilmeInputPutDTO filmeInputDto)  {
        var filme = await _filmeService.Atualiza(new Filme(filmeInputDto.Titulo, filmeInputDto.DiretorId), id);
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
    public async Task<ActionResult> Delete(int id){
        await _filmeService.Exclui(id);
        return Ok();
    }
}