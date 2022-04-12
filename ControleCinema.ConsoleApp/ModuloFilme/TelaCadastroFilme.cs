using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloFuncionario;
using ControleCinema.ConsoleApp.ModuloGenero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class TelaCadastroFilme : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Filme> _repositorioFilme;
        private readonly Notificador _notificador;               
        private readonly TelaCadastroGenero _telaCadastroGenero;
        private readonly IRepositorio<Genero> _repositorioGenero;


        

        public TelaCadastroFilme(IRepositorio<Filme> repositorioFilme, Notificador notificador, IRepositorio<Genero> repositorioGenero, TelaCadastroGenero telaCadastroGenero) : base("Cadastro de Filme")
        {
            this._repositorioFilme = repositorioFilme;
            this._notificador = notificador;
            this._telaCadastroGenero = telaCadastroGenero;
            this._repositorioGenero = repositorioGenero;
        }        

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Filmes");

            Filme novoFilme = ObterFilme();

            _repositorioFilme.Inserir(novoFilme);

            _notificador.ApresentarMensagem("Filme cadastrado com sucesso!", TipoMensagem.Sucesso);
        }
        public void Editar()
        {
            MostrarTitulo("Editando Filme");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum filme cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroFilme = ObterNumeroRegistro();

            Filme filmeAtualizado = ObterFilme();

            bool conseguiuEditar = _repositorioFilme.Editar(numeroFilme, filmeAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Gênero de Filme editado com sucesso!", TipoMensagem.Sucesso);
        }
        public void Excluir()
        {
            MostrarTitulo("Excluindo Filme");

            bool temFilmesRegistrados = VisualizarRegistros("Pesquisando");

            if (temFilmesRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum filme cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroFilme = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioFilme.Excluir(numeroFilme);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Filme excluído com sucesso", TipoMensagem.Sucesso);
        }
        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Filmes");

            List<Filme> filmes = _repositorioFilme.SelecionarTodos();

            if (filmes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum filme disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Filme filme in filmes)
                Console.WriteLine(filme.ToString());
            
            return true;
        }
        private Filme ObterFilme()
        {
            Console.Write("Digite o Titulo do Filme: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a Duração do Filme: ");
            string duracao = Console.ReadLine();

            Console.WriteLine("Escolha o Genero do Filme");
            Genero genero = ObtemGereno();

            return new Filme(titulo,duracao,genero);
        }
        private Genero ObtemGereno()
        {
            bool temFilmesDisponiveis = _telaCadastroGenero.VisualizarRegistros("Pesquisando");

            if (!temFilmesDisponiveis)
            {
                _notificador.ApresentarMensagem("Não há nenhum filme disponível para cadastrar gênero.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do Genero que ira ser adicionado: ");
            int numeroGenero = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Genero generoSelecionada = _repositorioGenero.SelecionarRegistro(x => x.id == numeroGenero);

            return generoSelecionada;
        }
        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do filme que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioFilme.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do filme não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
