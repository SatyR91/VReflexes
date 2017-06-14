using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingArea : MonoBehaviour {

    public bool handInArea;
    public Color positive;
    public Color negative;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        handInArea = true;
        GetComponent<MeshRenderer>().material.color = positive;
    }

    void OnTriggerExit(Collider other)
    {
        handInArea = false;
        GetComponent<MeshRenderer>().material.color = negative;
    }
}
