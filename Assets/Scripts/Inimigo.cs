using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour
{
      public GameObject[] MeusDestinos;
    //Maquina de Estado
    public enum Estados { Mover, Parado};
    public Estados MeuEstado;
    //Contador para o tempo
    public int contador = 0;

    //Utilizar componete de AI
    private NavMeshAgent Agente;
    public GameObject Objetivo;
    void Start()
    {
        //Definir Destino
        int tamanhoLista = MeusDestinos.Length;
        int posSorteada = Random.Range(0, tamanhoLista);
        Objetivo = MeusDestinos[posSorteada];
        
        //QUAL ESTADO ESTOU NO MOMENTO
        MeuEstado = Estados.Mover;
        //Pegar o componente navMeshAgent
        Agente = GetComponent<NavMeshAgent>();




    }
    void Update()
    { 
        //FAZER O TEMPO PASSAR 
        contador ++;
        if(MeuEstado == Estados.Mover)
        {
            Agente.speed = 10;
            Mover();
            MudarDestino();
            if(contador > 100){
                contador = 0;
                MeuEstado = Estados.Parado;
            }
        }
        if(MeuEstado == Estados.Parado){
            Agente.speed = 0;
            if(contador > 50){
                contador = 0;
                MeuEstado = Estados.Mover;
            }
        }
        
    }
    //MOve o Corpo
    private void Mover(){
        //Pego a posicao do objetivo
        Vector3 posicaodoObjetivo = Objetivo.transform.position;
        //Move em direcao a posicao do objetivo
        Agente.SetDestination(posicaodoObjetivo);
    }


/* 
//DETECTAR INIMIGO
    void OnTriggerEnter(Collider col){
        
        if(col.gameObject.tag == "Inimigo"){
            Debug.Log("Visualizei inimigo");
        }
    }*/

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
