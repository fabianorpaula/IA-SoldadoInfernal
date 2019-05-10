using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldado : MonoBehaviour
{
    ///DADOS DO JOGADOR
    public string Nome = "COLOQUE SEU NOME";//DESS JEITO,não é para botar nome
    public int Vida = 10;
    private int TempoTiro = 20;
    
    public void TomeiDano(){
        Vida--;
        Debug.Log(Nome+": "+Vida);
        if(Vida <= 0){
            Morreu();
        }
    }

    void Morreu(){
        Destroy(gameObject);
    }













    //Visao do Personagem
    public Visao MeusOlhos;
    //uTILIZADO PARA CONTROLAR AS AÇÕES DO PERSONAGEM
    public Actions Acao;
    //uTILIZADO PARA SETAR A ARMA QUE O PERSONAGEM USA
    public PlayerController Arma;
    //uTILIZADO PARA ATIRAR
    public Atirar Mira;

    public GameObject[] MeusDestinos;
    //Maquina de Estado

    public enum Estados { Mover, Parado, Perseguir, Atirar};
    public Estados MeuEstado;
    //Contador para o tempo
    public int contador = 0;

    //Utilizar componete de AI
    private NavMeshAgent Agente;
    public GameObject Objetivo;
    void Start()
    {
        //dEFINIR ARMA
        Arma.SetArsenal("Rifle");

        //Definir Destino
        int tamanhoLista = MeusDestinos.Length;
        int posSorteada = Random.Range(0, tamanhoLista);
        Objetivo = MeusDestinos[posSorteada];
        
        //QUAL ESTADO ESTOU NO MOMENTO
        MeuEstado = Estados.Mover;
        //cOMEÇAR A MOVER
        Acao.Run();
        //Pegar o componente navMeshAgent
        Agente = GetComponent<NavMeshAgent>();




    }
    void Update()
    { 

        //Evitar ERRO
        if(Objetivo == null){
            Debug.Log("ALGUEM MORREU");
            int tamanhoLista = MeusDestinos.Length;
            int posSorteada = Random.Range(0, tamanhoLista);
            Objetivo = MeusDestinos[posSorteada];
            MeuEstado = Estados.Mover;
        }

        //FAZER O TEMPO PASSAR 
        contador ++;
        if(MeuEstado == Estados.Mover)
        {
            Agente.speed = 30;
            Mover();
            MudarDestino();
            if(contador > 100){
                contador = 0;
                MeuEstado = Estados.Parado;
                Acao.Stay();
            }
            Visualizar();
        }
        if(MeuEstado == Estados.Parado){
            Agente.speed = 0;
            
            if(contador > 50){
                contador = 0;
                MeuEstado = Estados.Mover;
                Acao.Run();
            }
            Visualizar();
        }
        if(MeuEstado == Estados.Perseguir){
            Agente.speed = 2;
            
            Mover();
            TempoTiro++;
            transform.LookAt(Objetivo.transform.position);
            if(Vector3.Distance(transform.position, Objetivo.transform.position)    < 5){
                if(TempoTiro >= 20){
                    MeuEstado = Estados.Atirar;
                }
                
            }else{
                MeuEstado = Estados.Mover;
            }

            
        }

        if(MeuEstado == Estados.Atirar){
            transform.LookAt(Objetivo.transform.position);
            Mira.Atirou();
            Acao.Attack();
            TempoTiro = 0;
            MeuEstado = Estados.Perseguir;
            Acao.Aiming();

        }
        
    }
    //MOve o Corpo
    private void Mover(){
        //Pego a posicao do objetivo
        Vector3 posicaodoObjetivo = Objetivo.transform.position;
        //Move em direcao a posicao do objetivo
        Agente.SetDestination(posicaodoObjetivo);
    }


    void Visualizar(){
        if(MeusOlhos.Avistou(Nome) == true){
            Debug.Log("Estou o vendo o inimigo");
            Objetivo = MeusOlhos.ObjetoVisto;
            MeuEstado = Estados.Perseguir;
            Acao.Aiming();
            

        }
    }


//DETECTAR INIMIGO
/* 
    void OnTriggerEnter(Collider col){
        
        if(col.gameObject.tag == "Inimigo"){
            //Debug.Log("Visualizei inimigo");
        }
    }
    */

    void MudarDestino(){
        Vector3 minhapos = transform.position;
        Vector3 destinopos = Objetivo.transform.position;
        float distanciaObjetivo = Vector3.Distance(minhapos, destinopos);
        //if(Vector3.Distance(transform.position, Objetivo.transform.position) < 5)
        if(distanciaObjetivo < 5){
        //Definir Destino
        int tamanhoLista = MeusDestinos.Length;
        int posSorteada = Random.Range(0, tamanhoLista);
        Objetivo = MeusDestinos[posSorteada];
        }
    }

}
