using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRock : MonoBehaviour {

    public GameObject rock;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        rock.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
