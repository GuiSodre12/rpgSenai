using System;
using System.Threading;

class Personagens{
    private string nome;
    private int vida;
    private int vidaMaxima;
    private int ataque;
    private int defesa;
    private int ataqueEspecial;
    private int nivel;
    private int experiencia;
    private int experienciaParaProximoNivel;

    public string Nome { get { return nome; } }
    public int Vida { get { return vida; } set { vida = Math.Min(Math.Max(0, value), vidaMaxima); } }
    public int VidaMaxima { get { return vidaMaxima; } }
    public int Ataque { get { return ataque; } }
    public int Defesa { get { return defesa; } }
    public int AtaqueEspecial { get { return ataqueEspecial; } }
    public int Nivel { get { return nivel; } }
    public int Experiencia { get { return experiencia; } }
    public int ExperienciaParaProximoNivel { get { return experienciaParaProximoNivel; } }

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

    public virtual void Atacar(Personagens alvo){
        int poder = 50;
        Random rng = new Random();
        double variacao = rng.Next(85, 101) / 100.0;
        bool critico = rng.Next(0, 100) < 6;
        double defesaAlvo = critico ? alvo.Defesa / 2.0 : alvo.Defesa;
        double danoBase = (((2 * nivel / 5.0 + 2) * poder * ataque / defesaAlvo) / 50) + 2;
        int danoFinal = (int)(danoBase * variacao);
        if (critico){
            danoFinal = (int)(danoFinal * 1.5);
            Console.WriteLine("\nAcerto crítico!");
        }
        alvo.Vida -= danoFinal;
        Console.WriteLine($"\n{nome} ataca {alvo.Nome} e causa {danoFinal} de dano.");
    }

    public void GanharExperiencia(int xp){
        experiencia += xp;
        Console.WriteLine($"{nome} ganhou {xp} XP.");
        while (experiencia >= experienciaParaProximoNivel){
            experiencia -= experienciaParaProximoNivel;
            SubirNivel();
        }
    }

    private void SubirNivel(){
        nivel++;
        vidaMaxima += 10;
        ataque += 2;
        defesa += 2;
        ataqueEspecial += 3;
        vida = vidaMaxima;
        experienciaParaProximoNivel = 100 + (nivel - 1) * 50;
        Console.WriteLine($"\n{nome} subiu para o nível {nivel}!");
        Console.WriteLine($"Vida máxima aumentou para {vidaMaxima}.");
        Console.WriteLine($"Ataque, Defesa e Ataque Especial aumentaram.");
    }

    public virtual void UsarItem(){
        Vida += 10;
        Console.WriteLine($"\n{nome} usa uma poção e agora tem {Vida} de vida!");
    }

    public virtual void GameOver(){
        Console.WriteLine("\nVocê morreu, fim de jogo.");
    }
}

class Mago : Personagens{
    public Mago(string nome, int vidaMaxima, int ataque, int defesa, int ataqueEspecial, int nivel) : base(nome, vidaMaxima, ataque, defesa, ataqueEspecial, nivel){}

    public override void Atacar(Personagens alvo){
        int poder = 65;
        Random rng = new Random();
        double variacao = rng.Next(85, 101) / 100.0;
        bool critico = rng.Next(0, 100) < 6;
        double defesaAlvo = critico ? alvo.Defesa / 2.0 : alvo.Defesa;
        double danoBase = (((2 * Nivel / 5.0 + 2) * poder * AtaqueEspecial / defesaAlvo) / 50) + 2;
        int danoFinal = (int)(danoBase * variacao);
        if (critico){
            danoFinal = (int)(danoFinal * 1.5);
            Console.WriteLine("\nAcerto crítico!");
        }
        alvo.Vida -= danoFinal;
        Console.WriteLine($"\n{Nome} atira uma magia em {alvo.Nome} causando {danoFinal} de dano.");
    }
}

public class HelloWorld
{
    
    static void PrintarDramatico(string texto, int velocidade = 135)
    {
        foreach (char c in texto){
            Console.Write(c);
            
            if (".!?,".Contains(c)){ //se for algum caractere especial ele da uma segurada a mais pra fazer drama
            Thread.Sleep(velocidade * 7);
            }
        }
        
    Console.WriteLine();
    }
    
    //Main
    public static void Main(string[] args)
    {
        Personagens guerreirinho = new Personagens("Guerreirinho", 50, 20, 15, 5, 10);
        Personagens mago = new Mago("John Arias", 40, 10, 10, 30, 10);
        
        PrintarDramatico("###########################");
        PrintarDramatico("Mamae... meu pipi ta doendo.");

        
    }
    
    //Combate
    static void combateEmTurno(Personagens jogador, Personagens inimigo)
    {
        Random rng = new Random();

        while (jogador.Vida > 0 && inimigo.Vida > 0)
        {
            Console.WriteLine("\nEscolha sua acao:");
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
                    Console.WriteLine("\nOpcao inválida.");
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
            // Ganha XP após vencer
            jogador.GanharExperiencia(50 + inimigo.Nivel * 10);
        }
    }
    
    static void capituloUm(){
        Console.WriteLine("###########################");
        Console.WriteLine("Maracana, a casa do fluminense - RJ, 2025");
        Console.WriteLine("###########################");
        Console.WriteLine("Voce, John Arias, esta no maracana treinando para estourar o botao do chelsea no mundial.");
        Console.WriteLine("John Olha de canto de olho, e ve um monstro terrivel enviado pelos demonios chamados de socios do fluminense e ele ataca!");
    }
}

