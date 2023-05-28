using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    public float minX;
    public float maxX;

    private float offsetx;         //Private variable to store the offset distance between the player and camera
    private float offsety;         //Private variable to store the offset distance between the player and camera

    public float changeView = 250;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offsetx = transform.position.x - player.transform.position.x;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (player.transform.position.x + offsetx >= minX && player.transform.position.x + offsetx <= maxX)
        {
            transform.position = new Vector3(player.transform.position.x + offsetx, transform.position.y, 0);  
        }

        if (player.transform.position.y >= changeView)
        {
            transform.position = new Vector3(transform.position.x, changeView, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

    }
}
