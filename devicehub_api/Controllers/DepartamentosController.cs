using devicehub_api.Entities;
using devicehub_api.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace devicehub_api.Controllers
{
    [Route("api/departamentos")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly DeviceHubDbContext _context;

        public DepartamentosController(DeviceHubDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os departamentos cadastrados.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// [
        ///     {
        ///         "id": 1,
        ///         "nome": "TI"
        ///     },
        ///     {
        ///         "id": 2,
        ///         "nome": "Financeiro"
        ///     }
        /// ]
        /// </remarks>
        /// <returns>Lista de departamentos</returns>
        /// <response code="200">Retorna a lista de departamentos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var departamentos = _context.Departamentos.ToList();
            return Ok(departamentos);
        }

        /// <summary>
        /// Retorna um departamento pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// {
        ///     "id": 1,
        ///     "nome": "TI"
        /// }
        /// </remarks>
        /// <param name="id">ID do departamento</param>
        /// <returns>Departamento encontrado</returns>
        /// <response code="200">Retorna o departamento</response>
        /// <response code="404">Departamento não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var departamento = _context.Departamentos.SingleOrDefault(d => d.Id == id);
            if (departamento == null)
            {
                return NotFound();
            }
            return Ok(departamento);
        }

        /// <summary>
        /// Cria um novo departamento.
        /// </summary>
        /// <remarks>
        /// Exemplo de corpo da requisição:
        /// 
        /// {
        ///     "nome": "RH"
        /// }
        /// 
        /// Exemplo de retorno:
        /// 
        /// {
        ///     "id": 3,
        ///     "nome": "RH"
        /// }
        /// </remarks>
        /// <param name="departamento">Dados do novo departamento</param>
        /// <returns>Departamento criado</returns>
        /// <response code="201">Departamento criado com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(Departamento departamento)
        {
            _context.Departamentos.Add(departamento);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = departamento.Id }, departamento);
        }

        /// <summary>
        /// Atualiza os dados de um departamento existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de corpo da requisição:
        /// 
        /// {
        ///     "nome": "Marketing"
        /// }
        /// </remarks>
        /// <param name="id">ID do departamento</param>
        /// <param name="input">Novos dados do departamento</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Departamento atualizado com sucesso</response>
        /// <response code="404">Departamento não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, Departamento input)
        {
            var departamento = _context.Departamentos.SingleOrDefault(d => d.Id == id);
            if (departamento == null)
            {
                return NotFound();
            }

            departamento.Nome = input.Nome;
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Exclui um departamento pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// Nenhum conteúdo retornado.
        /// </remarks>
        /// <param name="id">ID do departamento a ser excluído</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Departamento excluído com sucesso</response>
        /// <response code="404">Departamento não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var departamento = _context.Departamentos.SingleOrDefault(d => d.Id == id);
            if (departamento == null)
            {
                return NotFound();
            }

            _context.Departamentos.Remove(departamento);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
