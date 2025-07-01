//`;-.          ___,
//  `.`\_...._/`.-"`
//    \        /      ,
//    /()   () \    .' `-._
//   |)  .    ()\  /   _.'
//   \  -'-     ,; '. <         oramsesterceiro
//    ;.__     ,;|   > \    
//   / ,    / ,  |.-'.-'
//  (_/    (_/ ,;|.<`
//   \    ,     ;-`
//     >   \    /
//    (_,-'`> .'
//         (_,' 
using System;
class Personagens{
    public string nome;
    public int vida;
    public int ataque;
    public int defesa;
    public int ataqueEspecial;
    
    public Personagens(string nome, int vida, int ataque, int defesa, int ataqueEspecial)
    {
        this.nome = nome;
        this.vida = vida;
        this.ataque = ataque;
        this.defesa = defesa;
        this.ataqueEspecial = ataqueEspecial;
    }
    
    public virtual void Atacar(Personagens alvo){
     if(ataque >= alvo.defesa){
        int dano = ataque - alvo.defesa;
        alvo.vida = alvo.vida - dano;
        Console.WriteLine($"\n{nome} ataca {alvo.nome} e causa {dano} de dano.");
     }
    }
    
    public virtual void UsarItem(){
        vida += 10;
        Console.WriteLine($"\n{nome} usa uma pocao e agora tem {vida} de vida!");
    }
    
    public virtual void GameOver(){
        Console.WriteLine("\n Você morreu, fim de jogo.");
    }
}

class Mago : Personagens{
    public Mago(string nome, int vida, int ataque, int defesa, int ataqueEspecial) : base(nome, vida, ataque, defesa, ataqueEspecial){}
    
    public override void Atacar(Personagens alvo){
       if(ataqueEspecial >= alvo.defesa){
        int dano = ataqueEspecial - alvo.defesa;
        alvo.vida = alvo.vida - dano;
        Console.WriteLine($"\n {nome} atira uma magia em {alvo.nome} causando {dano} de dano");
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
        while (jogador.vida > 0 && inimigo.vida > 0)
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

            if (inimigo.vida > 0 && permissao == 1)
            {
                inimigo.Atacar(jogador);
            }

            Console.WriteLine($"\n{jogador.nome}: {jogador.vida}HP");
            Console.WriteLine($"{inimigo.nome}: {inimigo.vida}HP");
        }

        if (jogador.vida <= 0)
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
