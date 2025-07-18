using System;
using System.Threading;

class Personagens
{
    private string nome;
    private int vida;
    private int vidaMaxima;
    private int ataque;
    private int defesa;
    private int ataqueEspecial;
    private int nivel;
    private int experiencia;
    private int experienciaParaProximoNivel;

    public string Nome => nome;
    public int Vida
    {
        get => vida;
        set => vida = Math.Min(Math.Max(0, value), vidaMaxima);
    }
    public int VidaMaxima => vidaMaxima;
    public int Ataque => ataque;
    public int Defesa => defesa;
    public int AtaqueEspecial => ataqueEspecial;
    public int Nivel => nivel;
    public int Experiencia => experiencia;
    public int ExperienciaParaProximoNivel => experienciaParaProximoNivel;

    public Personagens(string nome, int vidaMaxima, int ataque, int defesa, int ataqueEspecial, int nivel)
    {
        this.nome = nome;
        this.vidaMaxima = vidaMaxima;
        this.vida = vidaMaxima;
        this.ataque = ataque;
        this.defesa = defesa;
        this.ataqueEspecial = ataqueEspecial;
        this.nivel = nivel;
        this.experiencia = 0;
        this.experienciaParaProximoNivel = 100 + (nivel - 1) * 50;
    }

    public virtual void Atacar(Personagens alvo)
    {
        int poder = 50;
        Random rng = new Random();
        double variacao = rng.Next(85, 101) / 100.0;
        bool critico = rng.Next(0, 100) < 6;
        double defesaAlvo = critico ? alvo.Defesa / 2.0 : alvo.Defesa;
        double danoBase = (((2 * nivel / 5.0 + 2) * poder * ataque / defesaAlvo) / 50) + 2;
        int danoFinal = (int)(danoBase * variacao);

        if (critico)
        {
            danoFinal = (int)(danoFinal * 1.5);
            Console.WriteLine("\nAcerto crítico!");
        }

        alvo.Vida -= danoFinal;
        Console.WriteLine($"\n{nome} ataca {alvo.Nome} e causa {danoFinal} de dano.");
    }

    public void GanharExperiencia(int xp)
    {
        experiencia += xp;
        Console.WriteLine($"{nome} ganhou {xp} XP.");

        while (experiencia >= experienciaParaProximoNivel)
        {
            experiencia -= experienciaParaProximoNivel;
            SubirNivel();
        }
    }

    private void SubirNivel()
    {
        nivel++;
        vidaMaxima += 10;
        ataque += 2;
        defesa += 2;
        ataqueEspecial += 3;
        vida = vidaMaxima;
        experienciaParaProximoNivel = 100 + (nivel - 1) * 50;

        Console.WriteLine($"\n{nome} subiu para o nível {nivel}!");
        Console.WriteLine($"Vida máxima aumentou para {vidaMaxima}.");
        Console.WriteLine("Ataque, Defesa e Ataque Especial aumentaram.");
    }

    public virtual void UsarItem()
    {
        Vida += 10;
        Console.WriteLine($"\n{nome} usa uma poção e agora tem {Vida} de vida!");
    }

    public virtual void GameOver()
    {
        Console.WriteLine("\nVocê morreu, fim de jogo.");
    }
}

class Mago : Personagens
{
    public Mago(string nome, int vidaMaxima, int ataque, int defesa, int ataqueEspecial, int nivel)
        : base(nome, vidaMaxima, ataque, defesa, ataqueEspecial, nivel) { }

    public override void Atacar(Personagens alvo)
    {
        int poder = 65;
        Random rng = new Random();
        double variacao = rng.Next(85, 101) / 100.0;
        bool critico = rng.Next(0, 100) < 6;
        double defesaAlvo = critico ? alvo.Defesa / 2.0 : alvo.Defesa;
        double danoBase = (((2 * Nivel / 5.0 + 2) * poder * AtaqueEspecial / defesaAlvo) / 50) + 2;
        int danoFinal = (int)(danoBase * variacao);

        if (critico)
        {
            danoFinal = (int)(danoFinal * 1.5);
            Console.WriteLine("\nAcerto crítico!");
        }

        alvo.Vida -= danoFinal;
        Console.WriteLine($"\n{Nome} atira uma magia em {alvo.Nome} causando {danoFinal} de dano.");
    }
}

public class HelloWorld
{
    static void PrintarDramatico(string texto, int velocidade = 100)
    {
        foreach (char c in texto)
        {
            Console.Write(c);

            if (".!?,".Contains(c))
                Thread.Sleep(velocidade * 7);
            else{
                Thread.Sleep(velocidade);
            }
        }

        Console.WriteLine();
    }

