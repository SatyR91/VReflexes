using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Timer timer;
    public bool active;

	// Use this for initialization
	void Start () {
        timer = new Timer();
        active = false;
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
        active = true;
    }

    public void End() {
        timer.StopTimer();
        GetComponent<MeshRenderer>().material.color = Color.white;
        Debug.Log(timer.duration);
        active = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            End();
        }
    }
}
