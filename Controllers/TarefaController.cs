using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepository _repository;

        public TarefaController(ITarefaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId([FromRoute] int id)
        {
            var tarefa = await _repository.ObterTarefaPorId(id);

            if(tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            var tarefas = await _repository.ObterTodasTarefas();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public async Task<IActionResult> ObterPorTitulo([FromQuery] string titulo)
        {
            var tarefas = await _repository.ObterTafefaPorTitulo(titulo);
            return Ok(tarefas);
        }

        [HttpGet("ObterPorData")]
        public async Task<IActionResult> ObterPorData([FromQuery] DateTime data)
        {
            var tarefas = await _repository.ObterTafefaPorData(data);
            return Ok(tarefas);
        }

        [HttpGet("ObterPorStatus")]
        public async Task<IActionResult> ObterPorStatus([FromQuery] EnumStatusTarefa status)
        {
            var tarefas = await _repository.ObterTarefaPorStatus(status);
            return Ok(tarefas);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            await _repository.CriarTarefa(tarefa);

            return Ok(tarefa.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] Tarefa tarefa)
        {
            if(await _repository.ObterTarefaPorId(id) == null)
                return NotFound();

            var tarefaAtualizada = await _repository.AtualizarTarea(id, tarefa);

            return Ok(tarefaAtualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar([FromRoute] int id)
        {
            if(await _repository.ObterTarefaPorId(id) == null)
                return NotFound();

            await _repository.DeletarTarefa(id);

            return Ok();
        }
    }
}