    public static void Main(string[] args)
    {
        //Personagens jogador = CriarPersonagem();
        //Personagens guerreirinho = new Personagens("Guerreirinho", 50, 20, 15, 5, 10);

        //CombateEmTurno(jogador, guerreirinho);
        CapituloUm();
    }

    static Personagens CriarPersonagem()
    {
        Console.WriteLine("Crie seu personagem:");

        string nome = "";
        while (string.IsNullOrWhiteSpace(nome))
        {
            Console.Write("Digite o nome do seu personagem: ");
            nome = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(nome))
                Console.WriteLine("Nome não pode estar vazio!");
        }

        string escolhaClasse = "";
        while (escolhaClasse != "1" && escolhaClasse != "2")
        {
            Console.WriteLine("\nEscolha sua classe:");
            Console.WriteLine("1. Guerreiro");
            Console.WriteLine("2. Mago");
            Console.Write("Escolha (1 ou 2): ");
            escolhaClasse = Console.ReadLine().Trim();

            if (escolhaClasse != "1" && escolhaClasse != "2")
                Console.WriteLine("Classe inválida. Escolha 1 ou 2.");
        }

        int pontosDistribuir = 20;
        int ataque = 0, defesa = 0, ataqueEspecial = 0;

        Console.WriteLine($"\nVocê tem {pontosDistribuir} pontos para distribuir entre Ataque, Defesa e Ataque Especial.");

        bool distribuiuCorretamente = false;

        while (!distribuiuCorretamente)
        {
            ataque = defesa = ataqueEspecial = 0;
            int pontosRestantes = pontosDistribuir;

            ataque = LerPontos("Ataque", pontosRestantes);
            pontosRestantes -= ataque;

            defesa = LerPontos("Defesa", pontosRestantes);
            pontosRestantes -= defesa;

            ataqueEspecial = LerPontos("Ataque Especial", pontosRestantes);
            pontosRestantes -= ataqueEspecial;

            if (pontosRestantes != 0)
            {
                Console.WriteLine("\nVocê não distribuiu os pontos corretamente. Tentando de novo...\n");
            }
            else
            {
                distribuiuCorretamente = true;
            }
        }

        int nivelInicial = 1;
        int vidaTotal = 30 + (nivelInicial * 5);

        if (escolhaClasse == "2")
            return new Mago(nome, vidaTotal, ataque, defesa, ataqueEspecial, nivelInicial);
        else
            return new Personagens(nome, vidaTotal, ataque, defesa, ataqueEspecial, nivelInicial);
    }

    static int LerPontos(string atributo, int maximo)
    {
        int pontos = -1;
        while (pontos < 0 || pontos > maximo)
        {
            Console.Write($"{atributo} (0 até {maximo}): ");
            string entrada = Console.ReadLine().Trim();

            if (!int.TryParse(entrada, out pontos) || pontos < 0 || pontos > maximo)
            {
                Console.WriteLine("Valor inválido. Tente novamente.");
                pontos = -1;
            }
        }
        return pontos;
    }

    static void CombateEmTurno(Personagens jogador, Personagens inimigo)
    {
        Random rng = new Random();

        while (jogador.Vida > 0 && inimigo.Vida > 0)
        {
            Console.WriteLine("\nEscolha sua ação:");
            Console.WriteLine("1. Atacar");
            Console.WriteLine("2. Usar Item");
            Console.WriteLine("3. Defender");
            Console.Write("Escolha: ");
            string opcao = Console.ReadLine();
            int permissao;

            switch (opcao)
            {
                case "1":
                    jogador.Atacar(inimigo);
                    permissao = 1;
                    break;
                case "2":
                    jogador.UsarItem();
                    permissao = 1;
                    break;
                default:
                    Console.WriteLine("\nOpção inválida.");
                    permissao = 0;
                    break;
            }

            if (inimigo.Vida > 0 && permissao == 1)
            {
                inimigo.Atacar(jogador);
            }

            Console.WriteLine($"\n{jogador.Nome}: {jogador.Vida}HP");
            Console.WriteLine($"{inimigo.Nome}: {inimigo.Vida}HP");
        }

        if (jogador.Vida <= 0)
        {
            jogador.GameOver();
        }
        else
        {
            Console.WriteLine("Você venceu!");
            jogador.GanharExperiencia(50 + inimigo.Nivel * 10);
        }
    }

    static void CapituloUm()
    {
        PrintarDramatico("###########################");
        PrintarDramatico("Firjan SENAI - SG/ 2025");
        PrintarDramatico("###########################");
        PrintarDramatico("Luzes piscam. Computadores antigos ligam sozinhos no Lab TI 1.");
        PrintarDramatico("Um grupo de alunos está no pátio.");
    }
}
