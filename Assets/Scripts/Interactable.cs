using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Timer timer;
    public bool active;
    protected GameObject indicatorObject;

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
        indicatorObject.GetComponent<MeshRenderer>().material.color = Color.red;
        active = true;
    }

    public void End() {
        timer.StopTimer();
        indicatorObject.GetComponent<MeshRenderer>().material.color = Color.red;
        Debug.Log(timer.duration);
        active = false;
    }


}
