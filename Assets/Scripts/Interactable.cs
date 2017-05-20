using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    Timer timer;

	// Use this for initialization
	void Start () {
        timer = new Timer();
	}
	
	// Update is called once per frame
	void Update () {
        if (timer.hasStarted)
        {

        }
    }

    public void Begin() {
        timer.StartTimer();
        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    public void End() {
        timer.StopTimer();
        GetComponent<MeshRenderer>().material.color = Color.white;
        Debug.Log(timer.duration);
    }

    void OnTriggerEnter(Collider other)
    {
        End();
    }
}
