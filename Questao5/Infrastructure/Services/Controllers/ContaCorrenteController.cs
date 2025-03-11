using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly string _connectionString;
        public ContaCorrenteController(IConfiguration configuration) 
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        // GET: api/<ContaCorrente>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using (var sqlConnection = new SqliteConnection(_connectionString)) 
            {
                const string sql = "SELECT * FROM contacorrente";
                var contas = await sqlConnection.QueryAsync<ContaCorrente>(sql);
                return Ok(contas);
            }
        }

        // GET api/<ContaCorrente>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            using (var sqlConnection = new SqliteConnection(_connectionString))
            {
                var parameters = new { id };

                const string sql = "SELECT * FROM contacorrente WHERE idContaCorrente = @id";
                var contaCorrente = await sqlConnection.QuerySingleOrDefaultAsync<ContaCorrente>(sql, parameters);

                if (contaCorrente == null) 
                    return NotFound();
                else 
                    return Ok(contaCorrente);
                
            }
        }
    }
}
