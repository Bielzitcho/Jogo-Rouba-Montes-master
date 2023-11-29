using System.Net.NetworkInformation;

class Partida
{
    private Stack<Cartas> cartasMesa = new Stack<Cartas>();
    public List<Cartas> ListaDeCartasAreaDescarte = new List<Cartas>();

    public List<Cartas> EmbaralharCartas(List<Cartas> cartasBaralho, int quantBaralhos)
    {
        Random numerosAleatorios = new Random();
        Cartas[] cartasEmbaralhadas = cartasBaralho.ToArray();
        List<int> PosicoesQueJaforam = new List<int>();
        int posicao, i = 0;

        while (i < 53 * quantBaralhos)
        {
            posicao = numerosAleatorios.Next(0, 53 * quantBaralhos);
            if (!PosicoesQueJaforam.Contains(posicao))
            {
                PosicoesQueJaforam.Add(posicao);
                cartasEmbaralhadas[i] = cartasBaralho[posicao];
                i++;
            }
        }
        cartasBaralho = cartasEmbaralhadas.ToList();
        return cartasBaralho;
    }
    public Stack<Cartas> Baralho(List<Cartas> baralhoMesa)
    {
        for (int i = 0; i < baralhoMesa.Count; i++)
        {
            cartasMesa.Push(baralhoMesa[i]);
        }
        return cartasMesa;
    }
    public void DistribuirCartas(Stack<Cartas> BaralhoMesa, List<Jogador> ListaDeJogadores)
    {
        int cont = 0;
        while (cont < ListaDeJogadores.Count)
        {
            while (ListaDeJogadores[cont].numeroCartasNaMao < 4)
            {
                ListaDeJogadores[cont].cartasNaMao.Add(BaralhoMesa.Pop());
                ListaDeJogadores[cont].numeroCartasNaMao++;
            }
            if (ListaDeJogadores[cont].numeroCartasNaMao == 4)
            {
                cont++;
            }
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n****4 CARTAS DISTRIBUIDAS ENTRE TODOS JOGADORES****\n");
        Console.ResetColor();
    }

    public void ComecarRodada(Queue<Jogador> rodada, Stack<Cartas> BaralhoMesa, out Cartas cartaDaVez, List<Jogador> listaJogadores)
    {
        Stack<Cartas> cartas = new Stack<Cartas>();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n****INICIANDO RODADA****\n");
        Console.ResetColor();
        Jogador jogador = rodada.Dequeue();
        rodada.Enqueue(jogador);

        Console.WriteLine($"O Jogador da vez é o {jogador.getNome()}, digite seu PIN");
        while (true)
        {
            string pin = Console.ReadLine();
            if (pin == jogador.getPin())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n****PIN VALIDADO****\n");
                Console.ResetColor();
                Console.WriteLine("\nAs cartas do seu baralho são:\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                for (int i = 0; i < jogador.numeroCartasNaMao; i++)
                {
                    Console.WriteLine(jogador.cartasNaMao[i].GetNumero() + " " + jogador.cartasNaMao[i].GetNaipe());
                }
                Console.ResetColor();
                cartaDaVez = BaralhoMesa.Pop();
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n****PIN INCORRETO****\n");
                Console.ResetColor();
            }
        }
        Console.WriteLine($"\nA carta retirada do baralho é: {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()}\n");
        Console.WriteLine("***** CARTAS DA MESA ****\n");
        if (ListaDeCartasAreaDescarte.Count > 0)
        {
            for (int i = 0; i < ListaDeCartasAreaDescarte.Count; i++)
            {
                Console.WriteLine($"{ListaDeCartasAreaDescarte[i].GetNumero()} {ListaDeCartasAreaDescarte[i].GetNaipe()}\n");
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" A área de descarte não tem cartas ainda.\n");
            Console.ResetColor();
        }
        Console.WriteLine("***** TOPO DOS MONTES DOS JOGADORES ****\n");

        for (int i = 0; i < listaJogadores.Count; i++)
        {
            if (listaJogadores[i].monteJogador.Count == 0)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"O jogador {listaJogadores[i].getNome()} não tem um monte.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"\nO jogador {listaJogadores[i].getNome()} tem seu monte com o topo: {listaJogadores[i].monteJogador.Peek().GetNumero()} {listaJogadores[i].monteJogador.Peek().GetNaipe()}");
            }
        }
        int certo = 0;
        while (certo == 0)
        {
            Console.WriteLine("\n****MENU DE OPÇÕES****\n");
            Console.WriteLine("1) Tentar pegar uma carta na área de descarte\n2) Tentar roubar um monte\n3) Formar monte com uma carta que está na sua mão\n4) Colocar carta na área de descarte");
            int opcaoDesejada = int.Parse(Console.ReadLine());
            switch (opcaoDesejada)
            {
                case 1:

                    bool ExisteNaArea = false;
                    while (true)
                    {
                        for (int i = 0; i < jogador.numeroCartasNaMao; i++)
                        {
                            for (int j = 0; j < ListaDeCartasAreaDescarte.Count; j++)
                            {
                                if (ListaDeCartasAreaDescarte[j].GetNumero() == cartaDaVez.GetNumero() && ListaDeCartasAreaDescarte.Count > 1)
                                {
                                    jogador.monteJogador.Push(ListaDeCartasAreaDescarte[j]);
                                    ListaDeCartasAreaDescarte.RemoveAt(j);
                                    jogador.monteJogador.Push(cartaDaVez);
                                    jogador.numeroCartasNaMao--;
                                    Console.WriteLine($"As cartas {ListaDeCartasAreaDescarte[j].GetNumero()} {ListaDeCartasAreaDescarte[j].GetNaipe()} e a {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()} foram adicionadas ao seu monte.");
                                    ExisteNaArea = true;
                                    certo = 1;
                                    break;
                                }
                                else if (jogador.getCartasNaMao()[i].GetNumero() == ListaDeCartasAreaDescarte[j].GetNumero())
                                {
                                    jogador.monteJogador.Push(jogador.getCartasNaMao()[i]);
                                    jogador.monteJogador.Push(ListaDeCartasAreaDescarte[j]);
                                    jogador.numeroCartasNaMao--;
                                    ExisteNaArea = true;
                                    certo = 1;
                                    Console.WriteLine($"As cartas {jogador.getCartasNaMao()[i].GetNumero()} {jogador.getCartasNaMao()[i].GetNaipe()} e {ListaDeCartasAreaDescarte[j].GetNumero()} {ListaDeCartasAreaDescarte[j].GetNaipe()} foram adicionadas ao seu monte.");
                                }

                            }
                        }
                        if (!ExisteNaArea)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"****NÃO EXISTE CARTA COM O NÚMERO {cartaDaVez.GetNumero()} NA ÀREA DE DESCARTE****");
                            Console.ResetColor();
                        }
                        break;
                    }
                    break;
                case 2:
                    bool ExisteTopoJogador = false;
                    for (int i = 0; i < listaJogadores.Count; i++)
                    {
                        if (listaJogadores[i].monteJogador.Count > 0)
                        {
                            if (cartaDaVez.GetNumero() == listaJogadores[i].monteJogador.Peek().GetNumero())
                            {
                                foreach (Cartas a in listaJogadores[i].monteJogador)
                                {
                                    jogador.monteJogador.Push(listaJogadores[i].monteJogador.Pop());
                                }
                                jogador.monteJogador.Push(cartaDaVez);
                                Console.WriteLine($"O monte do jogador {listaJogadores[i].getNome()} foi roubado!");
                                ExisteTopoJogador = true;
                                certo = 1;
                                break;
                            }

                        }
                    }
                    if (!ExisteTopoJogador)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n****ATRAVÉS DA CARTA {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()}, NÃO É POSSÍVEL ROUBAR MONTES****\n");
                        Console.ResetColor();
                    }
                    break;
                case 3:
                    if (jogador.numeroCartasNaMao > 0)
                    {
                        for (int i = 0; i < jogador.numeroCartasNaMao; i++)
                        {
                            if (cartaDaVez.GetNumero() == jogador.getCartasNaMao()[i].GetNumero())
                            {
                                Console.WriteLine($"As cartas {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()} e {jogador.getCartasNaMao()[i].GetNumero()} {jogador.getCartasNaMao()[i].GetNaipe()} foi adicionada ao seu monte.");
                                jogador.monteJogador.Push(jogador.getCartasNaMao()[i]);
                                jogador.monteJogador.Push(cartaDaVez);
                                jogador.getCartasNaMao().RemoveAt(i);
                                certo = 1;
                                break;
                            }
                        }
                        if (certo == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"O Jogador {jogador.getNome()} não possui cartas na mão que possam ser colocadas no monte junto com a {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()}");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"O jogador {jogador.getNome()} não possui cartas na mão.");
                    }
                    break;
                case 4:
                    ListaDeCartasAreaDescarte.Add(cartaDaVez);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"*** CARTA {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()} FOI ADICIONADA NA ÁREA DE DESCARTE ***");
                    Console.ResetColor();
                    certo = 1;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("**** OPÇÃO INVÁLIDA ****");
                    Console.ResetColor();
                    break;
            }

        }

    }

}



