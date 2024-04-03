using System.ComponentModel;

namespace Jogo_de_velha
{
    class Program
    {
        public static bool Venceu = false;
        public static Player player1;
        public static Player player2;
        static void Main()
        {
            Console.WriteLine("Nome do jogador 1: ");
            string jogador1 = Console.ReadLine();
            Console.WriteLine("Nome do jogador 2: ");
            string jogador2 = Console.ReadLine();
            Console.Clear();

            if (jogador1 == "")
            {
                jogador1 = "Jogador 1";
            }
            if (jogador2 == "")
            {
                jogador2 = "Jogador 2";
            }

            player1 = new Player(jogador1, "X");
            player2 = new Player(jogador2, "O");
            
            Console.WriteLine($"{player1.Nome} é X");
            Console.WriteLine($"{player2.Nome} é Bolinha");
            Console.WriteLine("Regras do jogo:");
            Console.WriteLine("-Para preencher um espaço, é necessário indicar qual espaço você quer preencher através de números.");
            Console.WriteLine("-Vence o jogador que conseguir formar uma linha com os três simbolos iguais primeiro.");
            Console.ReadKey();
            
            Game(player1, player2);
        }

        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("Pontuação:");
            Console.WriteLine($"{player1.Nome}: {player1.Vitorias} Pontos.");
            Console.WriteLine($"{player2.Nome}: {player2.Vitorias} Pontos.");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("O que você deseja fazer?\n[1]Jogar novamente\n[2]Sair");
            string tecla2 = Console.ReadLine();
            if (tecla2 == "1")
            {
                Game(player1, player2);
            }
            else if (tecla2 == "2")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Tecla não reconhecida. Tente novamente");
                Console.ReadKey();
                Menu();
            }
        }

        private static void DiferenteOuIgual()
        {
            foreach (var item in Grafico.combinacoes)
            {
                int quantidadeX = 0;
                int quantidadeO = 0;
                List<int> linha = new List<int>();
                foreach (var numero in item)
                {
                    if (Grafico.numeros[numero] == "X")
                    {
                        quantidadeX += 1;
                    }
                    else if (Grafico.numeros[numero] == "O")
                    {
                        quantidadeO += 1;
                    }
                    linha.Add(numero);
                }
                if (quantidadeX == 3 || quantidadeO == 3)
                {
                    if (player1.Jogando == true)
                    {
                        player1.Vitoria();
                    }
                    else if (player2.Jogando == true)
                    {
                        player2.Vitoria();
                    }
                    return;
                }
            }
        }

        private static void Game(Player player1, Player player2)
        {
            while (!Venceu)
            {
                Console.Clear();
                DiferenteOuIgual();
                Grafico.ExibirGrafico();
                if (Venceu) { break; }
                player2.Jogando = false;
                player1.Jogar();
                Console.Clear();
                DiferenteOuIgual();
                Grafico.ExibirGrafico();
                if (Venceu) { break; }
                player1.Jogando = false;
                player2.Jogar();
            }
            Console.ReadKey();
            Grafico.Redefinir(player1, player2);
            Venceu = false;
            Program program = new Program();
            program.Menu();
        }
    }

    class Player
    {
        public string Nome;
        string Tipo;
        public bool Jogando;
        public int Vitorias = 0;

        public Player(string nome, string tipo)
        {
            Nome = nome;
            Tipo = tipo;
        }

        public void Jogar()
        {
            Jogando = true;
            Console.WriteLine($"{Nome}:");
            try
            {
                int tecla1 = Convert.ToInt16(Console.ReadLine());

                if (Grafico.numeros.ContainsKey(tecla1))
                {
                    if (Grafico.numeros[tecla1] != "X" && Grafico.numeros[tecla1] != "O")
                    {
                        Grafico.numeros[tecla1] = Tipo;
                    }
                    else
                    {
                        Console.WriteLine("O espaço já está sendo ocupado. Você perdeu seu turno.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Este número não existe. Você perdeu seu turno.");
                    Console.ReadKey();
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine("O número é grande demais. Você perdeu seu turno.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Caractere inválido. Você perdeu seu turno.");
                Console.ReadKey();
            }
        }

        public void Vitoria()
        {
            Program.Venceu = true;
            Vitorias++;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Nome} venceu a rodada!");
            Console.ForegroundColor= ConsoleColor.Gray;
        }
    }

    static class Grafico
    {
        public static void Redefinir(Player player1, Player player2)
        {
            numeros[1] = "1"; numeros[2] = "2"; numeros[3] = "3"; numeros[4] = "4"; numeros[5] = "5";
            numeros[6] = "6"; numeros[7] = "7"; numeros[8] = "8"; numeros[9] = "9";
        }
        public static Dictionary<int, string> numeros = new Dictionary<int , string>()
        {
            {1, "1"},
            {2, "2"},
            {3, "3"},
            {4, "4"},
            {5, "5"},
            {6, "6"},
            {7, "7"},
            {8, "8"},
            {9, "9"}
        };
        public static List<List<int>> combinacoes = new List<List<int>>()
        {
            new List<int> {1, 2, 3},
            new List<int> {4, 5, 6},
            new List<int> {7, 8, 9},
            new List<int> {1, 4, 7},
            new List<int> {2, 5, 8},
            new List<int> {3, 6, 9},
            new List<int> {1, 5, 9},
            new List<int> {3, 5, 7}
        };
        static public void ExibirGrafico()
        {
            Console.WriteLine("              |              |              ");
            Console.WriteLine($"      {numeros[1]}       |      {numeros[2]}       |       {numeros[3]}      ");
            Console.WriteLine("              |              |              ");
            Console.WriteLine("--------------|--------------|--------------");
            Console.WriteLine("              |              |              ");
            Console.WriteLine($"      {numeros[4]}       |      {numeros[5]}       |       {numeros[6]}      ");
            Console.WriteLine("              |              |              ");
            Console.WriteLine("--------------|--------------|--------------");
            Console.WriteLine("              |              |              ");
            Console.WriteLine($"      {numeros[7]}       |      {numeros[8]}       |       {numeros[9]}      ");
            Console.WriteLine("              |              |              ");
        }
    }
}