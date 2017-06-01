using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int globalCounter;
    public ButtonsController VisualButtonController;
    public ButtonsController AudioButtonController;

	// Use this for initialization
	void Start () {
        globalCounter = 0;

        VisualButtonController.active = true;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (globalCounter > 15)
        {
            // STOP GAME
            Debug.Log("End of test");
            VisualButtonController.active = false;
            AudioButtonController.active = false;
        }
		else if (VisualButtonController.countRepetitions() > 4)
        {
            globalCounter += VisualButtonController.counter;
            VisualButtonController.resetCounter();
            VisualButtonController.active = false;
            AudioButtonController.active = true;
        }
        else if (AudioButtonController.countRepetitions() > 4)
        {
            globalCounter += AudioButtonController.counter;
            VisualButtonController.resetCounter();
            VisualButtonController.active = true;
            AudioButtonController.active = false;
        }
	}


}
