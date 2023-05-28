using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLeftRight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float speed = 300f;
    [SerializeField] private float xMin = 0f;
    [SerializeField] private float xMax = 0f;
    Rigidbody2D m_Rigidbody2D;

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.velocity = new Vector2(speed, 0);
    }

    // buat objek bulak balik dari kiri ke kanan, dan sebaliknya dengan batas xMin dan xMax
    void Update()
    {
        if (transform.position.x < xMin)
        {
            m_Rigidbody2D.velocity = new Vector2(speed, 0);
        } else if (transform.position.x > xMax)
        {
            m_Rigidbody2D.velocity = new Vector2(-speed, 0);
        }
    }
}
