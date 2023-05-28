using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlatformerCharacter2D = UnityStandardAssets._2D.PlatformerCharacter2D;

public class ElectricController : MonoBehaviour
{
    // change color sprite renderer to black
    public SpriteRenderer spriteRenderer;
    public bool isActive = false;
    [SerializeField] int time = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        // change color and isActive every 1 second
        if(Time.frameCount % time == 0){
            if(isActive){
                spriteRenderer.color = Color.black;
                isActive = false;
            }else{
                spriteRenderer.color = Color.white;
                isActive = true;
            }
        }        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            if(isActive){
                other.gameObject.GetComponent<PlatformerCharacter2D>().m_Dead = true;
                other.gameObject.GetComponent<PlatformerCharacter2D>().m_Anim.SetBool("Dangered", true);
                StartCoroutine(Restart());
            }
        }
    }

    IEnumerator Restart()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);

        }
}
