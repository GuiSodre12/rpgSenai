using System;
class Personagens{
    private string nome;
    private int vida;
    private int ataque;
    private int defesa;
    private int ataqueEspecial;

    public string Nome { get { return nome; } }
    public int Vida { 
        get { return vida; } 
        set { vida = Math.Max(0, value); } 
    }
    public int Ataque { get { return ataque; } }
    public int Defesa { get { return defesa; } }
    public int AtaqueEspecial { get { return ataqueEspecial; } }

    public Personagens(string nome, int vida, int ataque, int defesa, int ataqueEspecial)
    {
        this.nome = nome;
        this.vida = vida;
        this.ataque = ataque;
        this.defesa = defesa;
        this.ataqueEspecial = ataqueEspecial;
    }

    public virtual void Atacar(Personagens alvo){
        if(ataque >= alvo.Defesa){
            int dano = ataque - alvo.Defesa;
            alvo.Vida = alvo.Vida - dano;
            Console.WriteLine($"\n{nome} ataca {alvo.Nome} e causa {dano} de dano.");
        }
        else{
            Console.WriteLine($"\n{nome} tentou atacar, mas não causou dano.");
        }
    }

    public virtual void UsarItem(){
        Vida += 10;
        Console.WriteLine($"\n{nome} usa uma pocao e agora tem {Vida} de vida!");
    }

    public virtual void GameOver(){
        Console.WriteLine("\n Você morreu, fim de jogo.");
    }
}

class Mago : Personagens{
    public Mago(string nome, int vida, int ataque, int defesa, int ataqueEspecial) : base(nome, vida, ataque, defesa, ataqueEspecial){}

    public override void Atacar(Personagens alvo){
        if(AtaqueEspecial >= alvo.Defesa){
            int dano = AtaqueEspecial - alvo.Defesa;
            alvo.Vida = alvo.Vida - dano;
            Console.WriteLine($"\n {Nome} atira uma magia em {alvo.Nome} causando {dano} de dano");
        }
        else{
            Console.WriteLine($"\n {Nome} lançou uma magia, mas ela foi bloqueada.");
        }
    }
}

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Personagens guerreirinho = new Personagens("Guerreirinho", 20, 15, 10, 2);
        Personagens mago = new Mago("John Arias", 10, 8, 8, 17);

        capituloUm();
        combateEmTurno(mago, guerreirinho);
    }

    static void combateEmTurno(Personagens jogador, Personagens inimigo)
    {
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
                    Console.WriteLine("\n opcao inválida.");
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
        }
    }

    static void capituloUm(){
        Console.WriteLine("###########################");
        Console.WriteLine("Maracana, a casa do fluminense - RJ, 2025");
        Console.WriteLine("###########################");
        Console.WriteLine("Voce, John Arias, esta no maracana treinando para estourar o botao do al hilal no mundial.");
        Console.WriteLine("John Olha de canto de olho, e ve um monstro terrivel enviado pelos demonios chamados de socios do fluminense e ele ataca!");
    }
}

