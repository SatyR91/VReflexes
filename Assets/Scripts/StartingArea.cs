using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingArea : MonoBehaviour {

    public bool handInArea;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        handInArea = true;
    }

    void OnTriggerExit(Collider other)
    {
        handInArea = false;
    }
}
