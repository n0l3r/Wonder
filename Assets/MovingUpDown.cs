using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUpDown : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float speed = 300f;
    [SerializeField] private float yMin = 0f;
    [SerializeField] private float yMax = 0f;
    Rigidbody2D m_Rigidbody2D;

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.velocity = new Vector2(0, speed);
    }

    // buat objek bulak balik dari kiri ke kanan, dan sebaliknya dengan batas xMin dan xMax
    void Update()
    {
        if (transform.position.y < yMin)
        {
            m_Rigidbody2D.velocity = new Vector2(0, speed);
        } else if (transform.position.y > yMax)
        {
            m_Rigidbody2D.velocity = new Vector2(0, -speed);
        }
    }
}
