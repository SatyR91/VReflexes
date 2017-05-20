using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour {

    public List<Interactable> buttons;
    public bool isButtonActive;
    public bool hasWaitCoroutineStarted;
    public int activeButtonIndex;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasWaitCoroutineStarted)
        {        
            StartCoroutine(WaitCoroutine(5, StartButton));
        }
        else
        {
            if (isButtonActive)
            {
                if (!buttons[activeButtonIndex].active)
                {
                    hasWaitCoroutineStarted = false;
                    isButtonActive = false;
                    activeButtonIndex = -1;
                }
            }
            
        }
        
	}

    int SelectRandomButton() {
        int randomInt = Random.Range(0, buttons.Count);
        return randomInt;
    }

    IEnumerator WaitCoroutine(float maximumWaitTime, System.Action StartButton)
    {
        hasWaitCoroutineStarted = true;
        Debug.Log("Coroutine Started");
        float randomDuration = Random.Range(1, maximumWaitTime);
        
        yield return new WaitForSeconds(randomDuration);
        StartButton();
    }

    void StartButton()
    {
        isButtonActive = true;
        int randomIndex = SelectRandomButton();
        activeButtonIndex = randomIndex;
        buttons[activeButtonIndex].Begin();
    }
}
