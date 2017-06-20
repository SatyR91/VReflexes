﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : Interactable {

    protected ButtonAnimation bA;

	// Use this for initialization
	public void Start () {
        timer = new Timer();
        bA = transform.GetChild(0).GetComponent<ButtonAnimation>();
        indicatorObject = transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	protected override void Update () {
        if (timer.hasStarted)
        {
            if (timer.currentDuration() > 3000) //maximum time given for user to click on the butto/
            {
                Debug.Log("MISSION FAILED");
                hands.Add("NA");
                StartCoroutine(bA.ChangeColor(bA.gameObject, Color.red, bA.inactiveColor, 0.2f));
                End();
            }
            else if (bA.pressed && active)
            {
                End();
            }
        }
        
	}

    public override void Begin(bool b)
    {
        perturbated = b;

        indicatorObject.GetComponent<MeshRenderer>().material.color = Color.red;
        timer.StartTimer();
        active = true;
    }

    public override void Fake()
    {
        indicatorObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    public override void ResetColor()
    {
        indicatorObject.GetComponent<MeshRenderer>().material.color = indicatorObject.GetComponent<ButtonAnimation>().inactiveColor;
    }
}
