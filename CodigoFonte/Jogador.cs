class Jogador
{
    private string nome;
    public int numeroCartasNaMao;
    private string PIN;
    public List<Cartas> cartasNaMao = new List<Cartas>();
    public Stack<Cartas> monteJogador = new Stack<Cartas>();
    private Queue<string> ranking;

    public Jogador(string nome, string PIN)
    {
        this.nome = nome;
        this.numeroCartasNaMao = 0;
        this.PIN = PIN;
        this.ranking = new Queue<string>(5);
    }
    public string getNome()
    {
        return nome;
    }
  
    public string getPin()
    {
        return PIN;
    }
    public List<Cartas> getCartasNaMao()
    {
        return cartasNaMao;
    }
}