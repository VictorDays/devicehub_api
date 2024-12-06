using devicehub_api.Entities;
using devicehub_api.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace devicehub_api.Controllers
{
    [Route("api/fornecedores")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly DeviceHubDbContext _context;

        public FornecedoresController(DeviceHubDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os fornecedores cadastrados.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// [
        ///     {
        ///         "id": 1,
        ///         "nome": "Fornecedor A",
        ///         "cnpj": "00.000.000/0001-00",
        ///         "contato": "contato@fornecedora.com",
        ///         "endereco": "Rua A, 123"
        ///     },
        ///     {
        ///         "id": 2,
        ///         "nome": "Fornecedor B",
        ///         "cnpj": "11.111.111/0001-11",
        ///         "contato": "suporte@fornecedorb.com",
        ///         "endereco": "Avenida B, 456"
        ///     }
        /// ]
        /// </remarks>
        /// <returns>Lista de fornecedores</returns>
        /// <response code="200">Retorna a lista de fornecedores</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var fornecedores = _context.Fornecedores.ToList();
            return Ok(fornecedores);
        }

        /// <summary>
        /// Retorna um fornecedor pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// {
        ///     "id": 1,
        ///     "nome": "Fornecedor A",
        ///     "cnpj": "00.000.000/0001-00",
        ///     "contato": "contato@fornecedora.com",
        ///     "endereco": "Rua A, 123"
        /// }
        /// </remarks>
        /// <param name="id">ID do fornecedor</param>
        /// <returns>Fornecedor encontrado</returns>
        /// <response code="200">Retorna o fornecedor</response>
        /// <response code="404">Fornecedor não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var fornecedor = _context.Fornecedores.SingleOrDefault(f => f.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return Ok(fornecedor);
        }

        /// <summary>
        /// Cria um novo fornecedor.
        /// </summary>
        /// <remarks>
        /// Exemplo de corpo da requisição:
        /// 
        /// {
        ///     "nome": "Fornecedor C",
        ///     "cnpj": "22.222.222/0001-22",
        ///     "contato": "vendas@fornecedor.com",
        ///     "endereco": "Rua C, 789"
        /// }
        /// 
        /// Exemplo de retorno:
        /// 
        /// {
        ///     "id": 3,
        ///     "nome": "Fornecedor C",
        ///     "cnpj": "22.222.222/0001-22",
        ///     "contato": "vendas@fornecedor.com",
        ///     "endereco": "Rua C, 789"
        /// }
        /// </remarks>
        /// <param name="fornecedor">Dados do novo fornecedor</param>
        /// <returns>Fornecedor criado</returns>
        /// <response code="201">Fornecedor criado com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = fornecedor.Id }, fornecedor);
        }

        /// <summary>
        /// Atualiza os dados de um fornecedor existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de corpo da requisição:
        /// 
        /// {
        ///     "nome": "Fornecedor D",
        ///     "cnpj": "33.333.333/0001-33",
        ///     "contato": "contato@fornecedord.com",
        ///     "endereco": "Avenida D, 123"
        /// }
        /// </remarks>
        /// <param name="id">ID do fornecedor</param>
        /// <param name="input">Novos dados do fornecedor</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Fornecedor atualizado com sucesso</response>
        /// <response code="404">Fornecedor não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, Fornecedor input)
        {
            var fornecedor = _context.Fornecedores.SingleOrDefault(f => f.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            fornecedor.Nome = input.Nome;
            fornecedor.Cnpj = input.Cnpj;
            fornecedor.Contato = input.Contato;
            fornecedor.Endereco = input.Endereco;
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Exclui um fornecedor pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        /// Nenhum conteúdo retornado.
        /// </remarks>
        /// <param name="id">ID do fornecedor a ser excluído</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Fornecedor excluído com sucesso</response>
        /// <response code="404">Fornecedor não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var fornecedor = _context.Fornecedores.SingleOrDefault(f => f.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            _context.Fornecedores.Remove(fornecedor);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
