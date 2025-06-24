using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas
{
    public class Tarefa
    {
        //Descricao da tarefa informada pelo usuário
        public string Descricao { get; set; }

        //Indica se a tarefa foi concluida
        public bool Concluida { get; set; }

        //Data de criação da tarefa
        public DateTime CriadaEm { get; set; }

        //Construtor da classe. Define os valores iniciais
        public Tarefa(string descricao)
        {
            Descricao = descricao;
            Concluida = false;
            CriadaEm = DateTime.Now;
        }

        //Exibe as informações da tarefa no console
        public void Exibir()
        {
            string status = Concluida ? "Concluída" : "Pendente";
            Console.WriteLine($"[{status}] {Descricao} (Criada em: {CriadaEm:dd/MM/yyyy HH:mm})");
        }
    }
}