using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visao : MonoBehaviour
{
    public GameObject ObjetoVisto;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Avistou();
    }

    public bool Avistou(string nome)
    {
        RaycastHit hit;
        int alcance = 20;
        Vector3 MV = transform.position + (transform.forward*alcance);
         Debug.DrawLine(transform.position, MV, Color.white, 0.05f);
        if (Physics.Raycast(transform.position, transform.forward, out hit, alcance))
        {
            //Debug.Log(hit.collider.gameObject.name); 
            if(hit.collider.gameObject.tag == "Inimigo"){
               if(nome != hit.collider.gameObject.GetComponent<Soldado>().Nome){
                    ObjetoVisto = hit.collider.gameObject;
                    Debug.Log(ObjetoVisto.GetComponent<Soldado>().Nome);
                    return true;
               }else{
                   return false;
               }
            }else{
                return false;
            }
        }else{
            return false;
        }
        
               
    }

}
