using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 22000f;                  // Amount of force added when the player jumps.
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] public LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField] public LayerMask m_WhatIsTackle;
        [SerializeField] public LayerMask m_WhatIsSwing;


 
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        public float k_GroundedRadius = 1f; // Radius of the overlap circle to determine if grounded
        private float k_SwingRadius = 5f; // Radius of the overlap circle to determine if grounded
 
        private bool m_Grounded;            // Whether or not the player is grounded.\  
        private Transform m_FrontCheck;
        public float k_FrontRadius = 0.1f; 
        private bool m_Tackled;
        public Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        public bool m_Dead = false;
        private bool m_inWater = false;
        private bool m_Swing = false;
        private bool m_MoveWhenSwing = false;
        private bool m_DownSteep = false;
        private int childCount = 0;
        private AudioSource m_AudioSource;
        public AudioClip m_JumpSound;
        public AudioClip m_WalkSound;


        GameObject rope;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_FrontCheck = transform.Find("FrontCheck");
            childCount = 2;

            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_AudioSource = GetComponent<AudioSource>();

            m_WalkSound = Resources.Load<AudioClip>("Sound/Walk");
            m_JumpSound = Resources.Load<AudioClip>("Sound/Jump");
        }

        private void Update()
        {
            if(m_MoveWhenSwing == true)
                StartCoroutine(SwingOnAir());

            // play sound when move on ground
            if (m_Grounded && m_Anim.GetFloat("Speed") > 0.1f && !m_AudioSource.isPlaying)
            {
                m_AudioSource.clip = m_WalkSound;
                m_AudioSource.Play();
            }
            else if (m_Grounded && m_Anim.GetFloat("Speed") < 0.1f)
            {
                m_AudioSource.Stop();
            }

            // play sound when jump
            if (m_Grounded && Input.GetButtonDown("Jump"))
            {
                m_AudioSource.clip = m_JumpSound;
                m_AudioSource.Play();
            }


        }

        IEnumerator SwingOnAir()
        {
            yield return new WaitForSeconds(0.5f);
            m_MoveWhenSwing = false;
        }

        private void FixedUpdate()
        {
            m_Grounded = false;
            m_Tackled = false;
            m_Swing = false;
           

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
      
            }

            Collider2D[] tackle = Physics2D.OverlapCircleAll(m_FrontCheck.position, k_FrontRadius, m_WhatIsTackle);
            for (int i = 0; i < tackle.Length; i++)
            {
                if (tackle[i].gameObject != gameObject)
                    m_Tackled = true;

            }
            if (!m_MoveWhenSwing)
            {
                Collider2D[] swing = Physics2D.OverlapCircleAll(transform.position, k_SwingRadius, m_WhatIsSwing);
                for (int i = 0; i < swing.Length; i++)
                {
                    if (swing[i].gameObject != gameObject)
                    {
                        m_Swing = true;
                        rope = swing[i].gameObject;
                        //rope.GetComponent<FixedJoint2D>().enabled = true;
                        //rope.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
                        this.transform.SetParent(rope.GetComponent<Transform>());
                        m_Rigidbody2D.velocity = this.transform.parent.GetComponent<Rigidbody2D>().velocity;
                        m_Rigidbody2D.angularVelocity = this.transform.parent.GetComponent<Rigidbody2D>().angularVelocity;


                    }

                }
            }


            m_Anim.SetBool("Ground", m_Grounded);
            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y / 27.5f);

            if(m_Rigidbody2D.velocity.y <= -900f)
            {            
                m_Anim.SetBool("Dangered", true);
                m_Dead = true;
                StartCoroutine(Restart());
            }

           
        }

        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, k_SwingRadius);
        }

        public void Move(float move, bool jump)
        {

            if (m_Dead)
                return;
            if (m_Swing)
            {
                if (jump)
                {
                    this.transform.parent = null;
                    m_MoveWhenSwing = true;               
                    m_Rigidbody2D.AddForce(new Vector2(0, m_JumpForce));
                }
                else
                {
                    m_Rigidbody2D.velocity = new Vector2(0, 20);
                }
            }
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || (m_AirControl && !m_Tackled))
            {          
                

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));
                float turbo = m_DownSteep ? 1.3f : 1;

                if (transform.childCount != childCount && move != 0)
                {
                    move /= 2;
                    m_Anim.SetFloat("Speed", 0.05f);

                }           

                if (m_Rigidbody2D.velocity.y < 0)
                {
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed * turbo, m_Rigidbody2D.velocity.y * turbo);
                }
                else
                {
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed / turbo, m_Rigidbody2D.velocity.y / turbo);
                }


                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    if(transform.childCount == childCount)
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    if(transform.childCount == childCount)
                        Flip();
                }
            }
            
            // If the player should jump...
            if (jump && transform.childCount == childCount && (m_Grounded && m_Anim.GetBool("Ground")))
            {
                float jumpForce = m_JumpForce;
                if (m_inWater)
                    jumpForce /= 3;
                float turbo = m_DownSteep ? 1.1f : 1;
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                if (m_Rigidbody2D.velocity.y < 0)
                {
                    m_Rigidbody2D.AddForce(new Vector2(m_Rigidbody2D.velocity.x * turbo, jumpForce * turbo));
                }
                else
                {
                    m_Rigidbody2D.AddForce(new Vector2(m_Rigidbody2D.velocity.x / turbo, jumpForce / turbo));
                }                           
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Trap")
            {
                m_Anim.SetBool("Dangered", true);
                m_Dead = true;
                StartCoroutine(Restart());
            }

            if (other.tag == "SoulEater")
            {
                m_Anim.SetBool("Dangered", true);
                m_Dead = true;
                StartCoroutine(Restart());
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Rock")
            {
                if (Mathf.Abs(other.gameObject.GetComponent<Rigidbody2D>().velocity.y) >= 100)
                {
                    foreach (Collider2D c in this.GetComponents<Collider2D>())
                    {
                        c.isTrigger = true;                      
                    }
                    m_Anim.SetBool("Dangered", true);
                    m_Dead = true;
                    StartCoroutine(Restart());
                }
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {         
            if (other.tag == "Water")
            {
                m_inWater = false;
                m_AirControl = true;
                

            }
            if (other.tag == "Steep")
            {
                m_DownSteep = false;
            }
        }



        IEnumerator Restart()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);

        }
    }
}
