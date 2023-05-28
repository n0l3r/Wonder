using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


namespace UnityStandardAssets._2D{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour{
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

        private void Awake(){
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        private void Update(){
            if (!m_Jump){
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }

        private void FixedUpdate(){
            if(Input.GetKey(KeyCode.P)){
                SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
            }
            if (Input.GetKey(KeyCode.Escape)){
                SceneManager.LoadScene("Scenes/MainMenu");
            }

            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            m_Character.Move(h,m_Jump);
            m_Jump = false;
        }
    }
}
