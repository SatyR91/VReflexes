using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : Interactable {

    ButtonAnimation bA;

	// Use this for initialization
	void Start () {
        timer = new Timer();
        bA = transform.GetChild(0).GetComponent<ButtonAnimation>();
        indicatorObject = transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		if (bA.pressed && active)
        {
            End();
        }
	}

    public override void Begin()
    {
        timer.StartTimer();
        indicatorObject.GetComponent<MeshRenderer>().material.color = Color.red;
        active = true;
    }
}
