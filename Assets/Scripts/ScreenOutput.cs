using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOutput : MonoBehaviour {

    public TextMesh mainScreen;
    public TextMesh countdown;
    public TextMesh lastTime;

    private string mainScreenText;
    public float pause = 0.05f;

    // Use this for initialization
    void Start () {
        mainScreen = transform.FindChild("MainText").GetComponent<TextMesh>();
        countdown = transform.FindChild("Countdown").GetComponent<TextMesh>();
        lastTime = transform.FindChild("Time").GetComponent<TextMesh>();
    }

    public IEnumerator TypeMainScreenText(string text)
    {
        mainScreenText = text; // text to display
        mainScreen.text = ""; // clear the actual text

        // Iterate over each letter
        foreach (char letter in mainScreenText.ToCharArray())
        {
            mainScreen.text += letter; // Add a single character to the text
            yield return new WaitForSeconds(pause);
        }
        yield return StartCoroutine(CountDownCoroutine());
        mainScreen.text = "Test running";
    }

    IEnumerator CountDownCoroutine()
    {
        countdown.text = "";
        yield return new WaitForSeconds(2.5f);
        // Iterate over each letter
        for (int i = 3; i > 0; --i)
        {
            string currentNumber = i.ToString();
            countdown.text = currentNumber; // Add a single character to the text
            yield return new WaitForSeconds(1f);
        }
        countdown.text = "";
    }

}
