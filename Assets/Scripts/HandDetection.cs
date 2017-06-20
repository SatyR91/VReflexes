using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDetection : MonoBehaviour {

    public Interactable button;
    public ButtonAnimation bA;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hand = collision.gameObject;
        if (button.timer.hasStarted && bA.pressed)
        {
            if (button.timer.currentDuration() > 3000f)
            {
                button.hands.Add("NA");
            }
            else
            {
                button.hands.Add(hand.name);
            }
        }
    }
}
