using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDetection : MonoBehaviour {

    public Interactable button;
    public ButtonAnimation bA;
    public GameObject hand;
    public bool once;

	// Use this for initialization
	void Start () {
        once = false;
	}
	
	// Update is called once per frame
	void Update () {
    }

    private void OnCollisionEnter(Collision collision)
    {
        hand = collision.gameObject;
        Debug.Log(hand.name);
        if (button.active && !once)
        {
            button.hands.Add(hand.name);
            once = true;
        }
        else {
            once = false;
        }

    }
}
