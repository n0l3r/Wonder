using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterController : MonoBehaviour
{
    private Animator m_Anim;

    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_Anim.SetBool("active", true);
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Trap" && other.gameObject.tag != "Ground")
            m_Anim.SetBool("active", true);
    }


}
