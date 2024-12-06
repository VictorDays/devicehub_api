using devicehub_api.Entities;
using devicehub_api.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace devicehub_api.Controllers
{
    [Route("api/manutencoes")]
    [ApiController]
    public class ManutencoesController : ControllerBase
    {
        private readonly DeviceHubDbContext _context;

        public ManutencoesController(DeviceHubDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todas as manutenções cadastradas.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// [
        ///     {
        ///         "id": 1,
        ///         "descricao": "Troca de peça",
        ///         "data": "2024-01-01"
        ///     },
        ///     {
        ///         "id": 2,
        ///         "descricao": "Manutenção preventiva",
        ///         "data": "2024-02-01"
        ///     }
        /// ]
        /// </remarks>
        /// <returns>Lista de manutenções</returns>
        /// <response code="200">Retorna a lista de manutenções</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var manutencoes = _context.Manutencoes.ToList();
            return Ok(manutencoes);
        }

        /// <summary>
        /// Retorna uma manutenção pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// {
        ///     "id": 1,
        ///     "descricao": "Troca de peça",
        ///     "data": "2024-01-01"
        /// }
        /// </remarks>
        /// <param name="id">ID da manutenção</param>
        /// <returns>Manutenção encontrada</returns>
        /// <response code="200">Retorna a manutenção</response>
        /// <response code="404">Manutenção não encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var manutencao = _context.Manutencoes.SingleOrDefault(m => m.Id == id);
            if (manutencao == null)
            {
                return NotFound();
            }
            return Ok(manutencao);
        }

        /// <summary>
        /// Cria uma nova manutenção.
        /// </summary>
        /// <remarks>
        /// Exemplo de corpo da requisição:
        /// 
        /// {
        ///     "descricao": "Manutenção preventiva",
        ///     "data": "2024-03-01"
        /// }
        /// 
        /// Exemplo de retorno:
        /// 
        /// {
        ///     "id": 3,
        ///     "descricao": "Manutenção preventiva",
        ///     "data": "2024-03-01"
        /// }
        /// </remarks>
        /// <param name="manutencao">Dados da nova manutenção</param>
        /// <returns>Manutenção criada</returns>
        /// <response code="201">Manutenção criada com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(Manutencao manutencao)
        {
            _context.Manutencoes.Add(manutencao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = manutencao.Id }, manutencao);
        }

        /// <summary>
        /// Atualiza os dados de uma manutenção existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de corpo da requisição:
        /// 
        /// {
        ///     "descricao": "Manutenção corretiva",
        ///     "data": "2024-04-01"
        /// }
        /// </remarks>
        /// <param name="id">ID da manutenção</param>
        /// <param name="input">Novos dados da manutenção</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Manutenção atualizada com sucesso</response>
        /// <response code="404">Manutenção não encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, Manutencao input)
        {
            var manutencao = _context.Manutencoes.SingleOrDefault(m => m.Id == id);
            if (manutencao == null)
            {
                return NotFound();
            }

            manutencao.Descricao = input.Descricao;
            manutencao.Data = input.Data;
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Exclui uma manutenção pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// Nenhum conteúdo retornado.
        /// </remarks>
        /// <param nome="id">ID da manutenção a ser excluída</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Manutenção excluída com sucesso</response>
        /// <response code="404">Manutenção não encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var manutencao = _context.Manutencoes.SingleOrDefault(m => m.Id == id);
            if (manutencao == null)
            {
                return NotFound();
            }

            _context.Manutencoes.Remove(manutencao);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
