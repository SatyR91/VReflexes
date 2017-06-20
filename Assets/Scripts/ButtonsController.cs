using System.Collections;
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
    public bool perturbations = false;

    public StartingArea leftStartingArea;
    public StartingArea rightStartingArea;
    public bool shouldDisplayWarningMessage;


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

            if (leftStartingArea.handInArea && rightStartingArea.handInArea) // both hands are in the starting areas
                shouldDisplayWarningMessage = false;
            else
                shouldDisplayWarningMessage = true;

            if (!hasWaitCoroutineStarted)
            {
                if (perturbations) 
                    StartCoroutine(WaitCoroutineWithPerturbations(timeBetweenStimulis, StartButton));
                else
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

    /// <summary>
    /// Select random buttons that are not the active ones
    /// </summary>
    /// <param name="activeButtonIndex"></param>
    /// <returns></returns>
    List<int> SelectRandomButtons(int activeButtonIndex)
    {
        List<int> buttonsIndexes = new List<int>();
        int i = 0;
        while (i < 2)
        {
            int randomInt = Random.Range(0, buttons.Count - 1);
            if (randomInt != activeButtonIndex)
            {
                ++i;
                buttonsIndexes.Add(randomInt);
            }
        }
        return buttonsIndexes;
    }

    IEnumerator WaitCoroutine(float maximumWaitTime, System.Action StartButton)
    {
        hasWaitCoroutineStarted = true;
        float randomDuration = Random.Range(1, maximumWaitTime);

        yield return new WaitForSeconds(randomDuration);
        activeButtonIndex = SelectRandomButton();
        StartButton();
    }

    IEnumerator WaitCoroutineWithPerturbations(float maximumWaitTime, System.Action StartButton)
    {
        hasWaitCoroutineStarted = true;
        float randomDuration = Random.Range(1, maximumWaitTime);

        yield return new WaitForSeconds(randomDuration);
        activeButtonIndex = SelectRandomButton();
        List<int> randomIndexes = SelectRandomButtons(activeButtonIndex);
        StartFakeButtons(randomIndexes);
        for (int i = 0; i < randomIndexes.Count; ++i)
        {
            StartCoroutine(ResetColorCoroutine(randomIndexes[i]));
        }
        StartButtonWithPerturbations();
    }

    IEnumerator ResetColorCoroutine(int index)
    {
        yield return new WaitForSeconds(1);
        buttons[index].ResetColor();
    }

    void StartFakeButtons(List<int> buttonsIndexes)
    {
        for (int i = 0; i < buttonsIndexes.Count; ++i)
        {
            buttons[buttonsIndexes[i]].Fake();
        }
    }

    void StartButton()
    {
        if (leftStartingArea.handInArea && rightStartingArea.handInArea) // both hands are in the starting areas
        {
            isButtonActive = true;
            buttons[activeButtonIndex].Begin(false);
        }
        else
        {
            // display warning message
            Debug.Log("Please put your hands in the starting areas");
            hasWaitCoroutineStarted = false;
        }
       
    }

    void StartButtonWithPerturbations()
    {
        if (leftStartingArea.handInArea && rightStartingArea.handInArea) // both hands are in the starting areas
        {
            isButtonActive = true;
            buttons[activeButtonIndex].Begin(true);
        }
        else
        {
            // display warning message
            Debug.Log("Please put your hands in the starting areas");
            hasWaitCoroutineStarted = false;
        }

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
