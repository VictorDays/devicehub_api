using devicehub_api.Entities;
using devicehub_api.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace devicehub_api.Controllers
{
    [Route("api/licenca")]
    [ApiController]
    public class LicencasController : ControllerBase
    {
        private readonly DeviceHubDbContext _context;

        public LicencasController(DeviceHubDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todas as licenças cadastradas.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// [
        ///     {
        ///         "id": 1,
        ///         "nome": "Licença A",
        ///         "dataExpiracao": "2025-01-01"
        ///     },
        ///     {
        ///         "id": 2,
        ///         "nome": "Licença B",
        ///         "dataExpiracao": "2026-01-01"
        ///     }
        /// ]
        /// </remarks>
        /// <returns>Lista de licenças</returns>
        /// <response code="200">Retorna a lista de licenças</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var licencas = _context.Licencas.ToList();
            return Ok(licencas);
        }

        /// <summary>
        /// Retorna uma licença pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// {
        ///     "id": 1,
        ///     "nome": "Licença A",
        ///     "dataExpiracao": "2025-01-01"
        /// }
        /// </remarks>
        /// <param name="id">ID da licença</param>
        /// <returns>Licença encontrada</returns>
        /// <response code="200">Retorna a licença</response>
        /// <response code="404">Licença não encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var licenca = _context.Licencas.SingleOrDefault(l => l.Id == id);
            if (licenca == null)
            {
                return NotFound();
            }
            return Ok(licenca);
        }

        /// <summary>
        /// Cria uma nova licença.
        /// </summary>
        /// <remarks>
        /// Exemplo de corpo da requisição:
        /// 
        /// {
        ///     "nome": "Licença C",
        ///     "dataExpiracao": "2027-01-01"
        /// }
        /// 
        /// Exemplo de retorno:
        /// 
        /// {
        ///     "id": 3,
        ///     "nome": "Licença C",
        ///     "dataExpiracao": "2027-01-01"
        /// }
        /// </remarks>
        /// <param name="licenca">Dados da nova licença</param>
        /// <returns>Licença criada</returns>
        /// <response code="201">Licença criada com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(Licenca licenca)
        {
            _context.Licencas.Add(licenca);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = licenca.Id }, licenca);
        }

        /// <summary>
        /// Atualiza os dados de uma licença existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de corpo da requisição:
        /// 
        /// {
        ///     "nome": "Licença D",
        ///     "dataExpiracao": "2028-01-01"
        /// }
        /// </remarks>
        /// <param name="id">ID da licença</param>
        /// <param name="input">Novos dados da licença</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Licença atualizada com sucesso</response>
        /// <response code="404">Licença não encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, Licenca input)
        {
            var licenca = _context.Licencas.SingleOrDefault(l => l.Id == id);
            if (licenca == null)
            {
                return NotFound();
            }

            licenca.Nome = input.Nome;
            licenca.DataExpiracao = input.DataExpiracao;
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Exclui uma licença pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// Nenhum conteúdo retornado.
        /// </remarks>
        /// <param name="id">ID da licença a ser excluída</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Licença excluída com sucesso</response>
        /// <response code="404">Licença não encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var licenca = _context.Licencas.SingleOrDefault(l => l.Id == id);
            if (licenca == null)
            {
                return NotFound();
            }

            _context.Licencas.Remove(licenca);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
