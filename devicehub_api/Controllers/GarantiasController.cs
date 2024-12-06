using devicehub_api.Entities;
using devicehub_api.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace devicehub_api.Controllers
{
    [Route("api/garantias")]
    [ApiController]
    public class GarantiasController : ControllerBase
    {
        private readonly DeviceHubDbContext _context;

        public GarantiasController(DeviceHubDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todas as garantias cadastradas.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// [
        ///     {
        ///         "id": 1,
        ///         "dataInicio": "2024-01-01",
        ///         "dataFim": "2025-01-01"
        ///     },
        ///     {
        ///         "id": 2,
        ///         "dataInicio": "2023-05-01",
        ///         "dataFim": "2024-05-01"
        ///     }
        /// ]
        /// </remarks>
        /// <returns>Lista de garantias</returns>
        /// <response code="200">Retorna a lista de garantias</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var garantias = _context.Garantias.ToList();
            return Ok(garantias);
        }

        /// <summary>
        /// Retorna uma garantia pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// {
        ///     "id": 1,
        ///     "dataInicio": "2024-01-01",
        ///     "dataFim": "2025-01-01"
        /// }
        /// </remarks>
        /// <param name="id">ID da garantia</param>
        /// <returns>Garantia encontrada</returns>
        /// <response code="200">Retorna a garantia</response>
        /// <response code="404">Garantia não encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var garantia = _context.Garantias.SingleOrDefault(g => g.Id == id);
            if (garantia == null)
            {
                return NotFound();
            }
            return Ok(garantia);
        }

        /// <summary>
        /// Cria uma nova garantia.
        /// </summary>
        /// <remarks>
        /// Exemplo de corpo da requisição:
        /// 
        /// {
        ///     "dataInicio": "2024-01-01",
        ///     "dataFim": "2025-01-01"
        /// }
        /// 
        /// Exemplo de retorno:
        /// 
        /// {
        ///     "id": 3,
        ///     "dataInicio": "2024-01-01",
        ///     "dataFim": "2025-01-01"
        /// }
        /// </remarks>
        /// <param name="garantia">Dados da nova garantia</param>
        /// <returns>Garantia criada</returns>
        /// <response code="201">Garantia criada com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(Garantia garantia)
        {
            _context.Garantias.Add(garantia);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = garantia.Id }, garantia);
        }

        /// <summary>
        /// Atualiza os dados de uma garantia existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de corpo da requisição:
        /// 
        /// {
        ///     "dataInicio": "2024-06-01",
        ///     "dataFim": "2025-06-01"
        /// }
        /// </remarks>
        /// <param name="id">ID da garantia</param>
        /// <param name="input">Novos dados da garantia</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Garantia atualizada com sucesso</response>
        /// <response code="404">Garantia não encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, Garantia input)
        {
            var garantia = _context.Garantias.SingleOrDefault(g => g.Id == id);
            if (garantia == null)
            {
                return NotFound();
            }

            garantia.DataFim = input.DataFim;
            garantia.DataInicio = input.DataInicio;
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Exclui uma garantia pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// Nenhum conteúdo retornado.
        /// </remarks>
        /// <param name="id">ID da garantia a ser excluída</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Garantia excluída com sucesso</response>
        /// <response code="404">Garantia não encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var garantia = _context.Garantias.SingleOrDefault(g => g.Id == id);
            if (garantia == null)
            {
                return NotFound();
            }

            _context.Garantias.Remove(garantia);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
