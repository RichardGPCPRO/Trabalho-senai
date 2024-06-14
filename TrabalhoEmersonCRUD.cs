using System;
using System.Collections.Generic;
using System.IO;
//erro no objeto
namespace CalculoMediaAlunos
{
    // Representar um aluno
    class Aluno
    {
        public string Nome { get; set; } // Nome do aluno
        public List<double> Notas { get; set; } // Armazenar as notas do aluno

        // PRimeiro inicia perguntando o nome do alunono 
        public Aluno(string nome)
        {
            Nome = nome;
            Notas = new List<double>(); // Inicializa a lista de notas
        }

        // Calcular a média 
        public double CalcularMedia()
        {
            double soma = 0;
            foreach (var nota in Notas)
            {
                soma += nota;
            }
            return Notas.Count > 0 ? soma / Notas.Count : 0; // Retorna a média das notas ou 0 se não houver notas
        }
    }

    class Program
    {
        static List<Aluno> alunos = new List<Aluno>(); // Lista para armazenar todos os alunos

        static void Main(string[] args)
        {
            CarregarDados(); // Carrega os dados dos alunos do arquivo (se disponível)

            // Loop principal do programa
            while (true)
            {
                // Exibe as opções do menu
                Console.WriteLine("\nSelecione uma opção:");
                Console.WriteLine("0. Cadastrar aluno");
                Console.WriteLine("1. Arquivar notas de um aluno");
                Console.WriteLine("2. Calcular médias dos alunos");
                Console.WriteLine("3. Mostrar alunos aprovados");
                Console.WriteLine("4. Mostrar alunos reprovados");
                Console.WriteLine("5. Sair");

                switch (Console.ReadLine())
                {
                    case "1":
                        CadastrarAluno();
                        break;
                    case "2":
                        LancarNotas();
                        break;
                    case "3":
                        CalcularMedias();
                        break;
                    case "4":
                        MostrarAprovados();
                        break;
                    case "5":
                        MostrarReprovados();
                        break;
                    case "6":
                        SalvarDados();
                        return;
                    default:
                        Console.WriteLine("Opção inválida! Escolha uma dessas opções." );
                        break;
                }
            }
        }

        static void CadastrarAluno()
        {
            Console.WriteLine("\nDigite o nome do aluno:");
            string nome = Console.ReadLine();
            alunos.Add(new Aluno(nome));
            Console.WriteLine($"Aluno '{nome}' cadastrado com sucesso!");
        }

        static void LancarNotas()
        {
            if (alunos.Count == 0)
            {
                Console.WriteLine("\nNenhum aluno cadastrado!");
                return;
            }

            Console.WriteLine("\nLista de alunos:");
            for (int i = 0; i < alunos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {alunos[i].Nome}");
            }

            Console.WriteLine("\nDigite o número do aluno para lançar as notas:");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > alunos.Count)
            {
                Console.WriteLine("Opção inválida!");
                return;
            }

            Console.WriteLine("Digite as notas do aluno (digite '0' para parar):");
            string inputNota;
            do
            {
                inputNota = Console.ReadLine();
                if (inputNota.ToLower() == "0" || !double.TryParse(inputNota, out double nota))
                {
                    continue;
                }
                alunos[index - 1].Notas.Add(nota);
            } while (inputNota.ToLower() != "0");

            Console.WriteLine("Notas lançadas com sucesso!");
        }

        static void CalcularMedias()
        {
            if (alunos.Count == 0)
            {
                Console.WriteLine("\nNenhum aluno cadastrado!");
                return;
            }

            Console.WriteLine("\nMédias dos alunos:");
            foreach (var aluno in alunos)
            {
                Console.WriteLine($"Nome: {aluno.Nome}, Média: {aluno.CalcularMedia()}");
            }
        }

        static void MostrarAprovados()
        {
            MostrarStatus("Aprovados", aluno => aluno.CalcularMedia() >= 7.0);
        }

        static void MostrarReprovados()
        {
            MostrarStatus("Reprovados", aluno => aluno.CalcularMedia() < 7.0);
        }

        static void MostrarStatus(string status, Func<Aluno, bool> condicao)
        {
            Console.WriteLine($"\nAlunos {status}:");
            foreach (var aluno in alunos)
            {
                if (condicao(aluno))
                {
                    Console.WriteLine($"Nome: {aluno.Nome}, Média: {aluno.CalcularMedia()}");
                }
            }
        }

        static void SalvarDados()
        {
            using (StreamWriter writer = new StreamWriter("alunos.txt"))
            {
                foreach (var aluno in alunos)
                {
                    writer.WriteLine($"{aluno.Nome}:{string.Join(",", aluno.Notas)}");
                }
            }
            Console.WriteLine("Dados salvos com sucesso!");
        }

        static void CarregarDados()
        {
            if (File.Exists("alunos.txt"))
            {
                alunos.Clear();
                foreach (var line in File.ReadAllLines("alunos.txt"))
                {
                    string[] parts = line.Split(':');
                    string nome = parts[0];
                    string[] notasStr = parts[1].Split(',');
                    List<double> notas = new List<double>();
                    foreach (var notaStr in notasStr)
                    {
                        if (double.TryParse(notaStr, out double nota))
                        {
                            notas.Add(nota);
                        }
                    }
                    alunos.Add(new Aluno(nome) { Notas = notas });
                }
                Console.WriteLine("Dados carregados com sucesso!");
            }
            else
            {
                Console.WriteLine("Arquivo de dados não encontrado.0");
            }
        }
    }
}
