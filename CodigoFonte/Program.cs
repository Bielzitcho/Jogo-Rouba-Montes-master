using System.IO;
class Program
{
    static Cartas cartas = new Cartas();
    static List<Cartas> cartasBaralho = new List<Cartas>();
    static List<Jogador> ListaJogadores = new List<Jogador>();

    static StreamWriter escritorArquivo = new StreamWriter("Registros.txt", true);
    static void Main()
    {
        criarRegras();
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nBEM VINDO AO JOGO ROUBA MONTES ");
            Console.ResetColor();
            Console.WriteLine("\nVocê já conhece esse jogo?");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1) Sim");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("2) Não");
            Console.ResetColor();

            int opcao = int.Parse(Console.ReadLine());
            switch (opcao)
            {
                case 1:
                    Console.WriteLine("\nBeleza, vamos aos próximo passo.");
                    QuestionarioIniciarJogo();
                    break;
                case 2:
                    Console.WriteLine("\nNão se preocupe!! Temos um arquivo de texto com todas as regras do jogo disponibilizados na pasta, dê uma olhada!");
                    int preparado = 0;
                    Console.WriteLine("Quando estiverem prontos, digite 1: ");
                    while (preparado != 1)
                    {
                        preparado = int.Parse(Console.ReadLine());
                        if (preparado == 1)
                        {
                            Console.WriteLine("Bora pra próxima etapa!");
                            QuestionarioIniciarJogo();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" VOCÊ DIGITOU UM VALOR INVÁLIDO ");
                            Console.ResetColor();
                        }
                    }
                    break;
            }
            break;
        }
    }
    static void QuestionarioIniciarJogo()
    {
        int quantJogadores;
        Console.WriteLine("\nVocê deseja iniciar uma partida?");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n1)Sim");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("2)Não");
        Console.ResetColor();
        int iniciar = int.Parse(Console.ReadLine());
        switch (iniciar)
        {
            case 1:
                while (true)
                {
                    Console.WriteLine("\nQuantos jogadores vão jogar o jogo?");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Mínimo 2 - Máximo 8");
                    Console.ResetColor();
                    while (true)
                    {
                        quantJogadores = int.Parse(Console.ReadLine());
                        if (quantJogadores < 2 || quantJogadores > 8)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n****É NECESSÁRIO NO MÍNIMO 2 JOGADORES E NO MÁXIMO 8****");
                            Console.ResetColor();
                        }
                        else
                        {
                            Partida partida = new Partida();
                            break;
                        }
                    }
                    int quantBaralhos;
                    Console.WriteLine("\nQuantos baralhos serão usados no jogo?");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Mínimo 1 baralho (54 cartas)");
                    Console.ResetColor();
                    while (true)
                    {
                        quantBaralhos = int.Parse(Console.ReadLine());
                        if (quantBaralhos < 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n****É NECESSÁRIO NO MÍNIMO 1 BARALHO****");
                            Console.ResetColor();
                        }
                        else
                        {
                            CadastrarJogadores(quantJogadores);
                            IniciarPartida(quantBaralhos, quantJogadores);
                            break;
                        }
                    }
                    break;
                }
                break;
            case 2:

                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n****OPÇÃO INVÁLIDA****");
                Console.ResetColor();
                break;
        }
    }
    static void IniciarPartida(int quantBaralhos, int quantJogadores)
    {
        Queue<Jogador> rodada = new Queue<Jogador>(quantJogadores);
        Partida partida = new Partida();
        for (int i = 0; i < quantBaralhos; i++)
        {
            cartas.AdicionarCartas(cartasBaralho); //Adiciona cartas ao baralho
        }
        //Embaralha todas cartas
        List<Cartas> BaralhoEmbaralhado = partida.EmbaralharCartas(cartasBaralho, quantBaralhos);
        
        //Adicio as cartas no baralho
        partida.Baralho(BaralhoEmbaralhado);
        Stack<Cartas> BaralhoMesa = partida.Baralho(BaralhoEmbaralhado);

        //Distrubui as cartas entre os jogadores
        partida.DistribuirCartas(BaralhoMesa, ListaJogadores);
        for (int i = 0; i < quantJogadores; i++)
        {
            rodada.Enqueue(ListaJogadores[i]);
        }

        //Começa a rodada
        while (BaralhoMesa.Count >= 0)
        {
            Cartas cartaDaVez;
            partida.ComecarRodada(rodada, BaralhoMesa, out cartaDaVez, ListaJogadores);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n****A CARTA DA VEZ RETIRADA DO BARALHO É: {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()}");
            Console.ResetColor();
        }
    }
    static void CadastrarJogadores(int quantJogadores)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n****CADASTRO DE JOGADOR****\n");
        Console.ResetColor();
        for (int i = 0; i < quantJogadores; i++)
        {
            Console.WriteLine("Digite o nome do jogador");
            string nomeJogador = Console.ReadLine().ToUpper();
            while (true)
            {
                Console.WriteLine("\nInsira o PIN desejado:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("(O PIN deve ter 4 caracteres)");
                Console.ResetColor();
                string pin = Console.ReadLine();
                if (pin.Length == 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n****JOGADOR CADASTRADO****");
                    Console.ResetColor();
                    Jogador jogador = new Jogador(nomeJogador, pin);
                    ListaJogadores.Add(jogador);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n****O PIN DEVE TER 4 CARACTERES****");
                    Console.ResetColor();
                }
            }
        }
    }
    static void criarRegras()
    {
        string caminhoarquivo = "Regras.txt";
        string texto = "ROUBA MONTESn\nObjetivo do jogo:\n Acumular o maior monte de cartas.\n\n Regras: Um jogador distribui 4 cartas para cada participante e vira 4 cartas na mesa.\n O primeiro  jogador deve veri car se alguma carta que ele tem na mão é igual a alguma carta da mesa.\n Se for igual, ele junta as duas cartas em seu monte. Caso a carta seja igual a à carta do topo do monte adversário, ele poderá roubar esse monte, pegando todas as cartas.\n Caso não tenha uma carta igual a qualquer uma da mesa, deverá descartar uma carta da mão virada para cima no centro da mesa. Quando todos os jogadores estiverem sem cartas na mão, são distribuídas mais quatro para cada um, até que o baralho acabe.\n O jogo termina quando não houver mais cartas para serem distribuídas, e ganha quem tiver o maior monte. O coringa pode ser colocado em cima do monte do jogador para protegê-lo de ser roubado, e dura até que outra carta seja colocada por cima do monte.";
        StreamWriter regras = new StreamWriter(caminhoarquivo);
        regras.Write(texto);
        regras.Close();
    }
}