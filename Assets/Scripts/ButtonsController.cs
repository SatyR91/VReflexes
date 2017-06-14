﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{

    public bool active;

    public List<Interactable> buttons;
    public bool isButtonActive;
    public bool hasWaitCoroutineStarted;
    public int activeButtonIndex;
    public int timeBetweenStimulis;
    public int counter;

    public GameObject rightHand;
    public GameObject leftHand;

    // Use this for initialization
    void Start()
    {
        timeBetweenStimulis = 5;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (!hasWaitCoroutineStarted)
            {
                StartCoroutine(WaitCoroutine(timeBetweenStimulis, StartButton));
            }
            else {
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


    }

    int SelectRandomButton()
    {
        int randomInt = Random.Range(0, buttons.Count - 1);
        return randomInt;
    }

    IEnumerator WaitCoroutine(float maximumWaitTime, System.Action StartButton)
    {
        hasWaitCoroutineStarted = true;
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

    // --- Counter
    public int countRepetitions()
    {
        int tmp = 0;
        foreach (Interactable inter in buttons)
        {
            tmp += inter.getCounter();
        }
        counter = tmp;

        return tmp;
    }

    public void resetCounter()
    {
        counter = 0;
        foreach (Interactable inter in buttons)
        {
            inter.resetCounter();
        }
        // ---
    }
}
