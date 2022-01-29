using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ParallaxForGrids : MonoBehaviour {

    private float length;
    private float startpos;
    public GameObject camera;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start() {
        startpos = transform.position.x;
        length = GetComponentInChildren<Tilemap>().size.x;
    }

    void FixedUpdate() {
        float temp = (camera.transform.position.x * (1 - parallaxEffect));
        float dist = (camera.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
