using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOutput : MonoBehaviour {

    // Main Screen (center) Text meshes
    public TextMesh MSTitle;
    public TextMesh MSText;
    public TextMesh MSCountdown;
    // Left screen Text meshes
    public TextMesh LSTitle;
    public TextMesh LSText;
    public TextMesh LSms;
    // Right screen Text meshes
    public TextMesh RSTitle;
    public TextMesh RSText;

    private string MSTextToPrint;
    public float pause = 0.05f;

    // Use this for initialization
    void Start () {
        SetupForBeginning();
    }

    public IEnumerator TypeMainScreenText(string text)
    {
        MSTextToPrint = text; // text to display
        SetupForTest();

        if (text == "Visual")
        {
            SetupForVisualTest();
        }
        if (text == "Audio")
        {
            SetupForAudioTest();
        }

        // Iterate over each letter
        foreach (char letter in MSTextToPrint.ToCharArray())
        {
            MSTitle.text += letter; // Add a single character to the text
            yield return new WaitForSeconds(pause);
        }
        yield return StartCoroutine(CountDownCoroutine());
        MSTitle.text = "Test running";
    }

    IEnumerator CountDownCoroutine()
    {
        MSCountdown.text = "";
        yield return new WaitForSeconds(2.5f);
        // Iterate over each letter
        for (int i = 3; i > 0; --i)
        {
            string currentNumber = i.ToString();
            MSCountdown.text = currentNumber; // Add a single character to the text
            yield return new WaitForSeconds(1f);
        }
        MSCountdown.text = "";
    }

    public void Clear()
    {
        MSTitle.text = "";
        MSText.text = "";
        MSCountdown.text = "";
        LSTitle.text = "";
        LSText.text = "";
        LSms.text = "";
        RSTitle.text = "";
        RSText.text = "";
    }

    public void SetupForBeginning()
    {
        Clear();
        MSTitle.text = "Press the central button to begin the test";
        MSText.text = "Instructions will appear on the right screen";

    }

    public void SetupForTest()
    {
        Clear();
        LSTitle.text = "Previous reaction time";
        LSText.text = "_";
        LSms.text = "ms";

        RSTitle.text = "Instructions";        
    }

    public void SetupForAudioTest()
    {
        Clear();
        SetupForTest();
        MSTextToPrint = "Audio test beginning in";
        RSText.text = "Click on the central button \nas soon as you hear the sound";
    }

    public void SetupForVisualTest()
    {
        Clear();
        SetupForTest();
        MSTextToPrint = "Visual test beginning in";
        RSText.text = "Click on the button \nas soon as it turns red";
    }

}
