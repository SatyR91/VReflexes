using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginButton : MonoBehaviour {

    public bool firstTime = false;
    protected ButtonAnimation bA;
    public GameObject indicatorObject;
    public GameController gC;

    // Use this for initialization
    public void Start()
    {
        bA = transform.GetChild(0).GetComponent<ButtonAnimation>();
        indicatorObject = transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (bA.pressed && !firstTime) {
            if (gC) {
                //Begin first VRT test
                firstTime = true;
                gC.BeginTest();
            }
            else
                Debug.Log("GameController not asigned");

        }

    }
}
