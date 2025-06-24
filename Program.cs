using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GerenciadorDeTarefas
{
    class Program
    {
        // Lista pricnipal que armazena as tarefas
        static List<Tarefa> tarefas = new List<Tarefa>();

        // Caminho do arquivo JSON onde as tarefas serão salvas
        static string caminhoArquivo = "tarefas.json";

        static void Main(string[] args)
        {
            CarregarTarefas();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Gerenciador de Tarefas ===");
                Console.WriteLine("1. Adicionar Tarefa");
                Console.WriteLine("2. Listar Tarefas");
                Console.WriteLine("3. Marcar Tarefa como Concluída");
                Console.WriteLine("4. Remover Tarefa");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarTarefa();
                        break;
                    case "2":
                        ListarTarefas();
                        break;
                    case "3":
                        MarcarTarefaConcluida();
                        break;
                    case "4":
                        RemoverTarefa();
                        break;
                    case "0":
                        SalvarTarefas();
                        return;
                    default:
                        Console.WriteLine("opção inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine("Pressione ENTER para continuar...");
                Console.ReadLine();
            }
        }

        //Adiciona uma nova tarefa á lista
        static void AdicionarTarefa()
        {
            Console.Clear();
            Console.WriteLine("Digite a descrição da tarefa: ");
            string descricao = Console.ReadLine();

            tarefas.Add(new Tarefa(descricao));
            SalvarTarefas();

            Console.WriteLine("Tarefa adicionada com sucesso!");
        }

        //Lista todas as tarefas
        static void ListarTarefas()
        {
            Console.Clear();
            if (tarefas.Count == 0)
            {
                Console.WriteLine("Nenhuma tarefa encontrada.");
                return;
            }

            for (int i = 0; i < tarefas.Count; i++)
            {
                Console.WriteLine($"#{i + 1} - ");
                tarefas[i].Exibir();
            }
        }

        //Marca uma tarefa como concluída com base no índice
        static void MarcarTarefaConcluida()
        {
            ListarTarefas();

            Console.Write("Digite o número da tarefa para marcar como concluída: ");
            if (int.TryParse(Console.ReadLine(), out int indice) && indice >= 1 && indice <= tarefas.Count)
            {
                tarefas[indice - 1].Concluida = true;
                SalvarTarefas();
                Console.WriteLine("Tarefa marcada como concluída.");
            }
            else
            {
                Console.WriteLine("Número inválido.");
            }
        }

        //Remove uma tarefa com base no indice informado
        static void RemoverTarefa()
        {
            ListarTarefas();

            Console.Write("Digite o número da tarefa para remover: ");

            if (int.TryParse(Console.ReadLine(), out int indice) && indice >= 1 && indice <= tarefas.Count)
            {
                tarefas.RemoveAt(indice - 1);
                SalvarTarefas();
                Console.WriteLine("Tarefa removida com sucesso.");
            }
            else
            {
                Console.WriteLine("Número inválido.");
            }
        }

        //Carrega as tarefas do arquivo JSON
        static void CarregarTarefas()
        {
            if (File.Exists(caminhoArquivo))
            {
                string json = File.ReadAllText(caminhoArquivo);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    tarefas = JsonSerializer.Deserialize<List<Tarefa>>(json);
                }
                else
                {
                    tarefas = new List<Tarefa>();
                }
            }
        }


        //Salva as tarefas no arquivo JSON
        static void SalvarTarefas()
        {
            string json = JsonSerializer.Serialize(tarefas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(caminhoArquivo, json);
        }

    }
}