using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    [SerializeField] public LayerMask m_WhatIsThat;
    public Vector2 k_size = new Vector2(10,10);
    public float angle = 180;
    private bool m_isFollow;
    private Rigidbody2D m_Rigidbody2D;
    GameObject player;
    // Use this for initialization
    void Start () {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        m_isFollow = false;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, k_size,angle, m_WhatIsThat);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && Input.GetKey(KeyCode.E))
            {
                m_isFollow = true;
                player = colliders[i].gameObject;
      
                this.transform.SetParent(player.GetComponent<Transform>());
                m_Rigidbody2D.velocity = player.GetComponent<Rigidbody2D>().velocity;
                m_Rigidbody2D.angularVelocity = player.GetComponent<Rigidbody2D>().angularVelocity;
            }

        }
        if (m_isFollow == false)
        {
            this.transform.parent = null;
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, k_size);
    }
}
