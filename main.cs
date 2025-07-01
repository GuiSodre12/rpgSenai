// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

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
        Console.WriteLine($"{nome} ataca {alvo.nome} e causa {dano} de dano");
     }
    }
    
}

class Mago : Personagens{
    public Mago(string nome, int vida, int ataque, int defesa, int ataqueEspecial) : base(nome, vida, ataque, defesa, ataqueEspecial){}
    
    public override void Atacar(Personagens alvo){
       if(ataqueEspecial >= alvo.defesa){
        int dano = ataqueEspecial - alvo.defesa;
        alvo.vida = alvo.vida - dano;
        Console.WriteLine($"{nome} atira uma magia em {alvo.nome} causando {dano} de dano");
     }
    }
    
    
    
}

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Personagens guerreirinho = new Personagens("Guerreirinho", 20, 15, 10, 2);
        Personagens mago = new Mago("John Arias", 10, 8, 8, 17);
        
        guerreirinho.Atacar(mago);
        mago.Atacar(guerreirinho);
        
    }
}
