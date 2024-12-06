using devicehub_api.Entities;
using devicehub_api.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace devicehub_api.Controllers
{
    [Route("api/funcionarios")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly DeviceHubDbContext _context;

        public FuncionariosController(DeviceHubDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os funcionários cadastrados.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// [
        ///     {
        ///         "id": 1,
        ///         "nome": "Funcionario A",
        ///         "cargo": "Analista",
        ///         "departamentoId": 1
        ///     },
        ///     {
        ///         "id": 2,
        ///         "nome": "Funcionario B",
        ///         "cargo": "Desenvolvedor",
        ///         "departamentoId": 2
        ///     }
        /// ]
        /// </remarks>
        /// <returns>Lista de funcionários</returns>
        /// <response code="200">Retorna a lista de funcionários</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var funcionarios = _context.Funcionarios.ToList();
            return Ok(funcionarios);
        }

        /// <summary>
        /// Retorna um funcionário pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// {
        ///     "id": 1,
        ///     "nome": "Funcionario A",
        ///     "cargo": "Analista",
        ///     "departamentoId": 1
        /// }
        /// </remarks>
        /// <param name="id">ID do funcionário</param>
        /// <returns>Funcionário encontrado</returns>
        /// <response code="200">Retorna o funcionário</response>
        /// <response code="404">Funcionário não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var funcionario = _context.Funcionarios.SingleOrDefault(f => f.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return Ok(funcionario);
        }

        /// <summary>
        /// Cria um novo funcionário.
        /// </summary>
        /// <remarks>
        /// Exemplo de corpo da requisição:
        /// 
        /// {
        ///     "nome": "Funcionario C",
        ///     "cargo": "Gerente",
        ///     "departamentoId": 1
        /// }
        /// 
        /// Exemplo de retorno:
        /// 
        /// {
        ///     "id": 3,
        ///     "nome": "Funcionario C",
        ///     "cargo": "Gerente",
        ///     "departamentoId": 1
        /// }
        /// </remarks>
        /// <param name="funcionario">Dados do novo funcionário</param>
        /// <returns>Funcionário criado</returns>
        /// <response code="201">Funcionário criado com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = funcionario.Id }, funcionario);
        }

        /// <summary>
        /// Atualiza os dados de um funcionário existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de corpo da requisição:
        /// 
        /// {
        ///     "nome": "Funcionario D",
        ///     "cargo": "Analista Sênior",
        ///     "departamentoId": 2
        /// }
        /// </remarks>
        /// <param name="id">ID do funcionário</param>
        /// <param name="input">Novos dados do funcionário</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Funcionário atualizado com sucesso</response>
        /// <response code="404">Funcionário não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, Funcionario input)
        {
            var funcionario = _context.Funcionarios.SingleOrDefault(f => f.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            funcionario.Nome = input.Nome;
            funcionario.Cargo = input.Cargo;
            funcionario.DepartamentoId = input.DepartamentoId;
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Exclui um funcionário pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// Nenhum conteúdo retornado.
        /// </remarks>
        /// <param name="id">ID do funcionário a ser excluído</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Funcionário excluído com sucesso</response>
        /// <response code="404">Funcionário não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var funcionario = _context.Funcionarios.SingleOrDefault(f => f.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            _context.Funcionarios.Remove(funcionario);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
