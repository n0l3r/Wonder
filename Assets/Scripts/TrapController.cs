using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour {

    private Animator m_Anim;

    // Use this for initialization
    void Start () {
        m_Anim = GetComponent<Animator>();
        m_Anim.SetBool("active", false);
    }
	
	// Update is called once per frame
	void Update () {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Trap" && other.gameObject.tag != "Ground")
           m_Anim.SetBool("active", true); 
    }
}
