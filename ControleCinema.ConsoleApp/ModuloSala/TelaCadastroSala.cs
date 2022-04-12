using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloFilme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSala
{
    public class TelaCadastroSala : TelaBase, ITelaCadastravel
    {
        private IRepositorio<Sala> _repositorioSala;
        private Notificador _notificador;

        public TelaCadastroSala(IRepositorio<Sala> repositorioSala, Notificador notificador) : base ("Cadastro de Salas")
        {
            this._repositorioSala = repositorioSala;
            this._notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Sala");

            Sala novaSala = ObterSala();

            _repositorioSala.Inserir(novaSala);

            _notificador.ApresentarMensagem("Sala cadastrada com sucesso!", TipoMensagem.Sucesso);
        }
        public void Editar()
        {
            MostrarTitulo("Editando Sala");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma Sala cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroSala = ObterNumeroRegistro();

            Sala SalaAtualizado = ObterSala();

            bool conseguiuEditar = _repositorioSala.Editar(numeroSala, SalaAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Gênero de Filme editado com sucesso!", TipoMensagem.Sucesso);
        }
        public void Excluir()
        {
            MostrarTitulo("Excluindo Sala");

            bool temSessaoRegistrados = VisualizarRegistros("Pesquisando");

            if (temSessaoRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma Sala cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroSessao = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioSala.Excluir(numeroSessao);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sala excluída com sucesso", TipoMensagem.Sucesso);
        }
        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Salas");

            List<Sala> salas = _repositorioSala.SelecionarTodos();

            if (salas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum filme disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sala sala in salas)
                Console.WriteLine(sala.ToString());
            
            return true;
        }

        private Sala ObterSala()
        {
            int capacidade = 0;
            while (capacidade < 1)
            {
                Console.Write("Escolha a Capacidade da sala: ");
                int.TryParse(Console.ReadLine(),out capacidade);
            }
            Console.Write("Nome da Sala: ");
            string nomeDaSala = Console.ReadLine();

            return new Sala(capacidade, nomeDaSala);
            
        }
        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da Sessao que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioSala.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da Sala não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

    }
}

