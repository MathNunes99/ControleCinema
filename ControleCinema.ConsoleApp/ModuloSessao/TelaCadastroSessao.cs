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
    public class TelaCadastroSessao : TelaBase, ITelaCadastravel
    {
        private IRepositorio<Sessao> _repositorioSessao;
        private Notificador _notificador;
        private TelaCadastroSala _telaCadastroSala;
        private TelaCadastroFilme _telaCadastroFilme;
        private IRepositorio<Filme> _repositorioFilme;
        private IRepositorio<Sala> _repositorioSala;       

        public TelaCadastroSessao(
            IRepositorio<Sessao> repositorioSessao, 
            Notificador notificador,
            TelaCadastroFilme telaCadastroFilme,
            TelaCadastroSala telaCadastroSala, 
            IRepositorio<Sala> repositorioSala, 
            IRepositorio<Filme> repositorioFilme) : base("Cadastro de Sessão")
        {
            this._repositorioSessao = repositorioSessao;
            this._notificador = notificador;
            this._telaCadastroFilme = telaCadastroFilme;
            this._telaCadastroSala = telaCadastroSala;
            this._repositorioSala = repositorioSala;
            this._repositorioFilme = repositorioFilme;
            
        }        

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Sessao");

            Sessao novaSessao = ObterSessao();

            _repositorioSessao.Inserir(novaSessao);

            _notificador.ApresentarMensagem("Sessao cadastrada com sucesso!", TipoMensagem.Sucesso);
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

            int numeroSessao = ObterNumeroRegistro();

            Sessao SessaoAtualizado = ObterSessao();

            bool conseguiuEditar = _repositorioSessao.Editar(numeroSessao, SessaoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Gênero de Filme editado com sucesso!", TipoMensagem.Sucesso);
        }
        public void Excluir()
        {
            MostrarTitulo("Excluindo Sessao");

            bool temSessaoRegistrados = VisualizarRegistros("Pesquisando");

            if (temSessaoRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma Sessao cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroSessao = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioSessao.Excluir(numeroSessao);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sessao excluído com sucesso", TipoMensagem.Sucesso);
        }
        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Sessao");

            List<Sessao> sessoes = _repositorioSessao.SelecionarTodos();

            if (sessoes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma Sessao disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sessao sessao in sessoes)
                Console.WriteLine(sessao.ToString());

            Console.ReadLine();
            return true;
        }
        private Sessao ObterSessao()
        {                      
            Console.WriteLine("Escolha a Sala do Filme: ");
            Sala sala = ObtemSala();

            Console.WriteLine("Escolha o Filme");
            Filme filme = ObtemFilme();

            Console.WriteLine("Digite o Horario da Sessão");
            string horarioDaSessao = Console.ReadLine();

            return new Sessao(filme, sala, horarioDaSessao);
        }
        private Filme ObtemFilme()
        {
            bool temFilmesDisponiveis = _telaCadastroFilme.VisualizarRegistros("Pesquisando");

            if (!temFilmesDisponiveis)
            {
                _notificador.ApresentarMensagem("Não há nenhum filme disponível para escolher.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do Filme que ira ser adicionado: ");
            int numeroFilme = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Filme filmeSelecionada = _repositorioFilme.SelecionarRegistro(x => x.id == numeroFilme);

            return filmeSelecionada;
        }
        private Sala ObtemSala()
        {
            bool temSalasDisponiveis = _telaCadastroSala.VisualizarRegistros("Pesquisando");

            if (!temSalasDisponiveis)
            {
                _notificador.ApresentarMensagem("Não há nenhum Sala disponível.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da Sala a ser adicionada: ");
            int numeroSala = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Sala salaSelecionada = _repositorioSala.SelecionarRegistro(x => x.id == numeroSala);

            return salaSelecionada;
        }
        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da Sessao que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioSessao.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da Sessão não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 'vender' para Vender Ingresso");
            Console.WriteLine("Digite 'ver' para visualizar agrupada por filme");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }
    }
}
