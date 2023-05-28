using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRailHandler : MonoBehaviour
{
    public GameObject Rail;

    void Start(){
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            Rail.GetComponent<RailController>().direction = Rail.GetComponent<RailController>().direction == "left" ? "right" : "left";
        }
    }
    

   
    
}
