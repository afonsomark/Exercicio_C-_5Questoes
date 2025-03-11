using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using NSubstitute;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentoController : ControllerBase
    {
        private readonly string _connectionString;
        public MovimentoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        // GET api/<MovimentoController>/5
        [HttpGet("{idContaCorrente}")]
        public async Task<IActionResult> Get(string idContaCorrente)
        {
            using (var sqlConnection = new SqliteConnection(_connectionString))
            {
                var parameters = new { idContaCorrente };

                const string sqlcredito = "SELECT sum(valor) AS valor FROM movimento WHERE idContaCorrente = @idContaCorrente AND tipomovimento = 'C'";
                const string sqldebito = "SELECT sum(valor) AS valor FROM movimento WHERE idContaCorrente = @idContaCorrente AND tipomovimento = 'D'";
                var credito = await sqlConnection.QuerySingleOrDefaultAsync<Movimento>(sqlcredito, parameters);
                var debito = await sqlConnection.QuerySingleOrDefaultAsync<Movimento>(sqldebito, parameters);
                decimal saldo = credito.Valor - debito.Valor;

                if (saldo < 0)
                    return BadRequest("Valor de saldo inválido");
                else
                    return Ok(saldo);
            }
        }

        // POST api/<MovimentoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string idmovimento, string idcontacorrente, TipoMovimento tipomov, decimal valor)
        {
            using (var sqlConnection = new SqliteConnection(_connectionString))
            {
                DateTime date = DateTime.Now;
                var datamovimento = date.ToString();
                var tipomovimento = tipomov.ToString();
                var parameters = new { idmovimento, idcontacorrente, datamovimento, tipomovimento, valor };

                const string sqlconta = "SELECT * FROM contacorrente WHERE idcontacorrente = @idcontacorrente AND ativo = 1";
                var validaconta = await sqlConnection.QuerySingleOrDefaultAsync<ContaCorrente>(sqlconta, parameters);

                if (validaconta == null)
                    return BadRequest("Não é permitida movimentação em conta não cadastrada ou inativa");
                else
                {
                    var chave_idempotencia = idmovimento;
                    var requisicao = tipomov.ToString();
                    var resultado = "Sucesso";
                    var parameters_idempotencia = new { chave_idempotencia, requisicao, resultado };

                    const string sqlmov = "INSERT INTO movimento VALUES (@idmovimento, @idcontacorrente, @datamovimento, @tipomovimento, @valor)";
                    var movimentacao = await sqlConnection.ExecuteScalarAsync<Movimento>(sqlmov, parameters);

                    const string sqlidemp = "INSERT INTO idempotencia VALUES (@chave_idempotencia, @requisicao, @resultado)";
                    var idempotencia = await sqlConnection.ExecuteScalarAsync<Idempotencia>(sqlidemp, parameters_idempotencia);
                    return Ok(idmovimento);
                }
            }
        }
    }
}
