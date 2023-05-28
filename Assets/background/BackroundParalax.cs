using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackroundParalax : MonoBehaviour {

    public Transform cameraTransform;
    public float paralaxSpeed = 0.1f;
    private float lastCamera;
	// Use this for initialization
	void Start () {
        lastCamera = cameraTransform.position.x;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float deltax = cameraTransform.position.x - lastCamera;
        transform.position += Vector3.left * (deltax * paralaxSpeed);
        lastCamera = cameraTransform.position.x;
    }
}
