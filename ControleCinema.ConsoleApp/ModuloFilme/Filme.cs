using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloGenero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class Filme : EntidadeBase
    {        
        private readonly Genero genero;
        public string titulo;
        public string duracaoFilme;

        public Filme(string titulo, string duracao,Genero generoSelecionado)
        {
            this.titulo = titulo;
            this.duracaoFilme = duracao;

            genero = generoSelecionado;
        }
        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + titulo + Environment.NewLine+
                "Duraçao: " + duracaoFilme + Environment.NewLine+
                "Genero: " + genero.Descricao + Environment.NewLine;
        }
    }
}
