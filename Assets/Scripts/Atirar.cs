using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Atirou()
    {
        RaycastHit hit;
        int alcance = 30;
        Vector3 MV = transform.position + (transform.forward*alcance);
         Debug.DrawLine(transform.position, MV, Color.red, 0.05f);
        if (Physics.Raycast(transform.position, transform.forward, out hit, alcance))
        {
            //Debug.Log(hit.collider.gameObject.name); 
            if(hit.collider.gameObject.tag == "Inimigo"){
                hit.collider.gameObject.GetComponent<Soldado>().TomeiDano();
                return true;
            }else{
                return false;
            }
        }
        else{
            return false;
        }
               
    }

}
