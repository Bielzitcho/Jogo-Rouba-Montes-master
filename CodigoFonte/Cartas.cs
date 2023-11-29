class Cartas
{
    private string numero = "";
    private string naipe = "";
    public void AdicionarCartas(List<Cartas> cartasBaralho)
    {
        //Gerando carta coringa
        Cartas coringa = new Cartas();
        coringa.Contrutor("14", "CORINGA");
        cartasBaralho.Add(coringa);

        //Gerando cartas do às
        for (int i = 1; i < 14; i++)
        {
            Cartas carta = new Cartas();
            switch (i)
            {
                case 1:
                    carta.Contrutor("ÀS", "PAUS");
                    cartasBaralho.Add(carta);
                    break;

                case 11:
                    carta.Contrutor("VALETE", "PAUS");
                    cartasBaralho.Add(carta);
                    break;

                case 12:
                    carta.Contrutor("DAMA", "PAUS");
                    cartasBaralho.Add(carta);
                    break;

                case 13:
                    carta.Contrutor("REI", "PAUS");
                    cartasBaralho.Add(carta);
                    break;

                default:
                    carta.Contrutor(i.ToString(), "PAUS");
                    cartasBaralho.Add(carta);
                    break;
            }
        }
        //Gerando cartas ouro
        for (int i = 1; i < 14; i++)
        {
            Cartas carta = new Cartas();
            switch (i)
            {
                case 1:
                    carta.Contrutor("ÀS", "OURO");
                    cartasBaralho.Add(carta);
                    break;

                case 11:
                    carta.Contrutor("VALETE", "OURO");
                    cartasBaralho.Add(carta);
                    break;

                case 12:
                    carta.Contrutor("DAMA", "OURO");
                    cartasBaralho.Add(carta);
                    break;

                case 13:
                    carta.Contrutor("REI", "OURO");
                    cartasBaralho.Add(carta);
                    break;

                default:
                    carta.Contrutor(i.ToString(), "OURO");
                    cartasBaralho.Add(carta);
                    break;
            }
        }
        //Gerando cartas copas
        for (int i = 1; i < 14; i++)
        {
            Cartas carta = new Cartas();
            switch (i)
            {
                case 1:
                    carta.Contrutor("ÀS", "COPAS");
                    cartasBaralho.Add(carta);
                    break;

                case 11:
                    carta.Contrutor("VALETE", "COPAS");
                    cartasBaralho.Add(carta);
                    break;

                case 12:
                    carta.Contrutor("DAMA", "COPAS");
                    cartasBaralho.Add(carta);
                    break;

                case 13:
                    carta.Contrutor("REI", "COPAS");
                    cartasBaralho.Add(carta);
                    break;

                default:
                    carta.Contrutor(i.ToString(), "COPAS");
                    cartasBaralho.Add(carta);
                    break;
            }
        }
        //Gerando cartas Espadas
        for (int i = 1; i < 14; i++)
        {
            Cartas carta = new Cartas();
            switch (i)
            {
                case 1:
                    carta.Contrutor("ÀS", "ESPADAS");
                    cartasBaralho.Add(carta);
                    break;

                case 11:
                    carta.Contrutor("VALETE", "ESPADAS");
                    cartasBaralho.Add(carta);
                    break;

                case 12:
                    carta.Contrutor("DAMA", "ESPADAS");
                    cartasBaralho.Add(carta);
                    break;

                case 13:
                    carta.Contrutor("REI", "ESPADAS");
                    cartasBaralho.Add(carta);
                    break;

                default:
                    carta.Contrutor(i.ToString(), "ESPADAS");
                    cartasBaralho.Add(carta);
                    break;
            }
        }
    }
    public void Contrutor(string numero, string naipe)
    {
        this.numero = numero;
        this.naipe = naipe;
    }
    public string GetNumero()
    {
        return numero;
    }
    public string GetNaipe(){
        return naipe;
    }
}