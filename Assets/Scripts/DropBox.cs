using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour {

    private int count = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(count == 3)
        {
            this.GetComponent<HingeJoint2D>().enabled = false;
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {           
            count++;
        }
    }
}
