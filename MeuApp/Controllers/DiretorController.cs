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
public class DiretorController : ControllerBase {
    private readonly ApplicationDbContext _context;
    private readonly IDiretorService _diretorService;

    public DiretorController(ApplicationDbContext context, IDiretorService diretorService) {
        //_context = context;
        _diretorService = diretorService;
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
        var diretor = await _diretorService.GetById(id);
        var diretorOutputGetIdDTO = new DiretorOutputGetIdDTO(diretor.Id, diretor.Nome);
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
    public async Task<ActionResult<DiretorListOutputGetAllDTO>> Get(CancellationToken cancellationToken, int limit = 5, int page = 1) {
        return await _diretorService.GetByPageAsync(limit, page, cancellationToken);
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
    /// <param name="diretorInputDto">Nome do diretor</param>
    /// <returns>O diretor criado</returns>
    /// <response code="200">Diretor foi criado com sucesso</response>
    /// <response code="500">Erro interno inesperado</response>
    /// <response code="400">Erro de validação</response>

    [HttpPost]
    public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputDto) {
        var diretor = await _diretorService.Cria(new Diretor(diretorInputDto.Nome));
        var diretorOutputDto = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);
        return Ok(diretorOutputDto);
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
    public async Task<ActionResult<DiretorOutputPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputDto) {
        var diretor = await _diretorService.Atualiza(new Diretor(diretorInputDto.Nome), id);
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
    public async Task<ActionResult> Delete(long id) {
        await _diretorService.Exclui(id);
        return Ok();
    }
}