using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio
{
    public interface ITarefaRepository
    {
        Task CriarTarefa(Tarefa tarefa);
        ValueTask<Tarefa> AtualizarTarea(int id, Tarefa tarefa);
        Task DeletarTarefa(int id);
        ValueTask<Tarefa> ObterTarefaPorId(int id);
        ValueTask<List<Tarefa>> ObterTafefaPorTitulo(string titulo);
        ValueTask<List<Tarefa>> ObterTafefaPorData(DateTime data);
        ValueTask<List<Tarefa>> ObterTarefaPorStatus(EnumStatusTarefa status);
        ValueTask<List<Tarefa>> ObterTodasTarefas();
    }
}