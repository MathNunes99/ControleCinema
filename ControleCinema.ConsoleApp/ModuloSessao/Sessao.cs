using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloFilme;
using ControleCinema.ConsoleApp.ModuloSala;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class Sessao : EntidadeBase
    {
        private readonly List<Ingresso> ingressos;
        private readonly Sala sala;
        private readonly Filme filme;
        private string titulo;
        private string duracao;
        private string horarioDaSessao;

        public Sessao(Filme filme, Sala sala, string horarioDaSessao)
        {                      
            this.filme = filme;
            this.sala = sala;
            ingressos = new List<Ingresso>(sala.capacidade);
            this.titulo = filme.titulo;
            this.duracao = filme.duracaoFilme;
            this.horarioDaSessao = horarioDaSessao;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Sala: " + sala.nomeDaSala + Environment.NewLine +
                "capacidade: " + sala.capacidade + Environment.NewLine +
                "Filme: " + titulo + Environment.NewLine +
                "Duracao: " + duracao + Environment.NewLine;                
        }
    }
}
