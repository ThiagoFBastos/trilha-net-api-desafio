using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio
{
    public class TarefaRepository: ITarefaRepository
    {
        private readonly OrganizadorContext _context;

        public TarefaRepository(OrganizadorContext context)
        {
            _context = context;
        }
        public async Task CriarTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
        }

        public async ValueTask<Tarefa> AtualizarTarea(int id, Tarefa tarefa)
        {
            var tarefaOriginal = await this.ObterTarefaPorId(id);
            tarefaOriginal.Data = tarefa.Data;
            tarefaOriginal.Descricao = tarefa.Descricao;
            tarefaOriginal.Status = tarefa.Status;
            tarefaOriginal.Titulo = tarefa.Titulo;
            _context.Tarefas.Update(tarefaOriginal);
            await _context.SaveChangesAsync();
            return tarefaOriginal;
        }
        public async Task DeletarTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
        }

        public async ValueTask<Tarefa> ObterTarefaPorId(int id)
        {
            return await _context.
                            Tarefas
                            .Where(t => t.Id == id)
                            .FirstOrDefaultAsync();
        }
         public async ValueTask<List<Tarefa>> ObterTafefaPorTitulo(string titulo)
        {
            return await _context.
                            Tarefas
                            .Where(t => t.Titulo == titulo)
                            .ToListAsync();
        }
        public async ValueTask<List<Tarefa>> ObterTafefaPorData(DateTime data)
        {
            return await _context.
                            Tarefas
                            .Where(t => t.Data == data)
                            .ToListAsync();
        }

        public async ValueTask<List<Tarefa>> ObterTarefaPorStatus(EnumStatusTarefa status)
        {
            return await _context.
                            Tarefas
                            .Where(t => t.Status == status)
                            .ToListAsync();
        }

        public async ValueTask<List<Tarefa>> ObterTodasTarefas()
        {
            return await _context.
                            Tarefas
                            .ToListAsync();
        }
    }
}