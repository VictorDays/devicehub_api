using devicehub_api.Entities;
using devicehub_api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace devicehub_api.Controllers
{
    [Route("api/ativos")]
    [ApiController]
    public class AtivosController : ControllerBase
    {
        private readonly DeviceHubDbContext _context;

        public AtivosController(DeviceHubDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os ativos cadastrados no sistema.
        /// </summary>
        /// <returns>Uma lista de ativos.</returns>
        /// <response code="200">Retorna a lista de ativos.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Ativo>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var ativos = _context.Ativos.ToList();
            return Ok(ativos);
        }

        /// <summary>
        /// Busca um ativo pelo ID.
        /// </summary>
        /// /// <remarks>
        /// Retorno: 
        /// Exemplo de retorno com sucesso (200 OK):
        /// {
        ///     "id": 1,
        ///     "nome": "Notebook Dell",
        ///     "numeroSerie": "12345-ABC",
        ///     "descricao": "Notebook de alto desempenho",
        ///     "departamentoId": 2,
        ///     "fornecedorId": 3
        /// }
        /// Exemplo de retorno com erro (404 Not Found):
        /// {
        ///     "message": "Ativo não encontrado."
        /// }
        /// </remarks>
        /// <param name="id">O ID do ativo a ser buscado.</param>
        /// <returns>O ativo correspondente ao ID informado.</returns>
        /// <response code="200">Retorna o ativo correspondente.</response>
        /// <response code="404">Caso o ativo não seja encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Ativo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public IActionResult GetById(int id)
        {
            var ativo = _context.Ativos.SingleOrDefault(a => a.Id == id);
            if (ativo == null)
            {
                return NotFound(new { Message = "Ativo não encontrado." });
            }

            return Ok(ativo);
        }

        /// <summary>
        /// Cadastra um novo ativo.
        /// </summary>
        /// <param name="ativo">Os dados do ativo a ser cadastrado.</param>
        /// <returns>O ativo cadastrado.</returns>
        /// <response code="201">Retorna o ativo recém-criado.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Ativo), StatusCodes.Status201Created)]
        /// <remarks>
        /// Retorno:
        /// Exemplo de retorno (201 Created):
        /// {
        ///     "id": 3,
        ///     "nome": "Teclado Mecânico",
        ///     "numeroSerie": "54321-CBA",
        ///     "descricao": "Teclado RGB com switches mecânicos",
        ///     "departamentoId": 1,
        ///     "fornecedorId": 4
        /// }
        /// </remarks>
        public IActionResult Post(Ativo ativo)
        {
            _context.Ativos.Add(ativo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = ativo.Id }, ativo);
        }

        /// <summary>
        /// Atualiza os dados de um ativo existente.
        /// </summary>
        /// <param name="id">O ID do ativo a ser atualizado.</param>
        /// <param name="input">Os novos dados do ativo.</param>
        /// <response code="204">Indica que o ativo foi atualizado com sucesso.</response>
        /// <response code="404">Caso o ativo não seja encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        /// <remarks>
        /// Retorno:
        /// Exemplo de retorno com sucesso (204 No Content):
        /// Nenhum conteúdo é retornado, pois a operação foi bem-sucedida e não há dados para mostrar.
        /// </remarks>
        public IActionResult Update(int id, Ativo input)
        {
            var ativo = _context.Ativos.SingleOrDefault(a => a.Id == id);
            if (ativo == null)
            {
                return NotFound(new { Message = "Ativo não encontrado." });
            }

            ativo.Nome = input.Nome;
            ativo.NumeroSerie = input.NumeroSerie;
            ativo.Descricao = input.Descricao;
            ativo.DepartamentoId = input.DepartamentoId;
            ativo.FornecedorId = input.FornecedorId;
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Exclui um ativo existente.
        /// </summary>
        /// <param name="id">O ID do ativo a ser excluído.</param>
        /// <response code="204">Indica que o ativo foi excluído com sucesso.</response>
        /// <response code="404">Caso o ativo não seja encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        /// <remarks>
        /// Retorno:
        /// Exemplo de retorno com sucesso (204 No Content):
        /// Nenhum conteúdo é retornado, pois a operação foi bem-sucedida e o ativo foi excluído.
        /// </remarks>
        public IActionResult Delete(int id)
        {
            var ativo = _context.Ativos.SingleOrDefault(a => a.Id == id);
            if (ativo == null)
            {
                return NotFound(new { Message = "Ativo não encontrado." });
            }

            _context.Ativos.Remove(ativo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
