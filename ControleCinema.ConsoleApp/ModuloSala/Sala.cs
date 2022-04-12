using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloSessao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSala
{
    public class Sala : EntidadeBase
    {
        public string nomeDaSala;
        public int capacidade;

        public Sala(int capacidade,string nomeDaSala)
        {
            this.capacidade = capacidade;     
            this.nomeDaSala = nomeDaSala;
        }
        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome da Sala: " + nomeDaSala + Environment.NewLine +
                "Capacidade: " + capacidade + Environment.NewLine;

        }
    }
}
