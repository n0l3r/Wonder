using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFalling : MonoBehaviour
{
    public GameObject Box;
    Rigidbody2D m_Rigidbody2D;
    void Start()
    {
        m_Rigidbody2D = Box.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.tag == "Player"){
            m_Rigidbody2D.isKinematic = false;
        }
    }
}
